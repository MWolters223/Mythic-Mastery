using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoofdMenu : MonoBehaviour
{
    
    public void TerugNaarMenu()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(0);
    }

    public void SpelOpties()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(1);
    }

    public void StartSpelAnimatie()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(7);
    }

    public void SpelInstructies()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(2);
    }

    public void Credits()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(3);
    }

    public void SpelMenu()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(4);
    }

    public void Winkel()
    {
        SceneController.Instance.SceneTransitionController.LoadNextScene(5);
    }

    public void Stoppen()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        Application.Quit();
    }

    [SerializeField] GameObject PauzeMenu;
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if (PauzeMenu.activeSelf == true)
            {
                PauzeMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauzeMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void TerugNaarMenuVanuitSpel()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("Thema");
        SceneController.Instance.SceneTransitionController.LoadNextScene(6);
    }

    public void HervatSpel()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        PauzeMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
