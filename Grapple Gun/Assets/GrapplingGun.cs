using UnityEngine;
using System.Collections;

public class GrapplingGun : MonoBehaviour
{

    private Animator animator;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private float maxDistance = 50;
    private SpringJoint joint;


    public LayerMask whatIsGrappleable;
    public Transform shootPos;
    public Transform Camera;
    public Transform Player;


    public float spring = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast( Camera.position , Camera.forward , out hit , maxDistance , whatIsGrappleable))
        {
            animator.SetBool("IfShoot", true);
            FindObjectOfType<AudioManager>().Play("Shoot");

            grapplePoint = hit.point;
            joint = Player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance( Player.position,  grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;
            lr.positionCount = 2; 
        }
    }

    void StopGrapple()
    {
        animator.SetBool("IfShoot", false);
        lr.positionCount = 0;
        Destroy(joint);
    }


    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, shootPos.position);
        lr.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    /*
    IEnumerator DelaySootSound()
    {
        FindObjectOfType<AudioManager>().Play("Shoot");

        yield return new WaitForSeconds(1f);

        FindObjectOfType<AudioManager>().Pause("Shoot");
    }
    */
}
