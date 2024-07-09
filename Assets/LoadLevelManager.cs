using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextLevel : MonoBehaviour
{
    private int nextLevelIndex = 0;
    private int levelCount = 0;
    public TMPro.TextMeshProUGUI levelNumberText;


    void Start()
    {
        SceneTransitionManager sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        if (sceneTransitionManager != null)
        {
            levelCount = sceneTransitionManager.SceneCount;
            nextLevelIndex = sceneTransitionManager.NextLevelIndex;
        }

        SetUiLevelNumber();
        Fadeout();
        Invoke("Fadein", 5f);
        
        Invoke("LoadSceneWithDelay", 7f);
        Invoke("Fadeout", 7f);
    }

    private void SetUiLevelNumber()
    {
        if (levelNumberText != null)
        {
            levelNumberText.text = levelCount.ToString();
        }
    }

    void LoadSceneWithDelay()
    {
        SceneManager.LoadScene(nextLevelIndex);
        AudioManager.instance.PlayMusic("Battle Muziek");
    }

    void Fadein()
    {
        SceneAnimation.Active.EindScene();
    }

    void Fadeout()
    {
        SceneAnimation.Active.StartScene();
    }
}
