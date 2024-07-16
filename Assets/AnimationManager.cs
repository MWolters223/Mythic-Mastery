using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public Animator DoorAnimator;
    public Animator MusicAnimator;

    public int TransistionSceneIndex;
    public int FirstLevelIndex;
    public int LastMenuIndex;

    public Slider LoadingBar;

    public static AnimationManager Instance;

    private Boolean EnterIsPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        DoorAnimator.SetTrigger("tekst fade in");
    }

    // Update is called once per frame
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

    public async void loadNextScene(int sceneBuilderIndex)
    {
        //speel geluid af wanneer in menu
        if (sceneBuilderIndex <= LastMenuIndex)
        {
            AudioManager.instance.PlaySFX("Knop klik");
        }

        DoorAnimator.SetTrigger("door close");

        //laad menu scene
        if (sceneBuilderIndex <= LastMenuIndex)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            SceneManager.LoadSceneAsync(sceneBuilderIndex);
        }

        //laad eerst level transitie spel daarna laad eerste level
        if (sceneBuilderIndex == TransistionSceneIndex)
        {
            AudioManager.instance.PlaySFX("Knop klik");
            MusicAnimator.SetTrigger("Muziek fade out");
            await Task.Delay(TimeSpan.FromSeconds(1));
            SceneManager.LoadSceneAsync(TransistionSceneIndex);
            DoorAnimator.SetTrigger("door open");
            await Task.Delay(TimeSpan.FromSeconds(5));
            DoorAnimator.SetTrigger("door close");
            await Task.Delay(TimeSpan.FromSeconds(1));
            DoorAnimator.SetTrigger("Begin Loading");
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            StartCoroutine(loadAsync(FirstLevelIndex));
            DoorAnimator.SetTrigger("End Loading");
            await Task.Delay(TimeSpan.FromSeconds(0.5));
        }

        DoorAnimator.SetTrigger("door open");
    }

    //voor het laden van een level met daarbij een update laadbalk
    private IEnumerator loadAsync(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        //DoorAnimator.SetTrigger("Begin Loading");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress /0.9f);

            LoadingBar.value = progress;

            yield return null;
        }

        //DoorAnimator.SetTrigger("End Loading");
    }
}
