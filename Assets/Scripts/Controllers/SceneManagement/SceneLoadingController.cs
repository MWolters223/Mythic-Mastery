using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

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
        string currentLevel = SceneManager.GetActiveScene().name;

        yield return StartCoroutine(sceneAnimationController.CloseDoorAnimation());
        yield return StartCoroutine(LoadAsync(transitionSceneIndex));

        IncrementLevelNumberText(currentLevel);

        yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation());
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(sceneAnimationController.CloseDoorAnimation());
        yield return StartCoroutine(LoadAsync(targetSceneIndex));
        yield return StartCoroutine(sceneAnimationController.OpenDoorAnimation());
    }

    private void IncrementLevelNumberText(string currentLevel)
    {
        int levelNumber = 0; // Default to 0 if no number is found

        // Use regex to find a number in the scene name and parse it
        Match match = Regex.Match(currentLevel, @"\d+");
        if (match.Success)
        {
            levelNumber = int.Parse(match.Value);
        }

        int incrementedLevelNumber = levelNumber + 1;

        GameObject levelNumberObject = GameObject.Find("LevelNumber");
        if (levelNumberObject != null)
        {
            Text textComponent = levelNumberObject.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = incrementedLevelNumber.ToString();
            }
            else if (levelNumberObject.GetComponent<TextMeshProUGUI>() is TextMeshProUGUI tmpComponent)
            {
                tmpComponent.text = incrementedLevelNumber.ToString();
            }
        }
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
