using System;
using UnityEngine;

public class SoundMusicController : MonoBehaviour
{
    private string currentMusicName;

    private SoundModel soundModel;
    private SoundView soundView;

    public void Initialize(SoundModel model, SoundView view)
    {
        this.soundModel = model;
        this.soundView = view;
        PlayMusic("Thema");
    }

    public void PlayMusic(string name)
    {
        if (currentMusicName == name && soundView.musicSource.isPlaying)
        {
            return; // Music is already playing
        }

        Sound s = Array.Find(soundModel.musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found: " + name);
        }
        else
        {
            soundView.PlayMusic(s.clip);
            currentMusicName = name;
        }
    }

    public void StopMusic()
    {
        if (soundView.musicSource.isPlaying)
        {
            soundView.StopMusic();
            currentMusicName = null;
        }
    }

    public bool IsMusicPlaying(string musicName)
    {
        return currentMusicName == musicName && soundView.musicSource.isPlaying;
    }
}
