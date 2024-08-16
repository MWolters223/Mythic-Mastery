using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;
    private PlayerInputController inputController;
    private PlayerMovementController movementController;
    private PlayerShootingController shootingController;
    private PlayerCollisionController collisionController;

    private PlayerConfig config;
    private float cooldownTimer;

    void Start()
    {
        model = GetComponent<PlayerModel>();
        config = model.config;

        view = GetComponent<PlayerView>();
        view.Initialize(model);

        inputController = gameObject.AddComponent<PlayerInputController>();
        movementController = gameObject.AddComponent<PlayerMovementController>();
        shootingController = gameObject.AddComponent<PlayerShootingController>();
        collisionController = gameObject.AddComponent<PlayerCollisionController>();

        movementController.Initialize(model, view, GetComponent<Rigidbody>());
        shootingController.Initialize(model, view);
        collisionController.Initialize(model, view);
    }

    void FixedUpdate()
    {
        Vector2 movementInput = inputController.GetMovementInput();
        movementController.HandleMovement(movementInput);
        collisionController.PreventSfinxCollision();
    }

    void Update()
    {
        Vector3 mouseDirection = inputController.GetMouseDirection(view.transform, LayerMask.GetMask("Ground"));

        if (inputController.IsShooting() && cooldownTimer <= 0)
        {
            shootingController.HandleShooting(mouseDirection);
            cooldownTimer = config.shootingCooldown;
        }

        cooldownTimer -= Time.deltaTime;

        view.HandleStatueRotation(mouseDirection);
        view.SetLaserPosition(mouseDirection, config.shootingHeight);
    }
}
