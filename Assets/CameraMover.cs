using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMover : MonoBehaviour
{
    public float InitSpeed = 0.1f;
    public float Velocity = 1.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var tmp = transform.position;
        float zwischen = (float)Math.Pow((tmp.x+10f),(1/3));
        tmp.x += zwischen/90; //(float)Math.Sqrt(tmp.x  + 10f)/1000;
        transform.position = tmp;

        /*Moves this GameObject 2 units a second in the forward direction
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        } */
    }
}
