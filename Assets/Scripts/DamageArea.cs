using UnityEngine;
using UnityEngine.Networking;

// @todo: this will not work correctly with a host
public class DamageArea : NetworkBehaviour
{
    public GameObject psGo;
    public BoxCollider boxColl;
    public MeshRenderer mr;

    // Parameters
    public int damageOnEnter = 10;

    public override void OnStartClient()
    {
        // We keep only the particle system
        Destroy(boxColl);
        Destroy(mr);
    }

    public override void OnStartServer()
    {
        // We only need the 'logic' part
        Destroy(psGo);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!isServer)
            return;

        PlayerState ps = coll.gameObject.GetComponent<PlayerState>();
        if (ps != null)
        {
            ps.ServerTakeDamage(damageOnEnter, MyNetworkLobbyManager.SERVER_PLAYER_ID);
        }
    }

}
