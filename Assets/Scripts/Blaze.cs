using UnityEngine;
using UnityEngine.Networking;

public class Blaze : NetworkBehaviour
{
    public int damageFire = 15;
    public short playerId;

    private void OnTriggerEnter(Collider _other)
    {
        if (!isServer)
            return;
        
        PlayerState playerCollided = _other.gameObject.GetComponent<PlayerState>();

        if (playerCollided != null)
            playerCollided.ServerTakeDamage(damageFire, playerControllerId);
    }
}