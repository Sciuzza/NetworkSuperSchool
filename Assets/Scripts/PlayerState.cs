using UnityEngine;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;

    public void TakeDamage(int dmg)
    {
        if (!isServer)
            return;

        health -= dmg;
    }

}
