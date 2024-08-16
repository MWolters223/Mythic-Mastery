using UnityEngine;

public class ScarabeeController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;
    private ScarabeeScoreController scoreController;
    private ScarabeeCollisionController collisionController;
    private ScarabeeEnemyCollisionController enemyCollisionController;
    private ScarabeePlayerCollisionController playerCollisionController;
    private ScarabeeDestructionController destructionController;
    private ScarabeeMovementController movementController;
    private Rigidbody rb;

    private ScarabeeDebug scarabeeDebug;

    void Start()
    {
        model = GetComponent<ScarabeeModel>();
        view = GetComponent<ScarabeeView>();

        view.Initialize(model);

        scoreController = gameObject.AddComponent<ScarabeeScoreController>();
        collisionController = gameObject.AddComponent<ScarabeeCollisionController>();
        enemyCollisionController = gameObject.AddComponent<ScarabeeEnemyCollisionController>();
        playerCollisionController = gameObject.AddComponent<ScarabeePlayerCollisionController>();
        destructionController = gameObject.AddComponent<ScarabeeDestructionController>();
        movementController = gameObject.AddComponent<ScarabeeMovementController>();

        scarabeeDebug = gameObject.AddComponent<ScarabeeDebug>();

        scoreController.Initialize(model, view);
        enemyCollisionController.Initialize(model, view, scoreController, destructionController);
        playerCollisionController.Initialize(model, view, destructionController);
        movementController.Initialize(model, view);
        collisionController.Initialize(model, view, enemyCollisionController, playerCollisionController, destructionController, movementController);
    }
}
