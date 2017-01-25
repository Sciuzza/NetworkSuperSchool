using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractWeapon : NetworkBehaviour {
    public Transform weaponTr;

    public virtual int StartingAmmo
    {
        get { return -1; }
    }

    public abstract void Shoot(Vector3 targetPosition);
}
	
