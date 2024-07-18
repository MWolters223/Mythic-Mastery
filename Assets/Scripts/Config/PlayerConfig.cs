using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Setup Required")]
    public GameObject projectilePrefab;

    [Header("Movement properties")]
    public float speed = 35.0f;
    public float rotationSpeed = 100.0f;

    [Header("Aiming properties")]
    public float aimSpeed = 100.0f;

    [Header("Projectile properties")]
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 0.5f;
    public int maxReflectCount = 1;
    public float shootingHeight = 10.0f;

    [Header("Lazer properties")]
    public LayerMask groundMask;
    public LayerMask obstacleMask;
    public LayerMask enemyMask;

    [Header("Avoid sfinx properties")]
    public float avoidEnemyRadius = 1.0f;
    public float smoothTime = 0.1f;
    public float avoidEnemyDistance = 2.0f;
}
