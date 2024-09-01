using System;
using UnityEngine;

public class SoundView : MonoBehaviour
{
    public AudioSource musicSource, sfxSource, driveSourcePlayer, driveSourceAI;

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDriveSource(AudioSource source)
    {
        if (source == null) return;
        source.Play();
    }
}
