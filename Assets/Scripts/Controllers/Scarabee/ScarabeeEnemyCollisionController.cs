using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class ScarabeeEnemyCollisionController : MonoBehaviour
{
    private ScarabeeModel model; 
    private ScarabeeView view;

    private ScarabeeScoreController scoreController;
    private ScarabeeDestructionController destructionController;

    public void Initialize(ScarabeeModel model, ScarabeeView view, ScarabeeScoreController scoreController, ScarabeeDestructionController destructionController)
    {
        this.model = model;
        this.view = view;
        this.scoreController = scoreController;
        this.destructionController = destructionController;
    }

    public void HandleEnemyCollision(GameObject enemy)
    {
        string enemyName = enemy.gameObject.name;
        int points = DeterminePointsForEnemy(enemyName);

        scoreController.AddPoints(points);
        destructionController.DestroyObject(enemy);
        destructionController.DestroyProjectile(gameObject);
    }

    private int DeterminePointsForEnemy(string enemyName)
    {
        if (enemyName.Contains("sfinx", StringComparison.OrdinalIgnoreCase))
        {
            return 1;
        }
        else if (enemyName.Contains("horus", StringComparison.OrdinalIgnoreCase))
        {
            return 2;
        }
        else if (enemyName.Contains("ra", StringComparison.OrdinalIgnoreCase))
        {
            return 3;
        }
        else if (enemyName.Contains("bastet", StringComparison.OrdinalIgnoreCase))
        {
            return 3;
        }
        else if (enemyName.Contains("anubis", StringComparison.OrdinalIgnoreCase))
        {
            return 4;
        }
        else if (enemyName.Contains("sekhmet", StringComparison.OrdinalIgnoreCase))
        {
            return 4;
        }
        return 0;
    }
}
