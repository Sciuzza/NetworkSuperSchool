using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class BoomerangWeapon : AbstractWeapon
{
    private const float WEAPON_RANGE = 100;

    public GameObject boomerangGo;

    public override void Shoot(Vector3 targetPosition)
    {
        GameObject go = Instantiate(boomerangGo);
        go.transform.position = Vector3.zero;
        NetworkServer.Spawn(go);

        go.transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 2), 500f);
    }
	
	void Update ()
    {
                
    }
}
