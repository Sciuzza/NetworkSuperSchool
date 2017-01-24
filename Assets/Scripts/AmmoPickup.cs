using UnityEngine;
using UnityEngine.Networking;

public class AmmoPickup : NetworkBehaviour
{
    public BoxCollider boxColl;
    public MeshRenderer mr;

    // Parameters
    public int ammoAmount = 10;

    [SyncVar(hook = "OnWeaponChange")]
    public int weaponIndex = 0;

    public int respawnTime = 5;

    public void OnWeaponChange(int newIndex)
    {
        weaponIndex = newIndex;
        mr.material.color = ColorController.GetColorForWeapon(newIndex);
    }

    public override void OnStartClient()
    {
        // We show only the mesh renderer
        Destroy(boxColl);
    }

    public override void OnStartServer()
    {
        // We use only the collider
        Destroy(mr);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!isServer)
            return;

        PlayerWeaponUse pwu = coll.gameObject.GetComponent<PlayerWeaponUse>();
        if (pwu != null)
        {
            pwu.ServerAddAmmo(weaponIndex, ammoAmount);

            ServerDeactivate();
            Invoke("ServerReactivate", respawnTime);
        }
    }

    void ServerDeactivate()
    {
        this.gameObject.SetActive(false);
        RpcDeactivate();
    }

    void ServerReactivate()
    {
        this.gameObject.SetActive(true);
        RpcReactivate();
    }

    [ClientRpc]
    void RpcDeactivate()
    {
        this.gameObject.SetActive(false);
    }

    [ClientRpc]
    void RpcReactivate()
    {
        this.gameObject.SetActive(true);
    }
}
