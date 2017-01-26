using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class RocketWeapon : AbstractWeapon
{
    private const float WEAPON_RANGE = 200;

    public GameObject rocketPrefab;

    void Awake()
    {
        rocketPrefab = (GameObject)Resources.Load("RocketPrefab");
        ClientScene.RegisterPrefab(rocketPrefab);
    }

    public override void Shoot(Vector3 targetPosition)
    {
        GameObject rocketGo = (GameObject) Instantiate(rocketPrefab, weaponTr.position + weaponTr.forward * 3, Quaternion.identity);
        rocketGo.GetComponent<RocketAmmo>().shootDir = weaponTr.forward;
        rocketGo.GetComponent<RocketAmmo>().playerId = this.playerControllerId;
        rocketGo.transform.forward = weaponTr.forward;
      
        NetworkServer.Spawn(rocketGo);

    }	
}
