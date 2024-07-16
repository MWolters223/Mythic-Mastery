using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private GameObject Player;

    [Header("Required")]
    public GameObject GodPrefab;
    public GameObject ProjectilePrefab;

    [Header("Aiming properties")]
    public float rotationSpeed = 5.0f;
    public float idleRotationSpeed = 100.0f;
    public Transform statueTransform;
    public Transform diskTransform;

    [Header("Projectile properties")]
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 2.0f;
    public float cooldownTimer = 0.0f;
    public int maxReflectCount = 1;
    public float shootingHeight = 10.0f;

    [Header("RandomDriveProperty")]
    public float range = 200.0f;

    [Header("Movement")]
    public float speed = 10.0f;

    [Header("Avoid sfinx properties")]
    public float avoidEnemyRadius = 1.0f;
    public float smoothTime = 0.1f;
    public Vector3 velocity = Vector3.zero;
    public float avoidEnemyDistance = 2.0f;

    void Start()
    {
        InitializeComponents();
        InitializeTransforms();
        InitializePlayer();
    }

    void Awake()
    {
        cooldownTimer = 0.0f;
    }

    private void InitializeComponents()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent agent for AI is not setup");
        }
        else
        {
            agent.speed = speed;
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Add a rigidBody to the AI");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            Debug.LogError("AI has no box collider, please add it");
        }

        transform.localScale = new Vector3(2, 2, 2);
    }

    private void InitializeTransforms()
    {
        if (GodPrefab == null)
        {
            Debug.LogError("God prefab not assigned.");
            return;
        }

        statueTransform = GodPrefab.transform.Find("standbeeld");
        diskTransform = GodPrefab.transform.Find("grondplaat");

        if (statueTransform == null)
        {
            Debug.LogError("Standbeeld part not found in the god prefab.");
        }

        if (diskTransform == null)
        {
            Debug.LogError("Grondplaat part not found in the god prefab.");
        }
    }

    private void InitializePlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player is not in scene or player has no tag Player");
        }

        cooldownTimer = shootingCooldown;
    }
}