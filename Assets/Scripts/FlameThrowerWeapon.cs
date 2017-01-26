using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FlameThrowerWeapon : AbstractWeapon
{
    public GameObject flamePrefab;
    private bool isActive;
    
    private void Awake()
    {
        flamePrefab = Resources.Load<GameObject>("FlamePrefab");

        if (flamePrefab == null)
            Debug.LogWarning("FlamePrefab NOT founded");
        else
            Debug.Log("FlamePrefab founded");

        ClientScene.RegisterPrefab(flamePrefab);
    }

    // Server
    public override void Shoot(Vector3 targetPosition)
    {
        if (!isActive)
        {
            isActive = true;

            GameObject flameGO = Instantiate(flamePrefab, weaponTr.position, weaponTr.transform.rotation) as GameObject;
            flameGO.transform.name = "FlameSpawned";
            
            if (isLocalPlayer)
                flameGO.transform.SetParent(GameObject.Find("Cube").transform);
            
            flameGO.GetComponent<Blaze>().playerId = this.playerControllerId;

            Debug.Log("Blaze instantiated");

            NetworkServer.Spawn(flameGO);
            RpcFireFeedback(flameGO);
        }
    }

    [ClientRpc]
    public void RpcFireFeedback(GameObject _flameGO)
    {
        StartCoroutine(BlazeCO(1, _flameGO));
    }

    private IEnumerator BlazeCO(float _seconds, GameObject _flameGO)
    {
        ParticleSystem blaze = _flameGO.transform.GetChild(0).GetComponent<ParticleSystem>();
        blaze.Play();
        Debug.Log("Blaze is ON");

        yield return new WaitForSeconds(_seconds);

        while(blaze.startLifetime > 0)
        {
            blaze.startLifetime -= 0.0125f;
            yield return null;
        }

        blaze.Stop();
        Debug.Log("Blaze is OFF");

        NetworkServer.Destroy(_flameGO);
        isActive = false;
    }
}