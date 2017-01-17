using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public Transform headTr;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public float speed = 5;

	void Update ()
    {
        if (isLocalPlayer)
        {
            float dx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float dz = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            Vector3 moveDir = headTr.forward;
            moveDir.y = 0;
            moveDir.Normalize();

            Vector3 rightDir = headTr.right;
            rightDir.y = 0;
            rightDir.Normalize();

            transform.Translate(moveDir * dz);
            transform.Translate(rightDir * dx);
        } 

        if (isServer)
        {
        //    Debug.Log(this.name + " - " + this.connectionToClient.address + " - " + this.transform.position);
        }
    }

}
