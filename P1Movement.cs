using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Movement : MonoBehaviour
{

    public float velX, velY, velZ;
    public Rigidbody rb;
    public Vector3 pos;
    public float speed;
    public float gravity;
    public float xDir, zDir;
    public Collider col;
    public float jumpStall, dashStall; //variables for stalling between consecutive actions
    public bool canMove = true;
    public float forwardY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        gravity = -17.6f;
        rb.freezeRotation = true;
        jumpStall = 0;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 1.3f);
    }

// Update is called once per frame
void Update()
    {
        rb.AddForce(new Vector3(0, gravity, 0)); //applies gravity every frame
        Move();
        Dash();
        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
        }


        rb.velocity = new Vector3(velX, velY, velZ); //This must be called LAST in update, since it relies on other method calculations to complete

    }

    //The basic movement function. Inluced WASD and jump movements.
    void Move()
    {
        velY = rb.velocity.y;
        velX = 0;
        velZ = 0;
        if (Input.GetKey("w") && canMove)
        {
            velX += speed * transform.forward.x;
            velZ += speed * transform.forward.z;
        }
        if (Input.GetKey("s") && canMove)
        {
            velX -= speed * transform.forward.x;
            velZ -= speed * transform.forward.z;
        }
        if (Input.GetKey("d") && canMove)
        {
            velX += speed * transform.right.x;
            velZ += speed * transform.right.z;
        }
        if (Input.GetKey("a") && canMove)
        {
            velX -= speed * transform.right.x;
            velZ -= speed * transform.right.z;
        }
        jumpStall -= Time.deltaTime; //adds a delay so consecutive jumps aren't too rapid
        if (Input.GetKey("space") && IsGrounded() && jumpStall <= 0 && canMove)
        {
            velY += 9.8f;
            jumpStall = 0.1f;

        }
    }
    void Dash() {
        dashStall -= Time.deltaTime;
        if (Input.GetKey("left shift") && dashStall <= 0)
        {
            dashStall = 1f;
        }
        if (dashStall > 0.5f) {
            canMove = false;
            velX = speed * 5 * transform.forward.x;
            velZ = speed * 5 * transform.forward.z;
            velY = speed * 5 * forwardY;
        }
        if (dashStall <= 0.5f && canMove == false) {
            velY = 0;
            canMove = true;
        }
    }
}
