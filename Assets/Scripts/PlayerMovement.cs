using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    // Parameters
    public float speed = 5;

    // References
    public Transform headTr;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

	void Update ()
    {
        if (isLocalPlayer)
        {
            float dx = Input.GetAxis("Horizontal");
            float dz = Input.GetAxis("Vertical");

            Vector3 moveDir = headTr.forward;
            moveDir.y = 0;
            moveDir.Normalize();

            Vector3 rightDir = headTr.right;
            rightDir.y = 0;
            rightDir.Normalize();

            Vector3 totalDir = moveDir * dz + rightDir * dx;
            totalDir.Normalize();

            transform.Translate(totalDir * Time.deltaTime * speed);
        } 

        if (isServer)
        {
        //    Debug.Log(this.name + " - " + this.connectionToClient.address + " - " + this.transform.position);
        }
    }

}
