using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public float InitSpeed = 0.00001f;
    public float Velocity = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var tmp = transform.position;
        tmp.x += InitSpeed;
        transform.position = tmp;
        //InitSpeed *= Velocity;
    }
}
