using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

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
    public ScoreSystem scoreScript;
    public LevelSystem levelScript;
    public float collisionCooldown = 0.5f;
    private float collisionTimer = 0f;
    private bool collisionsEnabled = true;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
        inSlingshot = false;
        firing = false;
    }

    void Update()
    {
        // basic movement, given that the player is not inside the slingshot
        if (!inSlingshot) {
            movementX = Input.GetAxis("Horizontal");
            movementY = Input.GetAxis("Vertical");

            // jumping, requiring the player to be on the ground
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
                canJump = false;
            }
        }
        else if (inSlingshot)
        {
            // firing from the slingshot
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(forward * slingshot_force);
                rb.useGravity = true;
                inSlingshot = false;
                firing = true;
                collisionsEnabled = false; // to ensure the player doesn't technically re-enter the slingshot while firing
                collisionTimer = collisionCooldown;
                levelScript.shotsFired++;
            }
        }

        if (!collisionsEnabled)
        {
            collisionTimer -= Time.deltaTime;

            if (collisionTimer <= 0f)
            {
                collisionsEnabled = true;
            }
        }

        
    }


    void FixedUpdate()
    {
        // calibrate movement directions based on camera angle
        forward = orientation.forward;
        right = orientation.right;

        // disable vertical movement angling, unless in slingshot
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
            // move at a consistent speed
            transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            // if firing, move by adding force (more realistic midair movement)
            rb.AddForce(movement * speed);
        }

        // begin the fail timer if 3 shots have been completed
        if (levelScript.shotsFired == 3 && !firing)
        {
            levelScript.incrementFailTimer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; // re-allow jumping
            firing = false; // end any active slingshot shots
            levelScript.incrementFailTimer = true; // allow the fail timer to increment

            // reset movement
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // entering slingshot, if less than 3 shots have been fired
        if (collision.gameObject.CompareTag("Slingshot") && collisionsEnabled && levelScript.shotsFired < 3)
        {
            // keep the player still
            canJump = false;
            inSlingshot = true;
            movementX = 0f;
            movementY = 0f;
            rb.useGravity = false;
            transform.position = slingshot.transform.position + new Vector3(-1.75f, 1.5f, 0f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            firing = false; // end any active slingshot shots
        }

        if (collision.gameObject.CompareTag("Enemy Platform"))
        {
            // teleport the player back to the middle if they touch the enemy platform (this also applies to the outer invisible walls)
            transform.position = new Vector3(0f, 0.5f, 0f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
