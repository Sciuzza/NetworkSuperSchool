using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GrenadeLauncherWeapon : AbstractWeapon {

    float grenadeForce = 50f;
    public GameObject grenade;

    // We are on Sever.
    public override void Shoot(Vector3 targetPosition) {

       // Vector3 shootDir = (targetPosition - transform.position).normalized;

		GameObject shell = Instantiate <GameObject> (grenade);
        shell.transform.position = weaponTr.position;
        NetworkServer.Spawn(shell);
		shell.GetComponent<Rigidbody>().AddForce(weaponTr.forward * grenadeForce, ForceMode.Impulse);
    }
    
}