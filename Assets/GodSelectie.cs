using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodSelectie : MonoBehaviour
{
    public GameObject horusInstance;
    public GameObject sekhmetInstance;
    public GameObject bastetInstance;
    public GameObject raInstance;
    public GameObject anubisInstance;

    int horus;
    int sekhmet;
    int bastet;
    int ra;
    int anubis;

    public GameObject horusKnop;
    public GameObject sekhmetKnop;
    public GameObject bastetKnop;
    public GameObject raKnop;
    public GameObject anubisKnop;

    public GameObject horusPlaatje;
    public GameObject sekhmetPlaatje;
    public GameObject bastetPlaatje;
    public GameObject raPlaatje;
    public GameObject anubisPlaatje;

    void Start()
    {
        if (PlayerPrefs.GetInt("horus") == 0 && PlayerPrefs.GetInt("sekhmet") == 0 && PlayerPrefs.GetInt("bastet") == 0 && PlayerPrefs.GetInt("ra") == 0 && PlayerPrefs.GetInt("anubis") == 0)
        {
            PlayerPrefs.SetInt("horus", 1);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "Winkel")
        {
            if (PlayerPrefs.GetInt("horus") == 1)
            {
                horusPlaatje.SetActive(true);
            }
            if (PlayerPrefs.GetInt("sekhmet") == 1)
            {
                sekhmetPlaatje.SetActive(true);
            }
            if (PlayerPrefs.GetInt("bastet") == 1)
            {
                bastetPlaatje.SetActive(true);
            }
            if (PlayerPrefs.GetInt("ra") == 1)
            {
                raPlaatje.SetActive(true);
            }
            if (PlayerPrefs.GetInt("anubis") == 1)
            {
                anubisPlaatje.SetActive(true);
            }
        }

        if(scene.name.Contains("level", StringComparison.OrdinalIgnoreCase))
        {
            if (PlayerPrefs.GetInt("horus") == 1)
            {
                horusInstance.SetActive(true);
            }
            if (PlayerPrefs.GetInt("sekhmet") == 1)
            {
                sekhmetInstance.SetActive(true);
            }
            if (PlayerPrefs.GetInt("bastet") == 1)
            {
                bastetInstance.SetActive(true);
            }
            if (PlayerPrefs.GetInt("ra") == 1)
            {
                raInstance.SetActive(true);
            }
            if (PlayerPrefs.GetInt("anubis") == 1)
            {
                anubisInstance.SetActive(true);
            }
        }
    }

    void UpdateGodSelectie(int horusValue, int sekhmetValue, int bastetValue, int raValue, int anubisValue, GameObject activeImage)
    {
        PlayerPrefs.SetInt("horus", horusValue);
        PlayerPrefs.SetInt("sekhmet", sekhmetValue);
        PlayerPrefs.SetInt("bastet", bastetValue);
        PlayerPrefs.SetInt("ra", raValue);
        PlayerPrefs.SetInt("anubis", anubisValue);

        horusPlaatje.SetActive(false);
        sekhmetPlaatje.SetActive(false);
        bastetPlaatje.SetActive(false);
        raPlaatje.SetActive(false);
        anubisPlaatje.SetActive(false);

        activeImage.SetActive(true);
    }

    public void horusKnopfunctie()
    {
        UpdateGodSelectie(1, 0, 0, 0, 0, horusPlaatje);
    }

    public void sekhmetKnopfunctie()
    {
        UpdateGodSelectie(0, 1, 0, 0, 0, sekhmetPlaatje);
    }

    public void bastetKnopfunctie()
    {
        UpdateGodSelectie(0, 0, 1, 0, 0, bastetPlaatje);
    }
    public void raKnopfunctie()
    {
        UpdateGodSelectie(0, 0, 0, 1, 0, raPlaatje);
    }
    public void anubisKnopfunctie()
    {
        UpdateGodSelectie(0, 0, 0, 0, 1, anubisPlaatje);
    }

}
