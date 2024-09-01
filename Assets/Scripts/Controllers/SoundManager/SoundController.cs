using System;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    private SoundModel model;
    private SoundView view;

    private SoundDriveController soundDriveController;
    private SoundMusicController soundMusicController;
    private SoundSFXController soundSFXController;

    public static SoundController Instance { get; private set; }

    //  Getters to make controllers accesible from instance
    public SoundView View => view;
    public SoundDriveController SoundDriveController => soundDriveController;
    public SoundMusicController SoundMusicController => soundMusicController;
    public SoundSFXController SoundSFXController => soundSFXController;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        model = GetComponent<SoundModel>();
        view = GetComponent<SoundView>();

        soundDriveController = gameObject.AddComponent<SoundDriveController>();
        soundDriveController.Initialize(model, view);

        soundMusicController = gameObject.AddComponent<SoundMusicController>();
        soundMusicController.Initialize(model, view);

        soundSFXController = gameObject.AddComponent<SoundSFXController>();
        soundSFXController.Initialize(model, view);
    }
}
