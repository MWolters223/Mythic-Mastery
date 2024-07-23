using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Config/SfinxSettings", order = 1)]
public class SfinxConfig : ScriptableObject
{
    public GameObject projectilePrefab;
    public float rotationSpeed = 5.0f;
    public float idleRotationSpeed = 100.0f;
    public float projectileSpeed = 30.0f;
    public float projectileRadius = 15.0f;
    public float shootingCooldown = 5.0f;
    public float cooldownTimer = 0.0f;
    public int maxReflectCount = 1;
    public float shootingHeight = 10.0f;
}