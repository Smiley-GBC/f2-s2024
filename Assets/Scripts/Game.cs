using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for cross-communication between systems -- Ship, Bullet, Asteroid, etc.
public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject asteroidPrefab;

    Timer asteroidTimer = new Timer();

    void Start()
    {
        asteroidTimer.total = 1.5f;
        //AudioManager.PlayMusic(MusicName.WINGS);
    }

    void Update()
    {
        // Run the test function every 0.25 seconds!
        asteroidTimer.Tick(Time.deltaTime);
        if (asteroidTimer.Expired())
        {
            asteroidTimer.Reset();
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        GameObject asteroid = Instantiate(asteroidPrefab);

        float speed = 10.0f;
        Vector3 position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
        Vector2 velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));

        asteroid.transform.position = position;
        asteroid.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
