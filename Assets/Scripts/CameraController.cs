using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject v014;
    Vector3 v014Position;

    private void Start()
    {
        v014 = GameObject.Find("V014");
    }

    void Update()
    {
        transform.position = new Vector3(v014.transform.position.x, transform.position.y, v014.transform.position.z);
    }
}
