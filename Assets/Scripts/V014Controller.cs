using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V014Controller : MonoBehaviour
{
    [SerializeField] float impulse;
    [SerializeField] int speed;
    [SerializeField] int addImpulse;
    [SerializeField] int maxImpulse;
    [SerializeField] int initialImpulse;

    private bool idle;
    private bool thrust;

    private Rigidbody rb;
    private Camera mainCamera;


    private void Start()
    {
        initialImpulse = 5;
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        rb.AddForce(Vector3.right * initialImpulse, ForceMode.Impulse);
        impulse = 0;
        idle = true;
    }

    private void Update()
    {
        LookAtMouse();

        if (Input.GetMouseButton(0) && idle == true)
        {
            thrust = false;
            rb.drag = 5;
            impulse += addImpulse * Time.deltaTime;

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (impulse > maxImpulse) { impulse = maxImpulse; }
            rb.drag = 1.5f;
            idle = false;
            thrust = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
 
        if (thrust == true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * impulse, ForceMode.Impulse);
            impulse = 0;
            thrust = false;
        }

        if (rb.velocity.magnitude <= speed)
        {
            rb.drag = 0;
            idle = true;
        }
    }

    void LookAtMouse()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundplane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

}
