using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    public class SpriteAnimation
    {
        public SpriteRenderer renderer;
        public List<Sprite> sprites = new List<Sprite>();
        public Timer timer = new Timer();
        public int frameNumber = 0;

        public void Update(float dt)
        {
            renderer.sprite = sprites[frameNumber];
            if (timer.Expired())
            {
                timer.Reset();
                frameNumber++;
                frameNumber %= sprites.Count;
            }
            timer.Tick(dt);
        }
    }

    SpriteAnimation walk = new SpriteAnimation();

    void Start()
    {
        Sprite[] sp = Resources.LoadAll<Sprite>("Sprites/characters");
        walk.renderer = GetComponent<SpriteRenderer>();
        walk.timer.total = 0.25f;
        walk.sprites.Add(sp[1]);
        walk.sprites.Add(sp[0]);
        walk.sprites.Add(sp[1]);
        walk.sprites.Add(sp[2]);
    }

    void Update()
    {
        walk.Update(Time.deltaTime);
    }
}
