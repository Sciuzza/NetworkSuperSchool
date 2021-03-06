﻿using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    public GameObject face;
    private GameController gc;

    [SyncVar]
    public int health = 100;

    public float dyingTimer = 3;
    public float getBackToAction = 2;

    Vector3 tempOutOfZonePos = new Vector3(1000, 1000, 1000);

    [SyncEvent]
    public event System.Action EventOnDamageTaken;


    public override void OnStartServer()
    {
        ServerRefillHealth();
        gc = FindObjectOfType<GameController>();
    }

    public override void OnStartClient()
    {
        EventOnDamageTaken += ClientTakeDamage;
    }

    public override void OnStartLocalPlayer()
    {
        FindObjectOfType<HealthUI>().ps = this;
        face.gameObject.SetActive(false);
        
    }





    public void ServerRefillHealth()
    {
        health = 100;
    }

    public void ServerTakeDamage(int dmg, short hittingPlayerId)
    {
        if (!isServer)
            return;

        health -= dmg;

        EventOnDamageTaken();

        if (health <= 0)
        {
            ServerKillPlayer(hittingPlayerId);
        }
    }

    public void ClientTakeDamage()  // called through EventOnDamageTaken()
    {
        GetComponent<AudioSource>().Play();
    }


    private void ServerKillPlayer(short hittingPlayerId)
    {
        // Notify the clients that the player is dead
        if (hittingPlayerId == MyLobbyNetworkManager.SERVER_PLAYER_ID
            || hittingPlayerId == this.playerControllerId)
        {
            GetComponent<PlayerScore>().ChangeScore(-1);
        }
        else
        {
            gc.GetPlayerScoreById(hittingPlayerId).ChangeScore(1);
        }

        RpcKillPlayer();

        StartCoroutine(this.PlayerDyingTime());
    }
    

    [ClientRpc]
    public void RpcKillPlayer()
    {
        // Disable the player 
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerWeaponUse>().enabled = false;
        GetComponent<MeshRenderer>().material.color = Color.black;
        GameObject.FindGameObjectWithTag("Head").GetComponent<MeshRenderer>().material.color = Color.black;
    }

    private IEnumerator PlayerDyingTime()
    {
        var timer = 0.0f;

        while (timer < this.dyingTimer)
        {
            timer += Time.deltaTime;
            yield return null;

        }

        // Respawn a few seconds later
        StartCoroutine(this.ServerRespawn());
    }

    private IEnumerator ServerRespawn()
    {
        // Revive the player
        ServerRefillHealth();
        GetComponent<PlayerWeaponUse>().ServerRefillAmmo();


        RpcMovePlayerOutOfCombat(this.tempOutOfZonePos);

        var timer = 0.0f;

        while (timer < this.getBackToAction)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Move to a spawn point
        var respawnPosition = FindObjectOfType<NetworkManager>().GetStartPosition().position;

        // Tell all clients that the player is now enabled and respawn it
        this.RpcRevivePlayer(respawnPosition);


    }

    [ClientRpc]
    public void RpcMovePlayerOutOfCombat(Vector3 outOfCombatZonePos)
    {
        this.transform.position = outOfCombatZonePos;
    }


    [ClientRpc]
    public void RpcRevivePlayer(Vector3 respawnPosition)
    {
        this.transform.position = respawnPosition;

        // Re-enable the player 
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerWeaponUse>().enabled = true;
       // GetComponent<PlayerColorer>().SetColor(Color.white);:
    }
  
}
