using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RocketAmmo : NetworkBehaviour
{
    bool isExploded;
    public Vector3 shootDir;

	void Update ()
    {
        if (isServer)
        {
            if (!isExploded)
            {
                this.transform.position += shootDir * 20 * Time.deltaTime;
                //this.transform.eulerAngles += new Vector3(0, 0, 50) * Time.deltaTime;
            }
        }     
	}

    void OnCollisionEnter(Collision coll)
    {
       // if (isServer)
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
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
       // if (isServer)
        {
            if (coll.GetComponent<PlayerState>())
            {
                coll.GetComponent<PlayerState>().ServerTakeDamage(50, playerControllerId);
            }
        }   
    }
}
