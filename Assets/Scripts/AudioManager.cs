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

    private void Load()
    {
        soundClips[(int)SoundName.BOOM] = Resources.Load<AudioClip>("boom");
        musicClips[(int)MusicName.MASK] = Resources.Load<AudioClip>("MASK");

        sound = gameObject.AddComponent<AudioSource>();
        music = gameObject.AddComponent<AudioSource>();

        sound.clip = soundClips[(int)SoundName.BOOM];
        music.clip = musicClips[(int)MusicName.MASK];

        sound.Play();
        music.Play();
    }
}
