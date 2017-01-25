using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BasicWeapon : AbstractWeapon
{
    const float WEAPON_RANGE = 100;

    public override void Shoot(Vector3 targetPosition)
    {
        RaycastHit hit;
        Vector3 direction = targetPosition - weaponTr.transform.position;
        if (Physics.Raycast(weaponTr.position, direction, out hit, WEAPON_RANGE))
        {
            GameObject hitGo = hit.collider.gameObject;
            PlayerState ps = hitGo.GetComponent<PlayerState>();
            if (ps == null && hitGo.transform.parent != null) ps = hitGo.transform.parent.GetComponent<PlayerState>();

            if (ps != null)
            {
                Debug.DrawLine(weaponTr.position, hit.point, Color.green, 1f);
                ps.ServerTakeDamage(10, playerControllerId);
                RpcShootFeedback(hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(weaponTr.position, hit.point, Color.red, 1f);
                RpcShootFeedback(hit.point, Color.red);
            }

        }
        else
        {
            RpcShootFeedback(weaponTr.position + direction * WEAPON_RANGE, Color.red);
        }
    }

    [ClientRpc]
    public void RpcShootFeedback(Vector3 hitPoint, Color color)
    {
        GameObject shootSpawned = (GameObject)Instantiate(lineRenderer, weaponTr.position, Quaternion.identity);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(0, weaponTr.position);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
        shootSpawned.GetComponent<LineRenderer>().SetColors(color, color);
        StartCoroutine(DespawnShootFeedback(shootSpawned));
    }


    public GameObject lineRenderer;

    public IEnumerator DespawnShootFeedback(GameObject shootFeedback)
    {
        yield return new WaitForSeconds(1f);
        Destroy(shootFeedback);
    }
}
