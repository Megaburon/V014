using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V014Controller : MonoBehaviour
{

    [SerializeField] [Range(0f, 100f)] float speed;
    [SerializeField] [Range(0f, 100f)] int addImpulse;
    [SerializeField] [Range(0f, 100f)] int maxImpulse;

    private float impulse;
    private bool thrust;

    private Rigidbody rb;
    private Camera mainCamera;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        impulse = 0;

        int initialImpulse = 5;
        rb.AddForce(Vector3.forward * initialImpulse, ForceMode.Impulse);

    }

    private void Update()
    {
        LookAtMouse();
        
        Vector3 velocity = rb.velocity;

        // al mantener presionado el botón frena progresivamente (aumenta fricción) y carga el impulso
        if (Input.GetMouseButton(0))
        {
            thrust = false;
            rb.drag = 2;
            impulse += addImpulse * Time.deltaTime;
        }

        //al soltar el botón regula el impulso y la fricción y activa el impulso mediante físicas
        else if (Input.GetMouseButtonUp(0))
        {
            rb.drag = 1.5f;
            if (impulse > maxImpulse) { impulse = maxImpulse; }
            if (impulse < speed) { impulse = speed; }
            thrust = true;
        }

        //mientras el jugador no interactua frena progresivamente hasta la velocidad por defecto.
        else if (Input.GetMouseButton(0) == false)
        {
            if (rb.velocity.magnitude > speed)
            {
                rb.drag = 1.5f;
            }

            else
            {
                rb.drag = 0;
            }
        }
    }

    private void FixedUpdate()
    {

        //al activarse, anula su velocidad y recibe un impulso hacia adelante, resetea el valor del impulso y se desactiva
        if (thrust == true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * impulse, ForceMode.Impulse);
            impulse = 0;
            thrust = false;
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
