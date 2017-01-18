using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponUse : NetworkBehaviour
{
    public Transform headTr;

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
                ps.TakeDamage(10);
            }
            else
            {
                Debug.DrawLine(headTr.position, hit.point, Color.red, 1f);
            }
        }
    }

}
