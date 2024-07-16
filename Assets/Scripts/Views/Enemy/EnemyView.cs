using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Transform statueTransform;
    public Transform diskTransform;
    public GameObject projectilePrefab;

    public void Initialize(Transform statue, Transform disk)
    {
        statueTransform = statue;
        diskTransform = disk;
    }

    public Vector3 GetRotationPoint()
    {
        float diskHeight = diskTransform.localScale.y;
        Vector3 rotationPoint = diskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    public void RotateStatue(float idleRotationSpeed)
    {
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2) * idleRotationSpeed;
        statueTransform.Rotate(Vector3.up, angle * Time.deltaTime);
    }

    public void ShootProjectile(Vector3 position, Quaternion rotation, float speed, int maxReflectCount)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.maxReflectCount = maxReflectCount;
        projectileScript.reflectCount = 0;
        projectileScript.speed = speed;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = statueTransform.forward * speed;
        }
    }

    public void Move(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetVelocity(Vector3 velocity)
    {
        GetComponent<Rigidbody>().velocity = velocity;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}