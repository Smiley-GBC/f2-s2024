using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    float thrust = 10.0f;
    float turnSpeed = 360.0f;   // 1 revolution per second
    const float moveSpeedMax = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Translate
        Vector3 direction = transform.right;
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(direction * thrust);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-direction * thrust);
        }

        // Limit our movement speed (linear velocity) to a maximum
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeedMax);

        // Rotate
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, 0.0f, turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, 0.0f, -turnSpeed * Time.deltaTime);
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        Debug.DrawLine(transform.position, transform.position + direction * 5.0f, Color.red);
    }
}
