using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    const float moveForce = 10.0f;
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


    }
}
