using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds, DriveSoundPlayer, DriveSoundAI;
    public AudioSource musicSource, sfxSource, DriveSourcePlayer, DriveSourceAI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Thema");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Geluid niet gevonden");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
        
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sfx geluid niet gevonden");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayDrivePlayer(string name)
    {
        Sound s = Array.Find(DriveSoundPlayer, x => x.name == name);

        if (s == null)
        {
            Debug.Log("player drive geluid niet gevonden");
        }
        else
        {
            DriveSourcePlayer.Play();
        }
    }

    public void PlayDriveAI(string name)
    {
        Sound s = Array.Find(DriveSoundAI, x => x.name == name);

        if (s == null)
        {
            Debug.Log("AI drive geluid niet gevonden");
        }
        else
        {
            DriveSourceAI.Play();
        }
    }

}
