using UnityEngine;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;

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

        // Respawn a few seconds later
        Invoke("ServerRespawn", 3.0f);
    }

    private void ServerRespawn()
    {
        // Revive the player
        health = 100;

        // Move to a spawn point
        Vector3 respawnPosition = FindObjectOfType<NetworkManager>().GetStartPosition().position;

        // Tell all clients that the player is now enabled and respawn it
        RpcRevivePlayer(respawnPosition);
    }

    [ClientRpc]
    public void RpcKillPlayer()
    {
        // Disable the player 
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerWeaponUse>().enabled = false;
        GetComponent<MeshRenderer>().material.color = Color.black;
        GetComponentInChildren<MeshRenderer>().material.color = Color.black;
    }

    [ClientRpc]
    public void RpcRevivePlayer(Vector3 respawnPosition)
    {
        this.transform.position = respawnPosition;

        // Re-enable the player 
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerWeaponUse>().enabled = true;
        GetComponent<MeshRenderer>().material.color = Color.white;
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }

}
