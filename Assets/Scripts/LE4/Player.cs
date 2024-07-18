using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    Rigidbody2D rb;

    // Horizontal movement
    const float moveForce = 15.0f;
    float xAcc = 0.0f;

    // Vertical movement
    const float jumpForce = 15.0f;

    const int jumpCount = 2;
    int jumps = 0;

    // Limits
    const float xSpeedMax = 10.0f;
    const float ySpeedMax = 10.0f;

    // TODO -- Add drag for something to do in class?
    // Still not sure why Player slows down on its own...
    // Try making a physics material with no friction and seeing what happens!

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xAcc = moveForce * Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            jumps--;
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        float dx = xAcc * dt;
        float dy = 0.0f; // <-- in case we want custom vertical acceleration.
        // Best to let Unity Rigidbody2D physics handle vertical acceleration for now. 
        //float dy = Physics2D.gravity.y * rb.gravityScale;
        float xMax = Mathf.Clamp(rb.velocity.x + dx, -xSpeedMax, xSpeedMax);
        float yMax = Mathf.Clamp(rb.velocity.y + dy, -ySpeedMax, ySpeedMax);
        rb.velocity = new Vector2(xMax, yMax);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        jumps = jumpCount;
    }
}
