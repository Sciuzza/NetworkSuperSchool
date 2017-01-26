using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class RocketWeapon : AbstractWeapon
{
    private const float WEAPON_RANGE = 200;

    public GameObject rocketGo;

    public override void Shoot(Vector3 targetPosition)
    {
        rocketGo = (GameObject) Instantiate(rocketGo, weaponTr.position + weaponTr.forward * 3, Quaternion.identity);
        rocketGo.GetComponent<RocketAmmo>().shootDir = weaponTr.forward;
        rocketGo.transform.forward = weaponTr.forward;
      
        NetworkServer.Spawn(rocketGo);

    }	
}
