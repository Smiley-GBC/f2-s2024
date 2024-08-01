using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation
{
    public SpriteRenderer renderer;

    public List<Sprite> frames = new List<Sprite>();
    public Timer frameTime = new Timer();
    public int frameNumber = 0;

    public void Update(float dt)
    {
        renderer.sprite = frames[frameNumber];
        if (frameTime.Expired())
        {
            frameTime.Reset();
            frameNumber++;
            frameNumber %= frames.Count;
        }
        frameTime.Tick(dt);
    }
}

public class AnimationTester : MonoBehaviour
{
    SpriteAnimation walkDown = new SpriteAnimation();

    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/characters");
        walkDown.renderer = GetComponent<SpriteRenderer>();

        walkDown.frames.Add(sprites[1]);
        walkDown.frames.Add(sprites[0]);
        walkDown.frames.Add(sprites[1]);
        walkDown.frames.Add(sprites[2]);

        // Time (seconds) per frame!
        walkDown.frameTime.total = 0.25f;
    }

    void Update()
    {
        walkDown.Update(Time.deltaTime);
    }
}
