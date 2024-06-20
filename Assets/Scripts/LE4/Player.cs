using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    const float moveForce = 15.0f;
    const float jumpForce = 10.0f;

    // Future improvement: air-speed vs ground-speed
    const float xSpeedMax = 10.0f;
    const float ySpeedMax = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * moveForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * moveForce);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Impulse = "immediate change", Force = "gradual change" -- velocity vs acceleration
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        float vx = Mathf.Clamp(rb.velocity.x, -xSpeedMax, xSpeedMax);
        rb.velocity = new Vector2(vx, rb.velocity.y);

        // Don't need to clamp vertical velocity since jumping is an instantaneous change (jump force == max force)
    }
}
