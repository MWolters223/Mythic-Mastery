using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject godPrefab;
    public GameObject projectilePrefab;

    private PlayerModel model;
    private PlayerView view;
    private PlayerInputController inputController;
    private PlayerMovementController movementController;
    private PlayerShootingController shootingController;
    private PlayerCollisionController collisionController;

    private float cooldownTimer;

    void Start()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
        inputController = gameObject.AddComponent<PlayerInputController>(); 
        movementController = gameObject.AddComponent<PlayerMovementController>();
        shootingController = gameObject.AddComponent<PlayerShootingController>();
        collisionController = gameObject.AddComponent<PlayerCollisionController>();

        movementController.Initialize(model, view, GetComponent<Rigidbody>());
        shootingController.Initialize(model, view, projectilePrefab);
        collisionController.Initialize(model, view);

        InitializeView();
    }

    private void InitializeView()
    {
        if (!godPrefab || !projectilePrefab)
        {
            Debug.LogError("PlayerController: GodPrefab or ProjectilePrefab is not assigned.");
            return;
        }

        Transform statueTransform = godPrefab.transform.Find("standbeeld");
        Transform diskTransform = godPrefab.transform.Find("grondplaat");

        if (statueTransform == null || diskTransform == null)
        {
            Debug.LogError("PlayerController: Statue or Disk part not found in the god prefab.");
            return;
        }

        LineRenderer lineRenderer = GameObject.Find("Line")?.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("PlayerController: LineRenderer component not found.");
            return;
        }

        view.Initialize(statueTransform, diskTransform, lineRenderer);
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
            cooldownTimer = model.shootingCooldown;
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        HandleStatueRotation(mouseDirection);
        SetLaserPosition(mouseDirection);
    }

    private void HandleStatueRotation(Vector3 direction)
    {
        if (direction.magnitude < 1f || direction == Vector3.zero)
            return;

        Vector3 statueForward = view.StatueTransform.forward;
        float angle = Vector3.SignedAngle(statueForward, direction, Vector3.up);
        view.RotateStatue(GetRotationPoint(), Vector3.up, angle);
    }

    private Vector3 GetRotationPoint()
    {
        float diskHeight = view.DiskTransform.localScale.y;
        return view.DiskTransform.position + new Vector3(0, diskHeight / 2, 0);
    }

    private void SetLaserPosition(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Obstacle", "Enemy")))
        {
            view.UpdateLaser(transform.position + new Vector3(0, model.shootingHeight, 0), hit.point + new Vector3(0, model.shootingHeight, 0));
        }
        else
        {
            view.DisableLaser();
        }
    }
}
