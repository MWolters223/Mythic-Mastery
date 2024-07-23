using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    public GameObject GodPrefab;

    private Rigidbody rb;
    private GameObject Player;

    public EnemyConfig config;

    public NavMeshAgent agent;
    public Transform statueTransform;
    public Transform diskTransform;
    public float cooldownTimer;
    public Vector3 velocity = Vector3.zero;

    void Start()
    {
        InitializeComponents();
        InitializeTransforms();
        InitializePlayer();
    }

    void Awake()
    {
        cooldownTimer = config.shootingCooldown;
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
            agent.speed = config.speed;
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

        cooldownTimer = config.shootingCooldown;
    }

    public void SetShootingCooldownTimer(float cooldown)
    {
        cooldownTimer = cooldown;
    }
}