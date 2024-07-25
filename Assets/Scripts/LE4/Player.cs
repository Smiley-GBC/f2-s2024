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
    const float dragBounce = 0.95f; // Decelerate very slow on bounce!
    float drag = dragGround;
    // TODO -- Add more nuanced drag based on direction of motion.
    // For example, decrease air drag if falling down!

    [SerializeField]
    Transform spawn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Respawn();
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

        if (transform.position.y < 0.0f)
            Respawn();
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        float dx = xAcc * dt;
        float dy = 0.0f; // <-- no custom vertical acceleration.
        float xMax = Mathf.Clamp(rb.velocity.x + dx, -xSpeedMax, xSpeedMax);
        float yMax = Mathf.Clamp(rb.velocity.y + dy, -ySpeedMax, ySpeedMax);
        rb.velocity = new Vector2(xMax, yMax);
        rb.velocity *= Mathf.Pow(drag, dt);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            OnGrounded();
            drag = dragGround;
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            spawn.position = collision.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            OnAirborne();
            drag = dragAir;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllBounce"))
        {
            OnGrounded();
            drag = dragBounce;

            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.y) >= 0.9f)
            {
                // Vertical collision
                float dir = Mathf.Sign(normal.y);
                rb.velocity = new Vector2(rb.velocity.x, dir * ySpeedMax);
            }
            else if (Mathf.Abs(normal.x) >= 0.9f)
            {
                // Horizontal collision
                float dir = Mathf.Sign(normal.x);
                rb.velocity = new Vector2(dir * xSpeedMax, rb.velocity.y);
            }

            // TODO -- see if we can make this prettier with math ;)
            //rb.velocity = Vector2.Reflect(rb.velocity, normal * -1.0f) * 10.0f;
        }
        else if (collision.gameObject.CompareTag("TopBounce"))
        {
            OnGrounded();
            drag = dragBounce;

            Vector2 normal = collision.contacts[0].normal;
            if (normal.y >= 0.9f)
            {
                rb.velocity = new Vector2(rb.velocity.x, ySpeedMax);
            }
        }
    }

    void Respawn()
    {
        transform.position = spawn.position;
        rb.velocity = Vector2.zero;
    }

    void OnGrounded()
    {
        grounded = true;
        jumps = jumpCount;
    }

    void OnAirborne()
    {
        grounded = false;
    }
}
