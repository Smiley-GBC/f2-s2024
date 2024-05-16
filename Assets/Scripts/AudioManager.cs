using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    BOOM,
    DEATH,
    JUMP,
    LASER,

    COUNT
}

public enum MusicName
{
    MASK,
    THUNDERCATS,
    TMNT,

    COUNT
}

// Unity-style singleton so we can integrate with Inspector
public class AudioManager : MonoBehaviour
{
    // "static" means only a single instance of said object can exist
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    // Awake runs once when the game starts. Its where we initialize our audio manager!
    private void Awake()
    {
        // Destroy the singleton if there's more than 1
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // "this" refers to the instance of the object (AudioManager)
        instance = this;

        // Prevent object from being destroyed when switching scenes
        DontDestroyOnLoad(gameObject);

        // Load our sound & music clips!
        Load();
    }

    AudioClip[] soundClips = new AudioClip[(int)SoundName.COUNT];
    AudioClip[] musicClips = new AudioClip[(int)MusicName.COUNT];
    AudioSource sound;
    AudioSource music;

    // Use this to handle volume & other lab 1 tasks!
    [Range(0.0f, 1.0f)]
    public float testSlider;

    private void Load()
    {
        soundClips[(int)SoundName.BOOM] = Resources.Load<AudioClip>("boom");
        soundClips[(int)SoundName.DEATH] = Resources.Load<AudioClip>("death");
        soundClips[(int)SoundName.JUMP] = Resources.Load<AudioClip>("jump");
        soundClips[(int)SoundName.LASER] = Resources.Load<AudioClip>("laser");

        musicClips[(int)MusicName.MASK] = Resources.Load<AudioClip>("MASK");
        musicClips[(int)MusicName.THUNDERCATS] = Resources.Load<AudioClip>("Thundercats");
        musicClips[(int)MusicName.TMNT] = Resources.Load<AudioClip>("Turtles");

        sound = gameObject.AddComponent<AudioSource>();
        music = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(SoundName soundId)
    {
        sound.clip = soundClips[(int)soundId];
        sound.Play();
    }

    public void PlayMusic(MusicName musicId)
    {
        music.clip = musicClips[(int)musicId];
        music.Play();
    }
}
