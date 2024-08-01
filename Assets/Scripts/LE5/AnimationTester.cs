using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationTester : MonoBehaviour
{
    SpriteRenderer renderer;
    Timer animationTimer = new Timer();
    List<Sprite> animationFrames = new List<Sprite>();
    int frameNumber = 0;

    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/characters");
        renderer = GetComponent<SpriteRenderer>();

        animationFrames.Add(sprites[1]);
        animationFrames.Add(sprites[0]);
        animationFrames.Add(sprites[1]);
        animationFrames.Add(sprites[2]);

        // Time (seconds) per frame!
        animationTimer.total = 0.25f;
    }

    void Update()
    {
        renderer.sprite = animationFrames[frameNumber];
        if (animationTimer.Expired())
        {
            animationTimer.Reset();
            frameNumber++;
            frameNumber %= animationFrames.Count;
        }
        animationTimer.Tick(Time.deltaTime);
    }
}
