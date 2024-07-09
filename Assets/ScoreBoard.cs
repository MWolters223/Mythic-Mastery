using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    private int score = 0;

    public TMPro.TextMeshProUGUI sessionScoreText;
    private int sessionScore = 0;

    void Start()
    {
        SceneAnimation.Active.StartScene();
        if (PlayerPrefs.HasKey("Score: "))
        {
            score = PlayerPrefs.GetInt("Score: ");
        }

        scoreText.text = "Totale Score: " + score;

        if (PlayerPrefs.HasKey("SessionScore"))
        {
            sessionScore = PlayerPrefs.GetInt("SessionScore");
        }

        sessionScoreText.text = "Huidige Score: " + sessionScore;

        if (PlayerPrefs.HasKey("SessionScore"))
        {
            PlayerPrefs.SetInt("SessionScore", 0);
            PlayerPrefs.Save();
        }
    }
}
