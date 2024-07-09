using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class GodAI : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Required")]
    public GameObject GodPrefeb;
    public GameObject ProjectilePrefeb;

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
    public int MaxReflectCount = 1;
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

    GameObject Player;
    private Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        if (agent == null)
        {
            Debug.Log("NavMeshAgent agent for AI is not setup");
        }
        
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Add a rigidBody to the AI");
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

        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.Log("Player is not in scene or player has no tag Player");
        }


        statueTransform = GodPrefeb.transform.Find("standbeeld");
        diskTransform = GodPrefeb.transform.Find("grondplaat");

        if (statueTransform == null)
        {
            Debug.LogError("Standbeeld part not found in the god prefab.");
        }
        if (diskTransform == null)
        {
            Debug.LogError("Grondplaat part not found in the god prefab.");
        }

        cooldownTimer = shootingCooldown;
    }

    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            if (!ObjectWithTagInView("Player"))
            {
                AimAtPlayer();
                HandleNavMeshMovement();

                if (cooldownTimer > 0)
                {
                    cooldownTimer -= Time.deltaTime;
                }
                else
                {
                    if (ObjectWithTagInView("Enemy"))
                    {
                        ShootProjectile();
                        AudioManager.instance.PlaySFX("Scarabee afgevuurt");
                    }
                    cooldownTimer = shootingCooldown;

                }
            }
            else
            {
                RotateStatue();
                setRandomPoint();
            }
        }
        else
        {
            StopAiMovement();
        }
    }

    void FixedUpdate()
    {
        PreventSfinxCollision();
    }

    void StopAiMovement()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
        }

        Rigidbody aiRigidbody = GetComponent<Rigidbody>();
        if (aiRigidbody != null)
        {
            aiRigidbody.velocity = Vector3.zero;
            aiRigidbody.angularVelocity = Vector3.zero;
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

    void HandleNavMeshMovement()
    {
        agent.destination = GetPlayerLocation();
    }

    Vector3 GetPlayerLocation()
    {
        return Player.transform.position;
    }

    void AimAtPlayer()
    {
        if (Player != null)
        {
            Vector3 rotationPoint = GetRotationPoint();

            Vector3 direction = Player.transform.position - rotationPoint;
            direction.y = 0;

            Quaternion rotation = Quaternion.LookRotation(direction);
            statueTransform.rotation = Quaternion.Slerp(statueTransform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    Vector3 GetRotationPoint()
    {
        float diskHeight = diskTransform.localScale.y;
        Vector3 rotationPoint = diskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    bool ObjectWithTagInView(string tag)
    {
        RaycastHit hit;

        Vector3 direction = Player.transform.position - statueTransform.position;
        Vector3 raycastStart = new Vector3(statueTransform.position.x, shootingHeight, statueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                Debug.DrawRay(raycastStart, direction, Color.green);
                return false;
            }
        }
        Debug.DrawRay(raycastStart, direction, Color.red);


        return true; 
    }

    void RotateStatue()
    {
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2) * idleRotationSpeed;
        statueTransform.Rotate(Vector3.up, angle * Time.deltaTime);
    }

    void ShootProjectile()
    {
        Vector3 projectileSpawnPosition = statueTransform.position + statueTransform.forward * projectileRadius + new Vector3(0, shootingHeight, 0);

        GameObject projectile = Instantiate(ProjectilePrefeb, projectileSpawnPosition, statueTransform.rotation);

        Projectile projectile1 = projectile.GetComponent<Projectile>();
        projectile1.maxReflectCount = MaxReflectCount;
        projectile1.reflectCount = 0;
        projectile1.speed = projectileSpeed;

        Rigidbody projectileRb = projectile1.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = statueTransform.forward * projectileSpeed;
        }
    }

    void setRandomPoint()
    {
        NavMeshHit hit;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randompoint = GetRotationPoint() + UnityEngine.Random.insideUnitSphere * range;
            Debug.DrawRay(randompoint, Vector3.up, Color.red, 10.0f);
            
            if (NavMesh.SamplePosition(randompoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(randompoint);
            }
        }
    }
}
