using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GrenadeProjectile : NetworkBehaviour
{
    public short playerId;
    Rigidbody rb;

    public override void OnStartLocalPlayer()
    {
        //if (isLocalPlayer)
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
               
    }

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
            rb.isKinematic = true;
    }

    public override void OnStartServer()
    {
        rb.isKinematic = true;
    }
}