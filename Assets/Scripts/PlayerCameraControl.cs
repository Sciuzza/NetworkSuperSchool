using UnityEngine;
using UnityEngine.Networking;

public class PlayerCameraControl : NetworkBehaviour
{
    public Transform headTr;

	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Get the camera and put it on the head
        if (isLocalPlayer)
        {
            Camera.main.transform.SetParent(headTr);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }
    }
	
	void Update ()
    {
        if (isLocalPlayer)
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            headTr.localEulerAngles += new Vector3(-my*3, mx*3, 0);
        }
    }

}
