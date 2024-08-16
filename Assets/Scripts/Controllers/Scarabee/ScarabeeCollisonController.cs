using UnityEngine;

public class ScarabeeCollisionController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;

    private ScarabeeEnemyCollisionController enemyCollisionController;
    private ScarabeePlayerCollisionController playerCollisionController;
    private ScarabeeDestructionController destructionController;

    private ScarabeeDebug scarabeeDebug;

    private ScarabeeMovementController movementController;

    public void Initialize(ScarabeeModel model, ScarabeeView view, ScarabeeEnemyCollisionController enemyCollisionController, ScarabeePlayerCollisionController playerCollisionController, ScarabeeDestructionController destructionController, ScarabeeMovementController movementController)
    {
        this.model = model;
        this.view = view;
        this.enemyCollisionController = enemyCollisionController;
        this.playerCollisionController = playerCollisionController;
        this.destructionController = destructionController;
        this.movementController = movementController;

        scarabeeDebug = GetComponent<ScarabeeDebug>();
    }

    private void Update()
    {
        movementController.UpdateMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;

        string collidedTag = collidedObject.tag;

        switch (collidedTag)
        {
            case "Scarabee":
                destructionController.DestroyObject(collidedObject);
                destructionController.DestroyProjectile(gameObject);
                break;

            case "Player":
                if (!model.isIgnoringPlayer)
                {
                    playerCollisionController.HandlePlayerCollision(collidedObject);
                }
                break;

            case "Enemy":
                enemyCollisionController.HandleEnemyCollision(collidedObject);
                break;

            default:
                HandleDefaultCollision(collision);
                break;
        }
    }

    private void HandleDefaultCollision(Collision collision)
    {
        if (model.reflectCount >= model.maxReflectCount)
        {
            AudioManager.instance.PlaySFX("Scarabee raakt muur");
            destructionController.DestroyProjectile(gameObject);
        }
        else
        {
            model.reflectCount++;
            AudioManager.instance.PlaySFX("Scarabee raakt muur");

            movementController.ReflectMovement(collision);
            scarabeeDebug.traveledPathPoints.Add(transform.position);
        }
    }
}
