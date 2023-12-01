using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource, sfxSource;
    
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip failSound;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void PlayWinSound(float volume = 1f)
    {
        PlaySFX(winSound, volume);
    }
    public void PlayFailSound(float volume = 1f)
    {
        PlaySFX(failSound, volume);
    }
    public void PlayClick(float volume = 1f)
    {
        PlaySFX(clickSound, volume);
    }
    public void PlaySFX(AudioClip clip, float volume=1)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
    public void PlayBGMusic(AudioClip music) 
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    public void StopBGMusic()
    {
        musicSource.Stop();
    }
   
}
