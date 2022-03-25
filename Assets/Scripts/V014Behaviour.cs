using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V014Behaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float impulse;
    [SerializeField] float friction;
    [SerializeField] int maxImpulse;

    private Vector3 actualSpeed;

    private Vector3 screenPoint;
    private Vector3 mousePos;
    private Rigidbody rb;
    private Camera cameraRef;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        //cameraRef = Camera.main;

    }
    void Update()
    {
        // mousePos = Input.mousePosition;
        // screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        
    }

    private void FixedUpdate()
    {
        actualSpeed = transform.GetComponent<Rigidbody>().velocity;
        
        if (actualSpeed.magnitude > speed)
        {
            //
        }
    }

    void PlayerControler()
    {

        if (Input.GetMouseButton(0) && impulse < maxImpulse)
        {
            impulse += impulse;
            speed -= speed;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(Vector3.forward * impulse, ForceMode.Impulse);
            impulse = 10;
        }
    }
}
