using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    Rigidbody2D rb;

    float thrust = 10.0f;
    float turnSpeed = 360.0f;   // 1 revolution per second
    const float moveSpeedMax = 10.0f;
    const float bulletSpeed = 15.0f;

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

        // Rotate
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(thrust * 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-thrust * 0.1f);
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + direction;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            Destroy(bullet, 1.0f);

            AudioManager.PlaySound(SoundName.FIRE);
        }

        // Limit our movement speed (linear velocity) to a maximum
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeedMax);

        // Wrap ship
        GameUtilities.Wrap(gameObject);

        Debug.DrawLine(transform.position, transform.position + direction * 5.0f, Color.red);
    }
}
