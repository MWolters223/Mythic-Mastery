using UnityEngine;

public class ShootAtPlayerController : MonoBehaviour
{
    private GameObject player;
    private EnemyModel model;
    private EnemyView view;

    private EnemyConfig config;

    public void Initialize(EnemyModel model, EnemyView view) 
    {
        this.model = model;
        this.view = view; 
        this.config = model.config;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not in scene or player has no tag Player");
        }
    }

    public void ShootAtPlayer()
    {
       
        if (player != null && model.cooldownTimer <= 0)
        {
            ShootProjectile();
            model.cooldownTimer = config.shootingCooldown;
        }
        else
        { 
            model.cooldownTimer -= Time.deltaTime;
        }
    }

    public void ShootProjectile()
    {
        Transform statueTransform = model.statueTransform;
        Vector3 projectileSpawnPosition = statueTransform.position + statueTransform.forward * config.projectileRadius + new Vector3(0, config.shootingHeight, 0);

        GameObject projectile = Instantiate(config.projectilePrefab, projectileSpawnPosition, statueTransform.rotation);

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.maxReflectCount = config.maxReflectCount;
        projectileScript.reflectCount = 0;
        projectileScript.speed = config.projectileSpeed;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = statueTransform.forward * config.projectileSpeed;
        }
    }
}
