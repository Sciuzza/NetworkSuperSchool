using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

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
            transform.Translate(dx, 0, dz);
        } 

        if (isServer)
        {
            Debug.Log(this.name + " - " + this.connectionToClient.address + " - " + this.transform.position);
        }
    }

}
