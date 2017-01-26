using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// In onore di Cristiano
/// </summary>
public class DuckBulletBrain : NetworkBehaviour
{
    //References
    private Rigidbody rb;
    private PlayerState ps;

    //Public Variables
    public float lifeTime;
    public bool gizmo = false;
    public float speed = 5;
    public GameObject particle;

    //Private Variables
    private bool isSearching = false;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

    }


    public override void OnStartClient()
    {
        if (!isLocalPlayer)
            rb.isKinematic = false;
    }

    public override void OnStartServer()
    {
        rb.isKinematic = true;
    }
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Terrain"))
        {
            if (other.gameObject.GetComponent<PlayerState>())
            {
                other.gameObject.GetComponent<PlayerState>().ClientTakeDamage();
                RpcFeedbackParticle();
                DestroyMe();
            }

        }
        else if (!isSearching)
        {
            isSearching = true;
            StartCoroutine(SearchPlayerCO());
        }

    }
    void OnDrawGizmos()
    {
        if (gizmo)
        {
            Gizmos.DrawSphere(this.transform.position, 20);
        }

    }
    IEnumerator SearchPlayerCO()
    {
        float elapsedTime = 0.0f;
        bool isFind = false;
        while (elapsedTime < lifeTime && !isFind)
        {
            gizmo = false;
            elapsedTime += Time.deltaTime;
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 10, (1 << 8));

            if (hitColliders.Length >= 1)
            {
                StartCoroutine(GoToNearPlayerCO(elapsedTime, hitColliders));
                isFind = true;       
            }            
            yield return null;
        }
        if (!isFind)
        {
            RpcFeedbackParticle();
            DestroyMe();
        }
        
    }

    IEnumerator GoToNearPlayerCO(float _elapsedTime, Collider[] players)
    {

        float elapsedTime = _elapsedTime;
        float nearest = 100f;
        GameObject nearestPlayer = null;
        foreach (var player in players)
        {
            float distance = Vector3.Distance(this.transform.position, player.gameObject.transform.position);
            if (distance < nearest)
            {
                nearest = distance;
                nearestPlayer = player.gameObject;
            }
        }
        while (elapsedTime < lifeTime)
        {
            Vector3 directionToPlayer = (nearestPlayer.transform.position - this.transform.position).normalized;
            this.transform.position += directionToPlayer * Time.deltaTime * speed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RpcFeedbackParticle();
        DestroyMe();
    }
    [ClientRpc]
    void RpcFeedbackParticle()
    {
        Instantiate(particle, this.transform.position, Quaternion.identity);
    }

    void DestroyMe()
    {
        //Network.Destroy(this.gameObject);
        Destroy(this.gameObject);
    }
}
