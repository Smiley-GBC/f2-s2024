using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    CircleCollider2D collider;

    // TODO -- make collider smaller when a bullet hits an asteroid
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        GameUtilities.Wrap(gameObject);
    }

    // TODO -- handle player-asteroid collision & bullet-asteroid collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name + " colliding with " + collision.name);
    }
}
