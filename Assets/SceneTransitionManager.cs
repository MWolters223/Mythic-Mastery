using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Security.Cryptography;
using System.Collections;

/*public class SceneTransitionManager : MonoBehaviour
{
    private Scene currentScene;
    private string enemyTag = "Enemy";
    private int currentSceneIndex = 0;
    private int sceneCount = 1;
    private int nextLevelIndex = 8;

    bool scoreBoardLoading = false;

    private static SceneTransitionManager instance;

    // Getters for the transition scene
    public int CurrentSceneIndex => currentSceneIndex; 
    public int SceneCount => sceneCount; 
    public int NextLevelIndex => nextLevelIndex;

    public Animator MenuTransition;
    public Animator MusicTransition;
    public Animator SFXTransition;*/

    /*public IEnumerator EndScene()
    {
        MenuTransition.SetTrigger("Eind scene");
        MusicTransition.SetTrigger("Eind scene");
        SFXTransition.SetTrigger("Eind scene");
        yield return new WaitForSeconds(2);
    }

    public IEnumerator StartScene()
    {
        MenuTransition.SetTrigger("Start scene");
        MusicTransition.SetTrigger("Start scene");
        SFXTransition.SetTrigger("Start scene");
        yield return new WaitForSeconds(2);
    }

    // Singleton pattern
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        scoreBoardLoading = false;
    }

    void Start()
    {
        ResetLevelCount();
    }

    void Update()
    {
        if(currentScene != null)
        {
            if (currentScene.name.Contains("level", StringComparison.OrdinalIgnoreCase))
            {
                GameObject player = GameObject.FindWithTag("Player");

                if (AreAllEnemiesDead() && player)
                {                    
                    AudioManager.instance.musicSource.Stop();
                    SceneAnimation.Active.EindScene();
                    LoadNextScene();
                }

                if (!player && !scoreBoardLoading)
                {
                    SceneAnimation.Active.EindScene();
                    LoadScoreBoard();
                    PlayMainMenuMusic();
                }
            }
            else if (currentScene.name.Contains("Scoreboard", StringComparison.OrdinalIgnoreCase))
            {
                ResetLevelCount();
                scoreBoardLoading = true;
            }
        }
    }

    public void ResetLevelCount()
    {
        sceneCount = 1;
        nextLevelIndex = 8;
    }

    void LoadScoreBoard()
    {
        if (!scoreBoardLoading)
        {
            scoreBoardLoading = true;
            ResetLevelCount();
            SceneManager.LoadScene(6);
        }
    }

    void LoadNextScene()
    {
        sceneCount++;
        currentSceneIndex++;

        // Save the next level index so that it can be loaded in the transition scene
        nextLevelIndex = currentSceneIndex;

        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            int transitionSceneIndex = 7;
            SceneManager.LoadScene(transitionSceneIndex);
        }
        else
        {
            LoadScoreBoard();
            PlayMainMenuMusic();
        }
    }

    bool AreAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        return enemies.Length == 0;
    }

    public void PlayMainMenuMusic()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("Thema");
    }
}*/