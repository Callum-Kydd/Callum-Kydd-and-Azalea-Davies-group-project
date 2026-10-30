using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool canJump;
    private bool inSlingshot;
    private bool firing;
    public float jump_force;
    public float speed;
    public float slingshot_force;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public Transform orientation;
    private Vector3 forward;
    private Vector3 right;
    public GameObject slingshot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        canJump = true;
        inSlingshot = false;
        firing = false;
    }

    void Update()
    {
        if (!inSlingshot) {
            movementX = Input.GetAxis("Horizontal");
            movementY = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
                canJump = false;
            }
        }
        else if (inSlingshot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(forward * slingshot_force);
                rb.useGravity = true;
                inSlingshot = false;
                firing = true;
            }
        }
    }


    void FixedUpdate()
    {
        forward = orientation.forward;
        right = orientation.right;
        if (!inSlingshot)
        {
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
        }
        Vector3 movement = (forward * movementY) + (right * movementX);
        if (!firing)
        {
            transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            rb.AddForce(movement * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            firing = false;
        }

        if (collision.gameObject.CompareTag("Slingshot"))
        {
            canJump = false;
            inSlingshot = true;
            movementX = 0f;
            movementY = 0f;
            transform.position = slingshot.transform.position + new Vector3(-1.75f, 1.5f, 0f);
            firing = false;
            rb.useGravity = false;
        }

        if (collision.gameObject.CompareTag("Enemy Platform"))
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
        }
    }
}
