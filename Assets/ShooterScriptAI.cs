using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShooterScriptAI : MonoBehaviour
{
    [Header("Required")]
    public GameObject GodPrefeb;
    public GameObject ProjectilePrefeb;

    [Header("Aiming properties")]
    public float rotationSpeed = 5.0f;
    public float idleRotationSpeed = 100.0f;
    public Transform statueTransform;
    public Transform diskTransform;

    [Header("Porjectile properties")]
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 5.0f;
    public float cooldownTimer = 0.0f;
    public int MaxReflectCount = 1;
    public float shootingHeight = 10.0f;

    public GameObject Player;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Add a rigidBody to the AI");
        }
        else
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
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

            if (ObstacleBetween("Player"))
            {
                AimAtPlayer();

                if (cooldownTimer > 0)
                {
                    cooldownTimer -= Time.deltaTime;
                }
                else
                {
                    if (ObstacleBetween("Enemy"))
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
            }
        }
    }

    public void AimAtPlayer()
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
    public Vector3 GetRotationPoint()
    {
        float diskHeight = diskTransform.localScale.y;
        Vector3 rotationPoint = diskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    bool ObstacleBetween(string tag)
    {
        RaycastHit hit;

        Vector3 direction = Player.transform.position - statueTransform.position;
        Vector3 raycastStart = new Vector3(statueTransform.position.x, shootingHeight, statueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                return false;
            }
        }

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
}