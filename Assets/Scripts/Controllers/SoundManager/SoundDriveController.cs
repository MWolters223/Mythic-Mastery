using System;
using UnityEngine;

public class SoundDriveController : MonoBehaviour
{  
    private SoundModel soundModel;
    private SoundView soundView;

    public void Initialize(SoundModel model, SoundView view)
    {
        this.soundModel = model;
        this.soundView = view;
    }


    public void PlayDrivePlayer(string name)
    {
        Sound s = Array.Find(soundModel.driveSoundPlayer, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Player drive sound not found: " + name);
        }
        else
        {
            soundView.PlayDriveSource(soundView.driveSourcePlayer);
        }
    }

    public void PlayDriveAI(string name)
    {
        Sound s = Array.Find(soundModel.driveSoundAI, x => x.name == name);

        if (s == null)
        {
            Debug.Log("AI drive sound not found: " + name);
        }
        else
        {
            soundView.PlayDriveSource(soundView.driveSourceAI);
        }
    }
}
