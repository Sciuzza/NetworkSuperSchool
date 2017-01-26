using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RocketAmmo : NetworkBehaviour
{
    bool isExploded;
    public Vector3 shootDir;
    public short playerId;

	void Update ()
    {
        if (isServer)
        {
            if (!isExploded)
            {
                this.transform.position += shootDir * 20 * Time.deltaTime;      
            }
        }     
	}

    void OnCollisionEnter(Collision coll)
    {
        if (isServer)
        {
            if (coll.transform.tag != "Player")
            {
                isExploded = true;
                GetComponentsInChildren<MeshRenderer>()[0].material.color = new Color(0, 0, 0, 0);
                GetComponentsInChildren<MeshRenderer>()[1].enabled = true;
                GetComponentInChildren<SphereCollider>().enabled = true;
                StartCoroutine(DeactivateTrigger());
            }       
        }
    }

    IEnumerator DeactivateTrigger()
    {
        if (isServer)
        {
            yield return new WaitForSeconds(2);
            NetworkServer.Destroy(gameObject);
        }
    
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isServer)
        {
            if (coll.GetComponent<PlayerState>())
            {
                coll.GetComponent<PlayerState>().ServerTakeDamage(50, playerId);
            }
        }   
    }
}
