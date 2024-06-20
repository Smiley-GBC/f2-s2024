using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static void Wrap(GameObject go)
    {
        CircleCollider2D collider = go.GetComponent<CircleCollider2D>();
        float size = Camera.main.orthographicSize;
        float aspect = Camera.main.aspect;
        float xMin = -size * aspect;
        float xMax = size * aspect;
        float yMin = -size;
        float yMax = size;
        float radius = collider.radius;

        if (go.transform.position.x < xMin)
        {
            go.transform.position = new Vector3(xMax - radius, go.transform.position.y);
        }
        if (go.transform.position.x > xMax)
        {
            go.transform.position = new Vector3(xMin + radius, go.transform.position.y);
        }
        if (go.transform.position.y < yMin)
        {
            go.transform.position = new Vector3(go.transform.position.x, yMax - radius);
        }
        if (go.transform.position.y > yMax)
        {
            go.transform.position = new Vector3(go.transform.position.x, yMin + radius);
        }
    }
}
