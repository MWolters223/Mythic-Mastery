using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyManagerRegistrationController : MonoBehaviour
{
    private EnemyManagerModel model;

    public void Initialize(EnemyManagerModel model)
    {
        this.model = model;
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        model.ResetEnemyCount();
    }

    public void RegisterEnemy()
    {
        model.RegisterEnemy();
        Debug.Log("Enemy registered. Total enemies: " + model.ActiveEnemyCount);
    }

    public void UnregisterEnemy()
    {
        model.UnregisterEnemy();
        Debug.Log("Enemy unregistered. Total enemies: " + model.ActiveEnemyCount);
        CheckForRemainingEnemies();
    }

    private void CheckForRemainingEnemies()
    {
        if (model.AreAllEnemiesDefeated())
        {
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneController.Instance.SceneTransitionController.LoadNextScene(nextLevelIndex);
        }
    }
}