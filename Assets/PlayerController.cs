using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float movementSpeed = 3f;
    public float jumpForce = 300f;

    public PlayerManager.MobileDevice device;    

    float horMovement, vertMovement;
    Vector2 curVelocity;
    private Rigidbody2D rb;
    float distToGround;

    bool isJumping, alreadyJumped;

    
    void Start () {
        var collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        distToGround = collider.bounds.extents.y;
        Log($"{device.Id}");
    }

    void Log(string msg) => Debug.Log($"#{device.Id}# {msg}");


    bool isGrounded() => Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.1f);

    void Update () {
        horMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical"); 
        curVelocity = rb.velocity;

        //var upPressed = Input.GetKey(KeyCode.UpArrow);
        //var spacePressed = Input.GetKey(KeyCode.Space);

        //Log($"{upPressed} {spacePressed} {isGrounded()}");

        //var mov = new Vector2(0, 0);

        //if ((upPressed || spacePressed) && isGrounded())
        //{
        //    Log("Jump");
        //    mov.y = jumpSpeed;
        //}

        //mov.x = movementSpeed * Input.GetAxis("Horizontal");


        //Log(mov);
        //rb.AddForce(mov);
    }

    void FixedUpdate()
    {
        if (horMovement != 0)
        {
            rb.velocity = new Vector2(horMovement * movementSpeed * Time.deltaTime * 10, curVelocity.y);
        }    
    }
}