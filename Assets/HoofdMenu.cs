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
        AnimationManager.Instance.loadNextScene(0);
    }

    public void SpelOpties()
    {
        AnimationManager.Instance.loadNextScene(1);
    }

    public void StartSpelAnimatie()
    {
        AnimationManager.Instance.loadNextScene(7);
    }

    public void SpelInstructies()
    {
        AnimationManager.Instance.loadNextScene(2);
    }

    public void Credits()
    {
        AnimationManager.Instance.loadNextScene(3);
    }

    public void SpelMenu()
    {
        AnimationManager.Instance.loadNextScene(4);
    }

    public void Winkel()
    {
        AnimationManager.Instance.loadNextScene(5);
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
        AudioManager.instance.PlaySFX("Knop klik");
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("Thema");
        SceneManager.LoadScene(6);
    }

    public void HervatSpel()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        PauzeMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
