using UnityEngine;
using System.Collections;

public class WallRun : MonoBehaviour
{
    public static bool isWallR = false;
    public static bool isWallL = false;
    public static bool Wallgrounded;
    public static bool readyToWallJump;
    public static int jumpCount = 0;


    public static Vector3 wallv;
    public static Vector3 wallNormal;

    public GameObject Camera;
    private LineRenderer lr;
    private Rigidbody rb;

    public LayerMask whatIsWallRunable;
    RaycastHit hitR;
    RaycastHit hitL;

    
    public float WallupForce = 5f;
    public float WallforwardForce = 5f;

    private float maxDistance = 1f;
    private float timeToSnap = 0.2f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FindWall();
    }

    void FindWall()
    {
        if (Physics.Raycast(transform.position, transform.right, out hitR, maxDistance, whatIsWallRunable))
        {
            if (hitR.collider != null && Input.GetKey(KeyCode.D))
            {

                Wallgrounded = true;

                isWallR = true;
                isWallL = false; 
                rb.useGravity = false;

                wallv = Vector3.Cross(hitR.normal, Vector3.up);
                wallNormal = hitR.normal;

            }
        }

        else if (Physics.Raycast(transform.position, -transform.right, out hitL, maxDistance, whatIsWallRunable))
        {
            if (hitL.collider != null && Input.GetKey(KeyCode.A))
            {

                Wallgrounded = true;

                isWallR = false;
                isWallL = true;
                rb.useGravity = false;

                wallv = Vector3.Cross(hitL.normal, Vector3.up);
                wallNormal = hitL.normal;

            }
        }

        //當沒碰到牆
        else
        {
            Wallgrounded = false;
            rb.useGravity = true;
            isWallR = false;
            isWallL = false;
        }

    }

}

