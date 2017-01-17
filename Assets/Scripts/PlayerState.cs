using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(sendInterval = 0.5f)]
public class PlayerState : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;

    [SyncVar]
    public string playerName = "PIPPO";




    private TextMesh textMesh;

    public Transform headTr;

	void Start ()
    {
        textMesh = GetComponentInChildren<TextMesh>();

        Cursor.lockState = CursorLockMode.Locked;

        // Get the camera and put it on the head
        if (isLocalPlayer)
        {
            Camera.main.transform.SetParent(headTr);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }

        if (isLocalPlayer)
        {
            var ifield = FindObjectOfType<UnityEngine.UI.InputField>();
            ifield.onEndEdit.AddListener(ChangeName);
        }
    }
	

	void Update ()
    {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdShoot(headTr.position + headTr.forward*100);
            }

            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            headTr.localEulerAngles += new Vector3(-my*3, mx*3, 0);
        }

        /*if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }*/

        textMesh.text = playerName + ": " + health.ToString();
    }

    public void TakeDamage(int dmg)
    {
        if (!isServer)
            return;

        health -= dmg;
    }

    public void ChangeName(string newName)
    {
        if (isLocalPlayer)
        {
            // call the command
            CmdChangeName(newName);
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        this.playerName = newName;
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
