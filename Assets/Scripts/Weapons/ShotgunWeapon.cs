using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ShotgunWeapon: AbstractWeapon
{
    const float WEAPON_RANGE = 50;

    Vector3 leftOffset = new Vector3(0, 0, -10);
    Vector3 rightOffset = new Vector3(0, 0, 10);

    public override void Shoot(Vector3 targetPosition)
    {
        RaycastHit hit;
        RaycastHit hitTwo;
        RaycastHit hitThree;

        Vector3 direction = targetPosition - weaponTr.transform.position;

        #region FirstShot

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
                RpcShootFeedback(hit.point + leftOffset, Color.red);
                RpcShootFeedback(hit.point + rightOffset, Color.red);
            }
            else
            {
                //Debug.DrawLine(weaponTr.position, hit.point, Color.red, 1f);
                RpcShootFeedback(hit.point, Color.red);
                RpcShootFeedback(hit.point + leftOffset, Color.red);
                RpcShootFeedback(hit.point + rightOffset, Color.red);
            }

        }
        #endregion

        #region Second Shoot leftOffset

        else if (Physics.Raycast(weaponTr.position, direction + leftOffset, out hitTwo, WEAPON_RANGE))
        {

            GameObject hitGoTwo = hitTwo.collider.gameObject;
            PlayerState ps = hitGoTwo.GetComponent<PlayerState>();
            if (ps == null && hitGoTwo.transform.parent != null) ps = hitGoTwo.transform.parent.GetComponent<PlayerState>();

            if (ps != null)
            {
                Debug.DrawLine(weaponTr.position, (hitTwo.point + leftOffset), Color.green, 1f);
                ps.ServerTakeDamage(10, playerControllerId);
                RpcShootFeedback(hitTwo.point + leftOffset, Color.green);
                RpcShootFeedback(hitTwo.point, Color.red);
                RpcShootFeedback(hitTwo.point + rightOffset, Color.red);
            }
            else
            {

                //Second Bullet
                Debug.DrawLine(weaponTr.position, (hitTwo.point + leftOffset), Color.red, 1f);
                RpcShootFeedback(hitTwo.point + leftOffset, Color.red);
                RpcShootFeedback(hitTwo.point + rightOffset, Color.red);
                RpcShootFeedback(hitTwo.point, Color.red);

            }


        }
        

        #endregion

        #region ThirdShoot rightOffset

        else if (Physics.Raycast(weaponTr.position, direction + rightOffset, out hitThree, WEAPON_RANGE))
        {
            GameObject hitGoThree = hitThree.collider.gameObject;
            PlayerState ps = hitGoThree.GetComponent<PlayerState>();
            if (ps == null && hitGoThree.transform.parent != null) ps = hitGoThree.transform.parent.GetComponent<PlayerState>();

            if (ps != null)
            {

                //Second Bullet
                Debug.DrawLine(weaponTr.position, (hitThree.point + rightOffset), Color.green, 1f);

                RpcShootFeedback(hitThree.point + rightOffset, Color.green);
                RpcShootFeedback(hitThree.point + leftOffset, Color.red);
                RpcShootFeedback(hitThree.point, Color.red);
            }
            else
            {

                //Second Bullet
                Debug.DrawLine(weaponTr.position, (hitThree.point + rightOffset), Color.red, 1f);
                RpcShootFeedback(hitThree.point + rightOffset, Color.red);
                RpcShootFeedback(hitThree.point + leftOffset, Color.red);
                RpcShootFeedback(hitThree.point, Color.red);

            }

        }

        else
        {
            RpcShootFeedback(weaponTr.position + direction * WEAPON_RANGE, Color.red);
            RpcShootFeedback(weaponTr.position + direction + leftOffset * WEAPON_RANGE, Color.red);
            RpcShootFeedback(weaponTr.position + direction + rightOffset * WEAPON_RANGE, Color.red);
        }
        #endregion

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
