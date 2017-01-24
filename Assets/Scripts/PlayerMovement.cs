using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    // Parameters
    public float speed = 3;
    private bool isJumping;
    
    // References
    public Transform headTr;
    private Rigidbody rb;

    // Input
    private float dx, dz;
    private Vector3 moveDir, rightDir, totalDir;
    
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
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
            #region Input

            dx = Input.GetAxis("Horizontal");
            dz = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.AddForce(0, 20, 0, ForceMode.Impulse);
                isJumping = true;
            }

            #endregion

            #region Movement

            moveDir = headTr.forward;
            moveDir.y = 0;
            moveDir.Normalize();

            rightDir = headTr.right;
            rightDir.y = 0;
            rightDir.Normalize();

            totalDir = moveDir * dz * 20 + rightDir * dx * 20;
            totalDir.Normalize();

            if (totalDir.sqrMagnitude == 0)
                rb.velocity += new Vector3(0, totalDir.y, 0);
            else if (!isJumping)
                rb.velocity = totalDir * 20;

            #endregion
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.transform.CompareTag("Terrain"))
            isJumping = false;
    }
}