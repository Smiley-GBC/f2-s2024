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
    bool grounded = false;

    // Limits
    const float xSpeedMax = 10.0f;
    const float ySpeedMax = 10.0f;
    const float dragAir = 0.25f;    // Decelerate slower in air
    const float dragGround = 0.05f; // Decelerate faster on ground
    // TODO -- Add more nuanced drag based on direction of motion.
    // For example, decrease air drag if falling down!

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
        float dy = 0.0f; // <-- no custom vertical acceleration.
        float xMax = Mathf.Clamp(rb.velocity.x + dx, -xSpeedMax, xSpeedMax);
        float yMax = Mathf.Clamp(rb.velocity.y + dy, -ySpeedMax, ySpeedMax);
        float drag = grounded ? dragGround : dragAir;
        rb.velocity = new Vector2(xMax, yMax);
        rb.velocity *= Mathf.Pow(drag, dt);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        jumps = jumpCount;
        grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
