using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject target;
    public GameObject bulletPrefab;
    float speed = 10.0f;

    float current = 0.0f;
    float total = 0.25f;
    // Shoot every 0.25 seconds!

    void Update()
    {
        float dt = Time.deltaTime;
        current += dt;
        if (current > total && target != null)
        {
            current = 0.0f;
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;

            // AB = B - A
            Vector3 direction = (target.transform.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
