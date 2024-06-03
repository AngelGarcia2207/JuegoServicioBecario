using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public MusicManager music;

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip talk;
    public AudioClip win;

    public void PlayWalk()
    {
        sfxSource.Stop();
        sfxSource.clip = walk;
        sfxSource.Play();
    }

    public void PlayWalkDelayed()
    {
        StartCoroutine(WalkDelayed());
    }

    public void PlayJump()
    {
        sfxSource.Stop();
        sfxSource.clip = jump;
        sfxSource.Play();
    }

    public void PlayLand()
    {
        sfxSource.Stop();
        sfxSource.clip = land;
        sfxSource.Play();
    }

    public void PlayTalk()
    {
        sfxSource.Stop();
        sfxSource.clip = talk;
        sfxSource.Play();
    }

    IEnumerator WalkDelayed()
    {
        yield return new WaitForSeconds(0.8f);
        sfxSource.Stop();
        sfxSource.clip = walk;
        sfxSource.Play();
    }

    public void PlayWin()
    {
        sfxSource.Stop();
        music.StopAll();
        sfxSource.clip = win;
        sfxSource.Play();
    }
}
