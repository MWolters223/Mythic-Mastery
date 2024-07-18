using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public Animator DoorAnimator;
    public Animator MusicAnimator;

    public int TransistionSceneIndex;
    public int FirstLevelIndex;
    public int LastMenuIndex;

    public GameObject LoadingPanel;
    public Slider LoadingBar;

    public static AnimationManager Instance;

    private bool EnterIsPressed = false;


    void Start()
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
        DoorAnimator.SetTrigger("tekst fade in");
    }


    void Update()
    {
        if (DoorAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !EnterIsPressed)
            {
                EnterIsPressed = true;
                DoorAnimator.SetTrigger("tekst fade out");
            }
        }
    }


    public void loadNextScene(int sceneBuilderIndex)
    {
        StartCoroutine(LoadNextSceneCoroutine(sceneBuilderIndex));
    }


    private IEnumerator LoadNextSceneCoroutine(int sceneBuilderIndex)
    {
        // Speel geluid af wanneer in menu
        if (sceneBuilderIndex <= LastMenuIndex)
        {
            AudioManager.instance.PlaySFX("Knop klik");
        }

        if (sceneBuilderIndex == TransistionSceneIndex)
        {
            AudioManager.instance.PlaySFX("Knop klik");
            MusicAnimator.SetTrigger("Muziek fade out");
        }

        // Sluit de deur animatie
        yield return StartCoroutine(CloseDoorAnimation());

        // Laad menu scene
        if (sceneBuilderIndex <= LastMenuIndex)
        {
            yield return SceneManager.LoadSceneAsync(sceneBuilderIndex);
            yield return StartCoroutine(OpenDoorAnimation());
        }
        else if (sceneBuilderIndex == TransistionSceneIndex)
        {
            yield return SceneManager.LoadSceneAsync(TransistionSceneIndex);

            yield return StartCoroutine(OpenDoorAnimation());
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(CloseDoorAnimation());
            yield return StartCoroutine(LoadAsync(FirstLevelIndex));
            yield return StartCoroutine(OpenDoorAnimation());

            AudioManager.instance.PlayMusic("Battle Muziek");
            MusicAnimator.SetTrigger("Muziek fade in");
        }
        else
        {
            yield return StartCoroutine(OpenDoorAnimation());
        }
    }


    private IEnumerator LoadAsync(int levelIndex)
    {
        Debug.Log("Starting loading animation...");
        LoadingPanel.gameObject.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.value = progress;
            Debug.Log($"Loading progress: {progress * 100}%");

            yield return null;
        }

        Debug.Log("Ending loading animation...");
        LoadingPanel.gameObject.SetActive(false);
    }


    private IEnumerator OpenDoorAnimation()
    {
        Debug.Log("Opening door...");
        DoorAnimator.SetTrigger("door open");

        while (!DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door open") || DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Door open animation completed.");
    }


    private IEnumerator CloseDoorAnimation()
    {
        Debug.Log("Closing door...");
        DoorAnimator.SetTrigger("door close");

        while (!DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door close") || DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Door close animation completed.");
    }
}
