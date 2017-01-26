using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RocketAmmo : NetworkBehaviour
{
    bool isExploded;
    public Vector3 shootDir;
    public short playerId;
    public int explosiveForce = 500;
    public GameObject particleFeedbackGo;
    public int explosionRadius = 5;
    private Collider[] playerColliders;
    public int damage = 25;

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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(this.transform.position, explosionRadius);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (isServer)
        {
            if (coll.gameObject.GetComponent<NetworkIdentity>().playerControllerId != playerId)
            {
                isExploded = true;
                GetComponentsInChildren<MeshRenderer>()[0].material.color = new Color(0, 0, 0, 0);
                GetComponentsInChildren<MeshRenderer>()[1].enabled = true;
                playerColliders = Physics.OverlapSphere(this.transform.position, explosionRadius, 1<<8);

                StartCoroutine(DeactivateTrigger());
            }       
        }
    }

    void DoDamage()
    {
        foreach (var player in playerColliders)
        {
            player.GetComponent<PlayerState>().ServerTakeDamage(25, playerId);
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

    [ClientRpc]
    void RpcFeedback()
    {
        Instantiate(particleFeedbackGo, this.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isServer)
        {
            if (coll.GetComponent<PlayerState>())
            {
                coll.GetComponent<PlayerMovement>().isStunned = true;
                Debug.Log(coll.GetComponent<Rigidbody>());
                coll.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, this.transform.position, 50,3,ForceMode.Impulse);
                coll.GetComponent<PlayerState>().ServerTakeDamage(50, playerId);
            }
        }   
    }
}
