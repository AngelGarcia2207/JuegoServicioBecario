using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicIntroSource;
    [SerializeField] AudioSource musicLoopSource;
    [SerializeField] AudioSource SFXSource;

    public int currentLevel;

    public AudioClip[] background_I;
    public AudioClip[] background_L;

    /*
    public AudioClip background1_L;
    public AudioClip background2_I;
    public AudioClip background2_L;
    public AudioClip background3_I;
    public AudioClip background3_L;
    public AudioClip background4_I;
    public AudioClip background4_L;
    public AudioClip background5_I;
    public AudioClip background5_L;
    public AudioClip background5_I;
    public AudioClip background5_L;
    public AudioClip background5_I;*/
    
    public AudioClip jump;
    public AudioClip land;

    public void PlayNormalMusic()
    {
        musicIntroSource.clip = background_I[currentLevel - 1];
        musicIntroSource.Play();
        musicLoopSource.clip = background_L[currentLevel - 1];
        musicLoopSource.PlayDelayed(musicIntroSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
