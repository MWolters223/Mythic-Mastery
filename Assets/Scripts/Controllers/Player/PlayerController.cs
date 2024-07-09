using System;
using UnityEngine;

[RequireComponent(typeof(PlayerView), typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    public GameObject godPrefab;
    public GameObject projectilePrefab;

    private PlayerModel model;
    private PlayerView view;
    private PlayerInputHandler inputHandler;
    private PlayerMovementHandler movementHandler;
    private PlayerShootingHandler shootingHandler;
    private PlayerCollisionHandler collisionHandler;

    private Rigidbody rb;
    private BoxCollider collider;

    private float cooldownTimer;

    private LayerMask groundMask;
    private LayerMask obstacleMask;
    private LayerMask enemyMask;

    void Start()
    {
        model = new PlayerModel();
        view = GetComponent<PlayerView>();
        inputHandler = gameObject.AddComponent<PlayerInputHandler>();
        movementHandler = gameObject.AddComponent<PlayerMovementHandler>();
        shootingHandler = gameObject.AddComponent<PlayerShootingHandler>();
        collisionHandler = gameObject.AddComponent<PlayerCollisionHandler>();

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        if (rb == null || collider == null)
        {
            Debug.LogError("PlayerController: Rigidbody or BoxCollider component is missing.");
            return;
        }

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.localScale = new Vector3(2, 2, 2);

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

        groundMask = LayerMask.GetMask("Ground");
        obstacleMask = LayerMask.GetMask("Obstacle");
        enemyMask = LayerMask.GetMask("Enemy");

        movementHandler.Initialize(model, view, rb);
        shootingHandler.Initialize(model, view, projectilePrefab); // Pass projectilePrefab
        collisionHandler.Initialize(model, view);
    }

    void FixedUpdate()
    {
        Vector2 movementInput = inputHandler.GetMovementInput();
        movementHandler.HandleMovement(movementInput);
        collisionHandler.PreventSfinxCollision();
    }

    void Update()
    {
        Vector3 mouseDirection = inputHandler.GetMouseDirection(view.transform, groundMask);
        if (inputHandler.IsShooting() && cooldownTimer <= 0)
        {
            shootingHandler.HandleShooting(mouseDirection);
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
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask | obstacleMask | enemyMask))
        {
            view.UpdateLaser(transform.position + new Vector3(0, model.shootingHeight, 0), hit.point + new Vector3(0, model.shootingHeight, 0));
        }
        else
        {
            view.DisableLaser();
        }
    }
}
