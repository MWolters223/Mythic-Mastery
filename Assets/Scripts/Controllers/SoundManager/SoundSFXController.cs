using System;
using UnityEngine;

public class SoundSFXController : MonoBehaviour
{ 
    private SoundModel soundModel;
    private SoundView soundView;

    public void Initialize(SoundModel model, SoundView view)
    {
        this.soundModel = model;
        this.soundView = view;
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(soundModel.sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX not found: " + name);
        }
        else
        {
            soundView.PlaySFX(s.clip);
        }
    }
}
