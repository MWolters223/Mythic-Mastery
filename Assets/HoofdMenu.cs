using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoofdMenu : MonoBehaviour
{
    private GameObject player; // Cache the player reference
    [SerializeField] GameObject PauzeMenu;

    public void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

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
        SoundController.Instance.SoundSFXController.PlaySFX("Knop klik");
        Application.Quit();
    }

    private void SetAllScriptsActive(GameObject gameObject, bool isActive)
    {
        MonoBehaviour[] scripts = gameObject.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = isActive;
        }
    }

    private void DisablePlayerScripts()
    {
        SetAllScriptsActive(player, false);
    }

    private void EnablePlayerScripts()
    {
        SetAllScriptsActive(player, true);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (PauzeMenu.activeSelf)
            {
                // Unpause the game
                PauzeMenu.SetActive(false);
                Time.timeScale = 1;
                EnablePlayerScripts(); 
            }
            else
            {
                // Pause the game
                PauzeMenu.SetActive(true);
                Time.timeScale = 0;
                DisablePlayerScripts(); 
            }
        }
    }

    public void TerugNaarMenuVanuitSpel()
    {
        Time.timeScale = 1; // Reset time scale deur animatie
        SoundController.Instance.View.musicSource.Stop();
        SoundController.Instance.SoundMusicController.PlayMusic("Thema");
        SceneController.Instance.SceneTransitionController.LoadNextScene(6);
    }

    public void HervatSpel()
    {
        SoundController.Instance.SoundSFXController.PlaySFX("Knop klik");
        PauzeMenu.SetActive(false);
        EnablePlayerScripts(); 
        Time.timeScale = 1; // Reset time scale
    }
}
