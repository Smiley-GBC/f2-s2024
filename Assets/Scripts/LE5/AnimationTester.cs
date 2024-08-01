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
    SpriteAnimation walkLeft = new SpriteAnimation();
    SpriteAnimation walkRight = new SpriteAnimation();
    SpriteAnimation walkUp = new SpriteAnimation();
    SpriteAnimation walkDown = new SpriteAnimation();

    void Start()
    {
        walkLeft.renderer = GetComponent<SpriteRenderer>();
        walkRight.renderer = GetComponent<SpriteRenderer>();
        walkUp.renderer = GetComponent<SpriteRenderer>();
        walkDown.renderer = GetComponent<SpriteRenderer>();

        // Time (seconds) per frame!
        walkLeft.frameTime.total = 0.25f;
        walkRight.frameTime.total = 0.25f;
        walkUp.frameTime.total = 0.25f;
        walkDown.frameTime.total = 0.25f;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/characters");

        walkLeft.frames.Add(sprites[13]);
        walkLeft.frames.Add(sprites[12]);
        walkLeft.frames.Add(sprites[13]);
        walkLeft.frames.Add(sprites[14]);

        walkRight.frames.Add(sprites[25]);
        walkRight.frames.Add(sprites[24]);
        walkRight.frames.Add(sprites[25]);
        walkRight.frames.Add(sprites[26]);

        walkUp.frames.Add(sprites[37]);
        walkUp.frames.Add(sprites[36]);
        walkUp.frames.Add(sprites[37]);
        walkUp.frames.Add(sprites[38]);

        walkDown.frames.Add(sprites[1]);
        walkDown.frames.Add(sprites[0]);
        walkDown.frames.Add(sprites[1]);
        walkDown.frames.Add(sprites[2]);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            walkUp.Update(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            walkDown.Update(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            walkLeft.Update(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            walkRight.Update(Time.deltaTime);
        }
    }
}
