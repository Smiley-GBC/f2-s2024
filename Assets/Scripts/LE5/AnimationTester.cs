using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation
{
    public List<Sprite> frames = new List<Sprite>();
    public Timer frameTime = new Timer();
    public int frameNumber = 0;

    public void Update(SpriteRenderer renderer, float dt)
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
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    enum State
    {
        IDLE,
        WALK,
        RUN
    }

    Direction direction = Direction.DOWN;
    State state = State.IDLE;

    // Frame 0 of each direction is the idle frame
    SpriteAnimation walkLeft = new SpriteAnimation();
    SpriteAnimation walkRight = new SpriteAnimation();
    SpriteAnimation walkUp = new SpriteAnimation();
    SpriteAnimation walkDown = new SpriteAnimation();

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

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
            direction = Direction.UP;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = Direction.DOWN;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = Direction.LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Direction.RIGHT;
        }

        bool input =
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        state = input ? State.WALK : State.IDLE;

        if (state == State.IDLE)
        {
            OnIdle();
        }
        else
        {
            OnUpdate();
        }
    }

    void OnIdle()
    {
        Sprite sprite = null;
        switch (direction)
        {
            case Direction.LEFT:
                sprite = walkLeft.frames[0];
                break;

            case Direction.RIGHT:
                sprite = walkRight.frames[0];
                break;

            case Direction.UP:
                sprite = walkUp.frames[0];
                break;

            case Direction.DOWN:
                sprite = walkDown.frames[0];
                break;
        }
        renderer.sprite = sprite;
    }

    void OnUpdate()
    {
        float dt = Time.deltaTime;
        switch (direction)
        {
            case Direction.LEFT:
                walkLeft.Update(renderer, dt);
                break;

            case Direction.RIGHT:
                walkRight.Update(renderer, dt);
                break;

            case Direction.UP:
                walkUp.Update(renderer, dt);
                break;

            case Direction.DOWN:
                walkDown.Update(renderer, dt);
                break;
        }
    }
}
