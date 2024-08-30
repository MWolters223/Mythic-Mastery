using UnityEngine;

public class EnemyManagerModel : MonoBehaviour {
    private int activeEnemyCount = 0;

    public int ActiveEnemyCount => activeEnemyCount;

    public void RegisterEnemy()
    {
        activeEnemyCount++;
    }

    public void UnregisterEnemy()
    {
        activeEnemyCount--;
    }

    public bool AreAllEnemiesDefeated()
    {
        return activeEnemyCount == 0;
    }

    public void ResetEnemyCount()
    {
        activeEnemyCount = 0;
    }
}