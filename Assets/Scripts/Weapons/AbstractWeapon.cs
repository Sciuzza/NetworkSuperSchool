using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractWeapon : NetworkBehaviour {
    public Transform weaponTr;

    public abstract void Shoot(Vector3 targetPosition);
}
	
