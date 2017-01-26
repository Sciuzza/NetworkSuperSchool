using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class BoomerangWeapon : AbstractWeapon
{        
    public GameObject boomerangGo;

    void Awake()
    {
        boomerangGo = Resources.Load("BoomerangPrefab") as GameObject;
        ClientScene.RegisterPrefab(boomerangGo);
    }

    public override void Shoot(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - weaponTr.position;
        GameObject go = Instantiate(boomerangGo);

        go.GetComponent<BoomerangAmmo>().weaponTr = weaponTr;
        go.GetComponent<BoomerangAmmo>().idPlayer = this.playerControllerId;
        StartCoroutine(go.GetComponent<BoomerangAmmo>().Launch(go, direction));

        NetworkServer.Spawn(go);
    }

    
}