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
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(0);
    }

    public void SpelOpties()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(1);
    }

    public void StartSpelAnimatie()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneAnimation.Active.EindScene();
        //Invoke("Fadeout", 2f);
        Invoke("StartSpel", 2f);
        
    }

    public void StartSpel()
    {
        AudioManager.instance.musicSource.Stop();
        SceneManager.LoadSceneAsync(7);
        Time.timeScale = 1;
    }

    public void SpelInstructies()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(2);
    }

    public void Credits()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(3);
    }

    public void SpelMenu()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(4);
    }

    public void Winkel()
    {
        AudioManager.instance.PlaySFX("Knop klik");
        SceneManager.LoadSceneAsync(5);
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
