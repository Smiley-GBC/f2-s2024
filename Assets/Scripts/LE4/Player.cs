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

    const int jumpCount = 2;
    int jumps = jumpCount;
    int levelLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelLayer = LayerMask.NameToLayer("Level");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * moveForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * moveForce);
        }
        
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            // Impulse = "immediate change", Force = "gradual change" -- velocity vs acceleration
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            jumps--;
        }

        // Don't need to clamp vertical velocity since jumping is an instantaneous change (jump force == max force)
        float vx = Mathf.Clamp(rb.velocity.x, -xSpeedMax, xSpeedMax);
        rb.velocity = new Vector2(vx, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == levelLayer)
        {
            jumps = jumpCount;
            rb.drag = collision.GetComponent<PhysicsData>().drag;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == levelLayer)
        {
            rb.drag = 0.0f;
        }
    }
}
