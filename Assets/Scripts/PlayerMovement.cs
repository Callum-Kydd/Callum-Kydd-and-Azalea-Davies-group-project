using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool canJump;
    public float jump_force;
    public float speed;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }

    void Update()
    {
        
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");    

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            canJump = false;
        }
    }


        void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        transform.Translate(movement * speed * Time.deltaTime);
    }
   
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
