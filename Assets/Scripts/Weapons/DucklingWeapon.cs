using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class DucklingWeapon : AbstractWeapon {

    public GameObject duckBulletGo;
    void Awake()
    {
        duckBulletGo = (GameObject)Resources.Load("DuckBulletPrefab");
        ClientScene.RegisterPrefab(duckBulletGo);        
    }
    public override void Shoot(Vector3 targetPosition)
    {
        GameObject newDuckBulletGO = Instantiate(duckBulletGo);
        newDuckBulletGO.transform.position = weaponTr.transform.position;
        NetworkServer.Spawn(newDuckBulletGO);
        newDuckBulletGO.GetComponent<Rigidbody>().AddForce(weaponTr.forward * 60, ForceMode.Impulse);
    }

    
}
