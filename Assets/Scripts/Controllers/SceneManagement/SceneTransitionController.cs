using System.Collections;
using UnityEngine;

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
        // Handle audio transition sounds for the scene
        HandleSceneTransitionSound(sceneBuilderIndex);

        // Perform scene loading based on scene type
        if (model.IsMenuScene(sceneBuilderIndex))   // Load menu
        {
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithAnimation(sceneBuilderIndex));
        }
        else if (model.IsTransitionScene(sceneBuilderIndex)) // Transition to first level
        {
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithTransition(model.TransistionSceneIndex, model.FirstLevelIndex));

            AudioManager.instance.PlayMusic("Battle Muziek"); // Music in level
            view.SetMusicTrigger("Muziek fade in");
        }
        else if (model.IsLevelChange(sceneBuilderIndex)) // Level change animation
        {
            yield return StartCoroutine(sceneLoadingController.LoadSceneWithTransition(model.TransistionSceneIndex, sceneBuilderIndex));
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
            AudioManager.instance.PlaySFX("Knop klik");
            view.SetMusicTrigger("Muziek fade out");
        }
    }
}
