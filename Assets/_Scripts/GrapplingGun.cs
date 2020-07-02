using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    public new Transform camera;
    private float maxDistance = 400f;
    private SpringJoint joint;

    public float spring = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;

    public GameObject gunPosition;

    public Animator animator;

    PlayerControls controls;

    float grappling;
    void Awake()
    {
        controls = new PlayerControls();
        lr = GetComponent<LineRenderer>();

        if (Input.GetJoystickNames().Length > 0)
        {
            controls.Gameplay.Grapple.performed += ctx => grappling = ctx.ReadValue<float>();
            controls.Gameplay.Grapple.canceled += ctx => grappling = 0;
        } else
        {
            controls.Gameplay.Grapple.performed += ctx => Grappling();
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        StopGrapple();
        controls.Gameplay.Disable();
    }

    private bool gamepadGrappling;
    void Update()
    {
        if (PauseMenu.GameIsPaused)
            return;
        if (Input.GetJoystickNames().Length > 0)
        {
            if (grappling <= 0)
            {
                gamepadGrappling = false;
                StopGrapple();
                gunPosition.GetComponent<Animator>().enabled = true;
            }

            if (grappling > 0 && !gamepadGrappling)
            {
                gamepadGrappling = true;
                StopGrapple();
                StartGrapple();
                gunPosition.GetComponent<Animator>().enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    bool grappleOn = false;
    void Grappling()
    {
        if (!grappleOn)
        {
            StartGrapple();
            grappleOn = true;
        }
        else
        {
            StopGrapple();
            grappleOn = false;
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            gunPosition.GetComponent<Animator>().enabled = false;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;

            Debug.Log("Grapple Started");
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        Debug.Log("Grapple Stopped");
        gunPosition.GetComponent<Animator>().enabled = true;
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
