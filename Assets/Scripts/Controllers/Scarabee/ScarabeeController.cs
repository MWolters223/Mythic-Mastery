using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;

public class ScarabeeController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;
    private ScarabeeScoreController scoreController;
    private ScarabeeCollisionController collisionController;
    private ScarabeeEnemyCollisionController enemyCollisionController;
    private ScarabeePlayerCollisionController playerCollisionController;
    private ScarabeeDestructionController destructionController;
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

        scarabeeDebug = gameObject.AddComponent<ScarabeeDebug>();

        scoreController.Initialize(model, view);
        enemyCollisionController.Initialize(model, view, scoreController, destructionController);
        playerCollisionController.Initialize(model, view, destructionController);
        collisionController.Initialize(model, view, enemyCollisionController, playerCollisionController, destructionController);
        rb = GetComponent<Rigidbody>();
    }


    // adjust rotation; destroy if stationary or moving vertically
    void Update()
    { 
        Vector3 horizontalDirection = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalDirection.magnitude > 0.01f)
        {

            Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection) * Quaternion.Euler(0, 90, 0);
            transform.rotation = targetRotation;  
            Debug.Log($"New Rotation: {transform.rotation}");
        }
        else
        {
    
            Destroy(gameObject);
        }

        if (Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
