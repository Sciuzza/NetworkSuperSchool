using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerWeaponUse : NetworkBehaviour
{
    public Transform headTr;


    private const int NUMBER_OF_WEAPONS = 5;


    // Synched on the server to clients
    // -1 means infinite
    // 0 cannot shoot
    // >0 can shoot
    public SyncListInt ammoList;

    [SyncVar]
    public int selectedWeaponIndex = 0;

    public int CurrentWeaponAmmo
    {
        get { return ammoList[selectedWeaponIndex]; }
    }


    void Awake()
    {
        // Initialise the ammo list
        ammoList = new SyncListInt();
        for (int i = 0; i < NUMBER_OF_WEAPONS; i++)
        {
            ammoList.Add(0);
        }


        // Setup initial ammo
        ammoList[0] = 20;
    }

    public void ServerAddAmmo(int weaponIndex, int ammoAmount)
    {
        if (!isServer)
            return;

        ammoList[weaponIndex] += ammoAmount;
    }






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
        // Check if we can shoot
        bool canShoot = false;
        if (ammoList[selectedWeaponIndex] == -1)
        {
            canShoot = true;
        }
        else if (ammoList[selectedWeaponIndex] > 0)
        {
            ammoList[selectedWeaponIndex]--;
            canShoot = true;
        }

        // If we can shoot, shoot
        if (canShoot)
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
                    RpcShootFeedback(hit.point, Color.green);
                }
                else
                {
                    Debug.DrawLine(headTr.position, hit.point, Color.red, 1f);
                    RpcShootFeedback(hit.point, Color.red);
                }
      
             }
            else
            {
                RpcShootFeedback(headTr.forward*100, Color.red);
            }
        }

    }







    public GameObject lineRenderer;

    public IEnumerator DespawnShootFeedback(GameObject shootFeedback)
    {
        yield return new WaitForSeconds(1f);
        Destroy(shootFeedback);
    }

    [ClientRpc]
    public void RpcShootFeedback(Vector3 hitPoint, Color color)
    {
        GameObject shootSpawned = (GameObject)Instantiate(lineRenderer, headTr.position, Quaternion.identity);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(0, headTr.position);
        shootSpawned.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
        shootSpawned.GetComponent<LineRenderer>().SetColors(color, color);
        StartCoroutine(DespawnShootFeedback(shootSpawned));
    }
}