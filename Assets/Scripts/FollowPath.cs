using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    bool followPath = true;
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

        Debug.Log(currentWaypoint);
        Debug.Log(nextWaypoint);

        float tt = Time.realtimeSinceStartup;
        float t = currentTime;//Mathf.Cos(tt) * 0.5f + 0.5f;

        Vector2 A = waypoints[currentWaypoint].position;
        Vector2 B = waypoints[nextWaypoint].position;
        transform.position = Vector2.Lerp(A, B, t);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "End")
        {
            Destroy(gameObject);
        }
    }
}
