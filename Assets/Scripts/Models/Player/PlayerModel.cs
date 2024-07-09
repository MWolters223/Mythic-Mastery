using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
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

    void Start()
    {
        Debug.Log("GodController Start on " + gameObject.name);

        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("God has no rigidbody, please add it");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
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
}
