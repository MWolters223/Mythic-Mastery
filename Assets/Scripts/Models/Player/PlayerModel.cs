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

        InitializeComponents();
        InitializeTransforms();
        InitializeLayerMasks();
        InitializeLineRenderer();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("God has no rigidbody, please add it");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (GetComponent<BoxCollider>() == null)
        {
            Debug.LogError("God has no box collider, please add it");
        }

        transform.localScale = new Vector3(2, 2, 2);
    }

    private void InitializeTransforms()
    {
        if (godPrefab == null)
        {
            Debug.LogError("God prefab not assigned.");
            return;
        }

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
    }

    private void InitializeLayerMasks()
    {
        groundMask = GetLayerMask("Ground");
        obstacleMask = GetLayerMask("Obstacle");
        enemyMask = GetLayerMask("Enemy");
    }

    private LayerMask GetLayerMask(string layerName)
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex == -1)
        {
            Debug.LogError($"Layer '{layerName}' not found.");
            return 0;
        }
        return 1 << layerIndex;
    }

    private void InitializeLineRenderer()
    {
        GameObject lineRendererObject = GameObject.Find("Line");
        if (lineRendererObject == null)
        {
            Debug.LogError("GameObject 'Line' not found in scene, please add it.");
            return;
        }

        lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("No LineRenderer component found on 'Line'.");
        }
    }
}
