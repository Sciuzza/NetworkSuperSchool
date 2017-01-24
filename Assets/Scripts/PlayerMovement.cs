using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    // Parameters
    public float speed = 3;

    bool isJumping;

    // References
    public Transform headTr;
    public Rigidbody rb;

    // Input
    private float dx, dz;
    private Vector3 moveDir, rightDir, totalDir;
    
    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;


        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
            rb.isKinematic = true;
    }

    public override void OnStartServer()
    {
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            dx = Input.GetAxis("Horizontal");
            dz = Input.GetAxis("Vertical");
            
            #region Direction
            moveDir = headTr.forward;
            moveDir.y = 0;
            moveDir.Normalize();

            rightDir = headTr.right;
            rightDir.y = 0;
            rightDir.Normalize();

            totalDir = moveDir * dz * 10 + rightDir * dx * 10;
            totalDir.Normalize();
            #endregion
            
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.AddForce(0, 20, 0, ForceMode.Impulse);
                isJumping = true;
            }
                
            if (totalDir.sqrMagnitude == 0)
            {
                rb.velocity += new Vector3(0, totalDir.y, 0);
            }
            else if (!isJumping)
            {
                rb.velocity = totalDir * 20;
            }
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.transform.CompareTag("Terrain"))
        {
            isJumping = false;
        }
    }
}