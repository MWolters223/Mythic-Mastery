using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneLoadingController : MonoBehaviour
{
    private SceneView view;
    private SceneAnimationController sceneAnimationController;

    public void Initialize(SceneView view, SceneAnimationController sceneAnimationController)
    {
        this.view = view;
        this.sceneAnimationController = sceneAnimationController;
    }

    public IEnumerator LoadSceneWithAnimation(int sceneIndex)
    {
        yield return StartCoroutine(sceneAnimationController.CloseDoorAnimation());
        yield return StartCoroutine(LoadAsync(sceneIndex));
        yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation());
    }

    public IEnumerator LoadSceneWithTransition(int transitionSceneIndex, int targetSceneIndex)
    {
        yield return StartCoroutine(sceneAnimationController.CloseDoorAnimation());
        yield return StartCoroutine(LoadAsync(transitionSceneIndex));
        yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation());
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(sceneAnimationController.CloseDoorAnimation());
        yield return StartCoroutine(LoadAsync(targetSceneIndex));
        yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation());
    }

    public IEnumerator LoadAsync(int levelIndex)
    {
        Debug.Log("Starting loading animation...");
        view.ShowLoadingPanel(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            view.UpdateLoadingBar(progress);
            Debug.Log($"Loading progress: {progress * 100}%");
            yield return null;
        }

        Debug.Log("Ending loading animation...");
        view.ShowLoadingPanel(false);
    }
}
