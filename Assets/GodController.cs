using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * GodController.cs
 * 
 * This script handles the movement, rotation, and shooting logic for the god character.
 * It allows the god to move forward, backward, rotate left and right, aim the statue towards
 * the mouse cursor, and shoot projectiles in the direction of the cursor.
 */

public class GodController : MonoBehaviour
{
    [Header("Setup Required")]
    public GameObject godPrefab;  
    public GameObject projectilePrefab; 

    [Header("Movement properties")]
    public float speed = 35.0f;
    public float rotationSpeed = 100.0f;

    [Header("Aiming properties")]
    public Transform statueTransform;
    public float aimSpeed = 100.0f;
    public Transform diskTransform;
    private Rigidbody rb;

    [Header("Projectile properties")]
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 0.5f;
    public float cooldownTimer = 0.0f;
    public int maxReflectCount = 1;
    public float shootingHeight = 10.0f;

    [Header("Lazer properties")]
    public LineRenderer lineRenderer;
    public LayerMask groundMask;
    public LayerMask obstacleMask;
    public LayerMask enemyMask;

    [Header("Avoid sfinx properties")]
    public float avoidEnemyRadius = 1.0f;
    public float smoothTime = 0.1f;
    public Vector3 velocity = Vector3.zero;
    public float avoidEnemyDistance = 2.0f;

    public void FixedUpdate()
    {
        HandleMovement();
        PreventSfinxCollision();
    }

    public void Update()
    {
        HandleStatueRotation();
        HandleShooting();
        SetLaserPosition();
    }

    void Start()
    {
        Debug.Log("GodController Start on " + gameObject.name);

        rb = GetComponent<Rigidbody>();

        if(rb == null)
        {
            Debug.LogError("God has no rigidbody, please add it");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider == null)
        {
            Debug.LogError("God has no box collider, please add it");
        }

        transform.localScale = new Vector3(2, 2, 2);

        statueTransform = godPrefab.transform.Find("standbeeld");
        diskTransform = godPrefab.transform.Find("grondplaat");

        if (statueTransform == null)
        {
            Debug.LogError("Standbeeld part not found in the god prefab.");
        }
        if (diskTransform == null)
        {
            Debug.LogError("Grondplaat part not found in the god prefab.");
        }

        int groundLayerIndex = LayerMask.NameToLayer("Ground");
        if (groundLayerIndex == -1)
        {
            Debug.LogError("Layer 'Ground' not found.");
        }
        else
        {
            groundMask = 1 << groundLayerIndex;
        }

        int obstacleLayerIndex = LayerMask.NameToLayer("Obstacle");
        if (obstacleLayerIndex == -1)
        {
            Debug.LogError("Layer 'Obstacle' not found.");
        }
        else
        {
            obstacleMask = 1 << obstacleLayerIndex;
        }

        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        if (enemyLayerIndex == -1)
        {
            Debug.LogError("Layer 'Enemy' not found.");
        }
        else
        {
            enemyMask = 1 << enemyLayerIndex;
        }

        GameObject lineRendererObject = GameObject.Find("Line");
        if (lineRendererObject == null)
        {
            Debug.LogError("GameObject Line not found in scene, please add it.");
        }
        else
        {
            lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("No LineRenderer component found on Line.");
            }
        }
    }

    public void PreventSfinxCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, avoidEnemyRadius);

        foreach (Collider collider in colliders)
        {

            bool isEnemy = collider.gameObject.CompareTag("Enemy");
            bool isSfinx = collider.gameObject.name.Contains("sfinx", StringComparison.OrdinalIgnoreCase);

            if (isEnemy && isSfinx)
            {
                Vector3 direction;
                float distance;

                if (Physics.ComputePenetration(
                    GetComponent<Collider>(), transform.position, transform.rotation,
                    collider, collider.transform.position, collider.transform.rotation,
                    out direction, out distance))
                {
                    // Add a small offset to prevent getting stuck
                    float offset = 1f;
                    Vector3 targetPosition = transform.position + direction * (distance + offset);
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                }
            }
        }
    }

    public void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");

        if (rb && (horizontalInput != 0 || verticalInput != 0))
        {
            Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
            //AudioManager.instance.PlayDrivePlayer("SpelerRijd");
        }

        if (horizontalInput == 0 && verticalInput == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //AudioManager.instance.DriveSourcePlayer.Stop();
        }
    }

    public void HandleStatueRotation()
    {

        Vector3 direction = GetMouseDirection();

        if (direction.magnitude < 1f)
        {
            return;
        }

        if(direction == Vector3.zero)
        {
            return;
        }

        Vector3 statueForward = statueTransform.forward;
        float angle = Vector3.SignedAngle(statueForward, direction, Vector3.up);
        statueTransform.RotateAround(GetRotationPoint(), Vector3.up, angle);

    }

    Vector3 GetMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3 direction = hit.point - transform.position;
            direction.Normalize();

            if (Vector3.Distance(hit.point, statueTransform.position) < projectileRadius)
            {
                direction = Vector3.zero;
            }

            return direction;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void SetLaserPosition()
    {
        Vector3 direction = GetMouseDirection();

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        LayerMask combinedMask = groundMask | obstacleMask | enemyMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, combinedMask))
        {
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, transform.position + new Vector3(0, shootingHeight, 0));
            lineRenderer.SetPosition(1, hit.point + new Vector3(0, shootingHeight, 0));
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public Vector3 GetRotationPoint()
    {
        float diskHeight = diskTransform.localScale.y;
        Vector3 rotationPoint = diskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    public void HandleShooting()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (Input.GetMouseButton(0))
        {
            ShootProjectile();

            cooldownTimer = shootingCooldown;
        }
    }

    public void ShootProjectile()
    {
        Vector3 direction = GetMouseDirection();
        direction.y = 0;

        AudioManager.instance.PlaySFX("Scarabee afgevuurt");

        if (direction == Vector3.zero)
        {
            return;
        }

        GameObject projectileObj = SpawnProjectile(direction);

        Rigidbody rb = projectileObj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }

    public GameObject SpawnProjectile(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 projectilePosition = CalculateProjectilePosition();
        GameObject projectileObj = Instantiate(projectilePrefab, projectilePosition, rotation);

        ConfigureProjectile(projectileObj);

        return projectileObj;
    }

    public Vector3 CalculateProjectilePosition()
    {
        return statueTransform.position + statueTransform.forward * projectileRadius + new Vector3(0, shootingHeight, 0);
    }

    public void ConfigureProjectile(GameObject projectileObj)
    {
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.maxReflectCount = maxReflectCount;
        projectile.speed = projectileSpeed;
    }
}