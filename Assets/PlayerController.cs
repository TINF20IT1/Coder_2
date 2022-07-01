using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float movementSpeed = 3f;
    public float jumpForce = 300f;
    public float jumpAmount = 10;

    public PlayerManager.MobileDevice device;    
    Animator animator;

    float horMovement, vertMovement;
    Vector2 curVelocity;
    private Rigidbody2D rb;
    float distToGround;

    bool isJumping, alreadyJumped, isWalking, isSliding;
    
    void Start () {
        var collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        distToGround = collider.bounds.extents.y;
        Log($"{device.Id}");
        isJumping = false;
        isWalking = false;
        isSliding = false;
    }

    void Log(string msg) => Debug.Log($"#{device.Id}# {msg}");


    bool isGrounded() => Physics2D.Raycast(transform.position, -Vector2.up, distToGround + 0.1f);

    void Update () {
        horMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical"); 
        curVelocity = rb.velocity;

        if (Mathf.Abs(horMovement) < 0.01f)
        {
            isWalking = false ;
        }
        else
        {
            isWalking = true ;
        }

        isJumping = Input.GetAxis("Jump")   < 0.01f ? false : true;
        isSliding = Input.GetAxis("Crouch") < 0.01f ? false : true;

        if (Input.GetAxisRaw("Jump") == 1 && isGrounded() && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Force);
            Debug.Log("NO!");
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isSliding", isSliding);
        if (isJumping && isGrounded())
            isJumping = !isJumping;
        if (horMovement != 0)
        {
            rb.velocity = new Vector2(horMovement * movementSpeed * Time.deltaTime * 10, curVelocity.y);
        }    
    }
}


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
