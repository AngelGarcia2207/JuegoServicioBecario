using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicIntroSource;
    [SerializeField] AudioSource musicLoopSource;

    public int currentLevel;

    public AudioClip[] background_I;
    public AudioClip[] background_L;

    void Start()
    {
        PlayNormalMusic();
    }

    public void PlayNormalMusic()
    {
        musicIntroSource.clip = background_I[currentLevel - 1];
        musicIntroSource.Play();
        musicLoopSource.clip = background_L[currentLevel - 1];
        musicLoopSource.PlayDelayed(musicIntroSource.clip.length);
    }
}