using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float movementSpeed = 5.0f;
    private Rigidbody2D rb;

    void Start () {
        
    }
    
    void Update () {
        rb = GetComponent<Rigidbody2D>();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.AddForce(movement * movementSpeed);

    }
}