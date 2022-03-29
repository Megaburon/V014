using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallMovement : MonoBehaviour
{
    [Tooltip("Velocidad")]
    [SerializeField]
    private Vector3 velocity ;
    [Tooltip("Magnitud")]
    [SerializeField]
    private float magnitude;
    [Tooltip("Fuerza de movimiento")]
    [SerializeField]             
    [Range(0,1000)]               
    private float speed;
    [Tooltip("Velocidad Limite para ganar impulso")]    
    [SerializeField]
    [Range(0,100)]
    private float velocityTreshold ;
    //Rigid body del jugador
    private Rigidbody _rb;
    //TEST
    public Vector3 empuje;
    private void Start()
    {
        //Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {   
        velocity = _rb.velocity;        
        magnitude = velocity.magnitude; 
        Invoke("LookAtMouse",0);
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.timeScale > 0 && magnitude<=velocityTreshold)
        {
            _rb.velocity = Vector3.zero;
            speed = speed + 1;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && Time.timeScale > 0 && magnitude <= velocityTreshold)
        {
            Invoke("AddForce",0);
        }
    }
    private void LookAtMouse()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
        Vector3 mouseFollower;
        mouseFollower = mouseRay.origin + mouseRay.direction * midPoint;
        mouseFollower.Set(mouseFollower.x, 0, mouseFollower.z);
        transform.LookAt(mouseFollower);
    }

    private void AddForce()
    {
        _rb.AddRelativeForce(Vector3.forward*speed); 
        speed = 0;
    }
}

