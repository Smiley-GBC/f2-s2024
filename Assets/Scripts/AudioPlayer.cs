using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    public Button boom;
    public Button death;

    public Button mask;

    void PlayBoom()
    {
        AudioManager.Instance.PlaySound(SoundName.BOOM);
    }

    void PlayDeath()
    {
        AudioManager.Instance.PlaySound(SoundName.DEATH);
    }

    void PlayMask()
    {
        AudioManager.Instance.PlayMusic(MusicName.MASK);
    }

    void Start()
    {
        boom.onClick.AddListener(PlayBoom);
        death.onClick.AddListener(PlayDeath);

        mask.onClick.AddListener(PlayMask);
    }
}
