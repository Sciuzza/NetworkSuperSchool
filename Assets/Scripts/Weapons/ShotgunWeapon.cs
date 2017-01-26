using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ShotgunWeapon: AbstractWeapon
{
    const float WEAPON_RANGE = 50;

    [SyncVar]
    int fragments = 6;

    Vector3 x;
    Vector3 z;
    Vector3 y;

    int damage = 0;

    RaycastHit hit;

    void Awake()
    {
        lineRenderer = (GameObject)Resources.Load("LineRenderer");
    }

    void Randomize()
    {
        x = new Vector3(Random.Range(-8, 8), 0, 0);
        z = new Vector3(0, 0, Random.Range(-5, 5));
        y = new Vector3(0, Random.Range(-5, 5), 0);
    }

    public override void Shoot(Vector3 targetPosition)
    {
        
        //RaycastHit hitTwo;
        //RaycastHit hitThree;

        Vector3 direction = targetPosition - weaponTr.transform.position;

      

        for (int i = 0; i < fragments; i++)
        {
            Randomize();

            hit = new RaycastHit();

            if (Physics.Raycast(weaponTr.position, (direction + x + z + y), out hit, WEAPON_RANGE))
            {
                GameObject hitGo = hit.collider.gameObject;

                PlayerState ps = hitGo.GetComponent<PlayerState>();
                if (ps == null && hitGo.transform.parent != null) ps = hitGo.transform.parent.GetComponent<PlayerState>();

                if (ps != null)
                {
                    Debug.DrawLine(weaponTr.position, hit.point + x + z + y, Color.green, 1f);

                    ShotgunDamage(hit.distance);

                    ps.ServerTakeDamage(damage, playerControllerId);
                    RpcShootFeedback(hit.point + x + z + y, Color.green);

                }
                else if ((ps == null))
                {
                    //Debug.DrawLine(weaponTr.position, hit.point, Color.red, 1f);
                    RpcShootFeedback(hit.point + x + z + y, Color.red);

                }

            }

            else
            {
                RpcShootFeedback(weaponTr.position + direction +x+y+z * WEAPON_RANGE, Color.red);

            }
        }
        

    }

    void ShotgunDamage(float distance)
    {
        if (distance > 0 && distance <= 10)
            damage = 50;
        else if (distance > 10 && distance <= 20)
            damage = 40;
        else if (distance > 20 && distance <= 30)
            damage = 30;
        else if (distance > 30 && distance <= 40)
            damage = 20;
        else if (distance > 40 && distance <= 50)
            damage = 10;
       
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
