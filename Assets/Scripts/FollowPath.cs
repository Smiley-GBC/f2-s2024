using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    float currentTime = 0.0f;
    float totalTime = 1.0f;

    public Transform[] waypoints;
    int currentWaypoint = 0;
    int nextWaypoint = 1;

    void Update()
    {
        float dt = Time.deltaTime;
        currentTime += dt;
        if (currentTime > totalTime)
        {
            currentTime = 0.0f;
            currentWaypoint++;
            nextWaypoint++;
        }

        float tt = Time.realtimeSinceStartup;
        float t = currentTime;//Mathf.Cos(tt) * 0.5f + 0.5f;

        Vector2 A = waypoints[currentWaypoint].position;
        Vector2 B = waypoints[nextWaypoint].position;
        transform.position = Vector2.Lerp(A, B, t);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the enemy if its reached the last waypoint!
        if (collision.name == waypoints[waypoints.Length - 1].name)
        {
            Destroy(gameObject);
        }
    }
}
