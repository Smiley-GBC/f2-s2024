using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    const float moveForce = 15.0f;
    const float jumpForce = 10.0f;
    const float xSpeedMax = 10.0f;

    const int jumpCount = 2;
    int jumps = jumpCount;
    int levelLayer;

    Vector2 previousVelocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelLayer = LayerMask.NameToLayer("Level");
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(moveInput * moveForce, 0));

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Clear vertical velocity before jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumps--;
        }
    }

    void FixedUpdate()
    {
        float clampedVelocityX = Mathf.Clamp(rb.velocity.x, -xSpeedMax, xSpeedMax);
        rb.velocity = new Vector2(clampedVelocityX, rb.velocity.y);
        previousVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //OnCollision(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision);
    }

    // This is pretty jank... should probably switch to kinematic rigidbody but at that point why even use Unity Physics??
    void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.layer == levelLayer)
        {
            if (XCollision(collision))
            {
                rb.drag = 0.0f;
                rb.totalForce = new Vector2(0.0f, rb.totalForce.y);
                rb.velocity = new Vector2(rb.velocity.x, previousVelocity.y);
            }
            else
            {
                rb.drag = collision.gameObject.GetComponent<PhysicsData>().drag;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // No drag if airborne
        rb.drag = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == levelLayer)
        {
            // Reset jumps if grounded
            jumps = jumpCount;
        }
    }

    bool XCollision(Collision2D collision)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            // Side collision if MTV is horizontal (no vertical component)
            if (contact.normal.y == 0)
            {
                return true;
            }
        }
        return false;
    }

    bool YCollision(Collision2D collision)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            // Side collision if MTV is horizontal (no vertical component)
            if (contact.normal.x == 0)
            {
                return true;
            }
        }
        return false;
    }
}
