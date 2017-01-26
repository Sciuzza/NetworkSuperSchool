using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RocketAmmo : NetworkBehaviour
{
    bool isExploded;
    public Vector3 shootDir;
    public short playerId;
    public int explosiveForce = 5;
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
        if (isExploded)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(this.transform.position, explosionRadius);
        }
 
    }

    void OnCollisionEnter(Collision coll)
    {
        if (isServer)
        {
            if (coll.gameObject.GetComponent<NetworkIdentity>())
            {
                if (coll.gameObject.GetComponent<NetworkIdentity>().playerControllerId != playerId)
                {
                    ExplosionLogic();
                }

            }
            else
            {
                ExplosionLogic();
            }
        }
    }

    private void ExplosionLogic()
    {
        isExploded = true;
        GetComponentsInChildren<MeshRenderer>()[0].material.color = new Color(0, 0, 0, 0);
        playerColliders = Physics.OverlapSphere(this.transform.position, explosionRadius, 1 << 8);
        DoDamage();
        RpcFeedback();
        StartCoroutine(DeactivateTrigger());
    }

    void DoDamage()
    {
        if (isServer)
        {
            foreach (var player in playerColliders)
            {
                if (isServer)
                {
                    if (player.GetComponent<PlayerState>())
                    {
                        player.GetComponent<PlayerMovement>().isStunned = true;
                        Debug.Log(player.GetComponent<Rigidbody>());
                        player.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce/3, this.transform.position, explosionRadius, 3f, ForceMode.Impulse);
                        player.GetComponent<PlayerState>().ServerTakeDamage(25, playerId);
                    }
                }    
            }
        }      
    }

    IEnumerator DeactivateTrigger()
    {
        if (isServer)
        {
            yield return new WaitForSeconds(1);
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
       
    }
}
