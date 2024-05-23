using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Vector2 A;
    public Vector2 B;

    public Transform[] waypoints;
    int next = 0;

    void Start()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Debug.Log(waypoints[i].position);
        }
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position,
        //    waypoints[1].position, 5.0f * Time.deltaTime);

        float tt = Time.realtimeSinceStartup;
        float t = Mathf.Cos(tt) * 0.5f + 0.5f;
        Debug.Log(t);
        transform.position = Vector2.Lerp(A, B, t);

        //Vector2.Lerp()
        // We want to move between waypoints in a line.
        // How do we move in a line???
    }
}
