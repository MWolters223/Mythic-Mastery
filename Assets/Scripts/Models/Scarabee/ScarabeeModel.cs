using UnityEngine;

public class ScarabeeModel : MonoBehaviour
{
    public int Points { get; private set; }
    public int SessionPoints { get; private set; }

    [HideInInspector]
    public float speed = 0;

    [HideInInspector]
    public int reflectCount = 0;

    [HideInInspector]
    public int maxReflectCount = 1;

    [HideInInspector]
    public bool isIgnoringPlayer = false;

    public void LoadScores()
    {
        Points = PlayerPrefs.GetInt("Score: ", 0);
        SessionPoints = PlayerPrefs.GetInt("SessionScore", 0);
    }

    public void SaveScores()
    {
        PlayerPrefs.SetInt("SessionScore", SessionPoints);
        PlayerPrefs.SetInt("Score: ", Points);
        PlayerPrefs.Save();
    }

    public void AddPoints(int points)
    {
        Points += points;
        SessionPoints += points;
        SaveScores();
    }
}