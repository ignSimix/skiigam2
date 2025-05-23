using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private float acceleration = 100, turnSpeed = 100, minSpeed = 0, maxSPeed = 500, minAcceleration = -100, maxAcceleration = 200;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;
    [SerializeField] private float bounceForce = 10f;
    private float speed = 0;
    private Rigidbody rb;
    private Animator animator;
    private bool wasBounced = false;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!wasBounced)
        {
            float angle = Mathf.Abs(transform.eulerAngles.y - 100);
            acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
            speed += acceleration * Time.fixedDeltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSPeed);
            animator.SetFloat("playerSpeed", speed);
            Vector3 velocity = transform.forward * speed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        else
        {
            wasBounced = false; // Nākamajā kadra atļauj normālu kustību
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundTransform.position, groundLayers);

        if (isGrounded)
        {
            if (Input.GetKey(leftInput) && transform.eulerAngles.y <269)
            {
                transform.Rotate(new Vector3(0, turnSpeed * Time.fixedDeltaTime, 0), Space.Self);
            }
            if (Input.GetKey(rightInput) && transform.eulerAngles.y > 91)
            {
                transform.Rotate(new Vector3(0, -turnSpeed * Time.fixedDeltaTime, 0), Space.Self);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 bounceDirection = -transform.forward;
            bounceDirection.y = 0f;

            // Pielieto spēku atpakaļ
            rb.AddForce(bounceDirection.normalized * bounceForce, ForceMode.Impulse);

            speed *= 0.5f;
        }
    }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }
}
