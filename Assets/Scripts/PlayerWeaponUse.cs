using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerWeaponUse : NetworkBehaviour
{
    public Transform headTr;
    public GameObject lineRenderer;

	void Update ()
    {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdShoot(headTr.position + headTr.forward*100);
            }
        }
    }

    [Command]
    public void CmdShoot(Vector3 targetPosition)
    {
        RaycastHit hit;
        Vector3 direction = targetPosition - transform.position;
        if(Physics.Raycast(headTr.position, direction, out hit))
        {
            GameObject hitGo = hit.collider.gameObject;
            PlayerState ps = hitGo.GetComponent<PlayerState>();
            if (ps == null && hitGo.transform.parent != null) ps = hitGo.transform.parent.GetComponent<PlayerState>();

            if (ps != null)
            {
                Debug.DrawLine(headTr.position, hit.point, Color.green, 1f);
                ps.ServerTakeDamage(10);
            }
            else
            {
                Debug.DrawLine(headTr.position, hit.point, Color.red, 1f);
            }
            RpcShootFeedback(hit.point);
        }
    }  

    public IEnumerator DespawnShootFeedback(GameObject shootFeedback)
    {
        yield return new WaitForSeconds(1f);
        Destroy(shootFeedback);
    }

    [ClientRpc]
    public void RpcShootFeedback(Vector3 hitPoint)
    {
        GameObject shootSpawned = (GameObject)Instantiate(lineRenderer, headTr.position, Quaternion.identity);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(0, headTr.position);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
        StartCoroutine(DespawnShootFeedback(shootSpawned));
    }
}
