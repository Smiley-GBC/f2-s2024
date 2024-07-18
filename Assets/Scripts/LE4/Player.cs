using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    Rigidbody2D rb;

    // Horizontal movement
    const float moveForce = 5.0f;

    // Vertical movement
    const float jumpForce = 15.0f;
    const int jumpCount = 2;
    int jumps = 0;

    // Limits
    const float xSpeedMax = 10.0f;
    const float ySpeedMax = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // TODO -- see if changing velocity directly prevents sticking.
        // (Plus I can probably get manual drag control back by changing velocity directly)!
        // Maybe the way forward is to use dynamic bodies, but only apply velocity (no force/impulse).
        rb.AddForce(Vector2.right * moveForce * Input.GetAxisRaw("Horizontal"));
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            jumps--;
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        float xMax = Mathf.Clamp(rb.velocity.x, -xSpeedMax, xSpeedMax);
        float yMax = Mathf.Clamp(rb.velocity.y, -ySpeedMax, ySpeedMax);
        rb.velocity = new Vector2(xMax, yMax);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        jumps = jumpCount;
    }
}
