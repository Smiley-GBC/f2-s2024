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
    SpriteAnimation[] animations = new SpriteAnimation[4];
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        // Time (seconds) per frame!
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i] = new SpriteAnimation();
            animations[i].frameTime.total = 0.25f;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/characters");

        animations[0].frames = CreateAnimationFrames(sprites, 12);
        animations[1].frames = CreateAnimationFrames(sprites, 24);
        animations[2].frames = CreateAnimationFrames(sprites, 36);
        animations[3].frames = CreateAnimationFrames(sprites, 0);
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
            state = Input.GetKey(KeyCode.LeftShift) ? State.RUN : State.WALK;
            float frameTime = state == State.RUN ? 0.05f : 0.25f;
            for (int i = 0; i < animations.Length; i++)
            {
                animations[i].frameTime.total = frameTime;
            }
            OnUpdate();
        }
    }

    List<Sprite> CreateAnimationFrames(Sprite[] sprites, int index)
    {
        List<Sprite> frames = new List<Sprite>();
        frames.Add(sprites[index + 1]);
        frames.Add(sprites[index + 0]);
        frames.Add(sprites[index + 1]);
        frames.Add(sprites[index + 2]);
        return frames;
    }

    void OnIdle()
    {
        renderer.sprite = animations[(int)direction].frames[0];
    }

    void OnUpdate()
    {
        animations[(int)direction].Update(renderer, Time.deltaTime);
    }
}
