using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public GameObject projectilePrefab;

    [Header("Aiming properties")]
    public float rotationSpeed = 5.0f;
    public float idleRotationSpeed = 100.0f;

    [Header("Projectile properties")]
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 2.0f;
    public int maxReflectCount = 1;
    public float shootingHeight = 10.0f;

    [Header("RandomDriveProperty")]
    public float range = 200.0f;

    [Header("Movement")]
    public float speed = 10.0f;

    [Header("Avoid sfinx properties")]
    public float avoidEnemyRadius = 1.0f;
    public float smoothTime = 0.1f;
    public float avoidEnemyDistance = 2.0f;
}
