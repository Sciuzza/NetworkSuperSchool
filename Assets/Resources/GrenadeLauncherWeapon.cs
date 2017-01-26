using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GrenadeLauncherWeapon : AbstractWeapon {

    float grenadeForce = 100f;
    public GameObject grenadePrefab;

    void Awake()
    {
        grenadePrefab = (GameObject) Resources.Load("GrenadePrefab");
        ClientScene.RegisterPrefab(grenadePrefab);
    }

    // We are on Sever.
    public override void Shoot(Vector3 targetPosition) {

       // Vector3 shootDir = (targetPosition - transform.position).normalized;

		GameObject spawnedGrenade = Instantiate <GameObject> (grenadePrefab);
        spawnedGrenade.transform.position = weaponTr.position;
        spawnedGrenade.GetComponent<GrenadeProjectile>().playerId = playerControllerId;
        spawnedGrenade.GetComponent<Rigidbody>().AddForce(weaponTr.forward * grenadeForce, ForceMode.Impulse);
        NetworkServer.Spawn(spawnedGrenade);
    }
    
}