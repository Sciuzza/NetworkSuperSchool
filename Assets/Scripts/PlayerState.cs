using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;

    public float dyingTimer = 3;
    public float getBackToAction = 2;

    Vector3 tempOutOfZonePos = new Vector3(1000, 1000, 1000);

    public void ServerTakeDamage(int dmg)
    {
        if (!isServer)
            return;

        health -= dmg;

        if (health <= 0)
        {
            ServerKillPlayer();
        }
    }

    private void ServerKillPlayer()
    {
        // Notify the clients that the player is dead
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
        health = 100;

        RpcMovePlayerOutOfCombat(this.tempOutOfZonePos);

        var timer = 0;

        while (timer < this.getBackToAction)
        {
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
        // Disable the player 
        
        GetComponent<MeshRenderer>().material.color = Color.white;
    }


    [ClientRpc]
    public void RpcRevivePlayer(Vector3 respawnPosition)
    {
        this.transform.position = respawnPosition;

        // Re-enable the player 
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerWeaponUse>().enabled = true;
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
  
}
