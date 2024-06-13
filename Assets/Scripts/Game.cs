using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for cross-communication between systems -- Ship, Bullet, Asteroid, etc.
public class Game : MonoBehaviour
{
    Timer timer = new Timer();

    void Start()
    {
        timer.total = 0.25f;
        //AudioManager.PlayMusic(MusicName.WINGS);
    }

    void Update()
    {
        // Run the test function every 0.25 seconds!
        timer.Tick(Time.deltaTime);
        if (timer.Expired())
        {
            timer.Reset();
            //Test();
        }
    }

    void Test()
    {
        Debug.Log("Test");
    }
}
