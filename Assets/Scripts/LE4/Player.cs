using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D box;

    const float groundDrag = 0.25f;
    const float airDrag = 0.5f;

    // Horizontal movement
    const float xMoveAcc = 15.0f;
    const float xSpeedMax = 10.0f;
    float xAcc = 0.0f;

    // Vertical movement
    const float yJumpSpeed = 10.0f;
    const int jumpCount = 2;
    int jumps = 0;
    bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        // Player: { 0, 0.5 }
        // Square: { 0, 0 }
    }

    void Update()
    {
        xAcc = xMoveAcc * Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, yJumpSpeed);
            jumps--;
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        float yAcc = grounded ? 0.0f : Physics2D.gravity.y;
        rb.velocity += new Vector2(xAcc, yAcc) * dt;

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -xSpeedMax, xSpeedMax), rb.velocity.y);
        rb.velocity *= Mathf.Pow(grounded ? groundDrag : airDrag, dt);

        RaycastHit2D hit = Physics2D.BoxCast(box.transform.position, box.size, 0.0f, transform.right);
        if (hit)
        {
            Vector2 mtv = hit.centroid - hit.point;
            rb.position += mtv.normalized * (box.size.x / 2.0f - hit.distance);
        }

        //RaycastHit2D[] hits = Physics2D.BoxCastAll(box.transform.position, box.size, 0.0f, transform.right);
        //foreach (RaycastHit2D hit in hits)
        //{
        //    float boxLength = (hit.point - hit.centroid).magnitude;
        //    float penDepth = hit.distance - boxLength;
        //    rb.position += hit.normal * penDepth;
        //}
    }

    // Stop horizontal motion if x-collision, stop vertical motion if y-collision. If corner collision, maintain motion and resolve position.
    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        // Vector = magnitude * direction
        // contact.normal = "direction", contact.separation = "magnitude"

        if (contact.normal.y == 0.0f)
        {
            // Horiztonal collision
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        else if (contact.normal.x == 0.0f)
        {
            // Vertical collision
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }
        else
        {
            // Don't do anything -- preserve motion & resolve position only in collision-stay.
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
        jumps = jumpCount;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }

    //void ResolveCollision(Collision2D collision)
    //{
    //    ContactPoint2D contact = collision.contacts[0];
    //    Vector2 mtv = contact.normal * Mathf.Abs(contact.separation);
    //    rb.position += mtv;
    //}
}
