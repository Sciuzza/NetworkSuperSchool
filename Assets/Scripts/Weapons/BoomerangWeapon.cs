using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class BoomerangWeapon : AbstractWeapon
{
    private const float WEAPON_RANGE = 50;
        
    public GameObject boomerangGo;

    void Awake()
    {
        boomerangGo = Resources.Load("BoomerangPrefab") as GameObject;
        ClientScene.RegisterPrefab(boomerangGo);
    }

    public override void Shoot(Vector3 targetPosition)
    {
        GameObject go = Instantiate(boomerangGo);
        go.transform.position = Vector3.zero;
        NetworkServer.Spawn(go);

        StartCoroutine(Launch(go));
    }

    private IEnumerator Launch(GameObject go)
    {
        while ((weaponTr.position - (weaponTr.position + new Vector3(0, 0, WEAPON_RANGE))).magnitude > 0.01f)
        {
            go.transform.position = Vector3.Lerp(weaponTr.position, weaponTr.position + new Vector3(0, 0, WEAPON_RANGE), 5f);
            yield return 0;
        }
    }
}