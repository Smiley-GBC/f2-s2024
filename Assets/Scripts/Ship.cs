using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    Rigidbody2D rb;

    const float moveForce = 10.0f;
    const float turnForce = 1.0f;

    const float moveSpeedMax = 10.0f;
    const float turnSpeedMax = 360.0f;// 1 revolution per second

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
            rb.AddForce(direction * moveForce);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-direction * moveForce);
        }

        // Rotate
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(turnForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-turnForce);
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

        // TODO -- make a teleport function that randomly teleports the player, but ensures it doesn't hit an asteroid
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Returns all Asteroid components
            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
            for (int i = 0; i < asteroids.Length; i++)
            {
                // Access individual asteroids within loop:
                //GameObject asteroidGO = asteroids[i].gameObject;
                //Debug.Log(asteroidGO.transform.position);
                // (You'll want to hit-test every asteroid within this loop, maybe using Physics2D.OverlapCircle)
            }
        }

        // Limit linear velocity (position rate change) & angular velocity (rotation rate of change) to a maximum
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeedMax);
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -turnSpeedMax, turnSpeedMax);

        // Wrap ship
        GameUtilities.Wrap(gameObject);

        Debug.DrawLine(transform.position, transform.position + direction * 5.0f, Color.red);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name + " colliding with " + collision.name);
    }
}
