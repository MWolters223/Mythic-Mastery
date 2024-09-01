using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SceneTransitionController : MonoBehaviour
{
    private SceneModel model;
    private SceneView view;
    private SceneAnimationController sceneAnimationController;
    private SceneLoadingController sceneLoadingController;

    public void Initialize(SceneModel model, SceneView view, SceneAnimationController sceneAnimationController, SceneLoadingController sceneLoadingController)
    {
        this.model = model;
        this.view = view;
        this.sceneAnimationController = sceneAnimationController;
        this.sceneLoadingController = sceneLoadingController;
    }

    public void LoadNextScene(int sceneBuilderIndex)
    {
        StartCoroutine(LoadNextSceneCoroutine(sceneBuilderIndex));
    }

    private IEnumerator LoadNextSceneCoroutine(int sceneBuilderIndex)
    {
        // Handle audio transition sounds and fade out current music
        HandleSceneTransitionSound(sceneBuilderIndex);

        // Perform scene loading based on scene type
        if (model.IsMenuScene(sceneBuilderIndex))   // Load menu
        {
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithAnimation(sceneBuilderIndex));
            PlayBackgroundMusic("Thema"); // Play menu music
        }
        else if (model.IsTransitionScene(sceneBuilderIndex)) // Transition to first level
        {
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithTransition(model.TransistionSceneIndex, model.FirstLevelIndex));
            PlayBackgroundMusic("Battle Muziek"); // Music in level
        }
        else if (model.IsLevelChange(sceneBuilderIndex)) // Level change animation 
        {
            StopBackgroundMusic();
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithTransition(model.TransistionSceneIndex, sceneBuilderIndex));
            PlayBackgroundMusic("Battle Muziek"); // Music in level
        }
        else if (model.IsScoreBoard(sceneBuilderIndex)) // Change to scoreboard
        {
            StopBackgroundMusic();
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithAnimation(model.ScoreBoardIndex));
        }
        else
        {
            yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation()); // Open door to avoid bugs
        }
    }

    private void HandleSceneTransitionSound(int sceneBuilderIndex)
    {
        if (model.IsTransitionScene(sceneBuilderIndex) || model.IsLevelChange(sceneBuilderIndex))
        {
            SoundController.Instance.SoundSFXController.PlaySFX("Knop klik");
            view.SetMusicTrigger("Muziek fade out");
        }
    }

    private void PlayBackgroundMusic(string musicTrack)
    {
        if (!SoundController.Instance.SoundMusicController.IsMusicPlaying(musicTrack))
        {
            SoundController.Instance.SoundMusicController.StopMusic();
            view.SetMusicTrigger("Muziek fade in");
            SoundController.Instance.SoundMusicController.PlayMusic(musicTrack);
        }
    }

    private void StopBackgroundMusic()
    {
        // Trigger fade out and stop the music
        view.SetMusicTrigger("Muziek fade out");
        SoundController.Instance.SoundMusicController.StopMusic();
    }
}
