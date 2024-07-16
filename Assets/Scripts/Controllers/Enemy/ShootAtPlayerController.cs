using UnityEngine;

public class ShootAtPlayerController : MonoBehaviour
{
    private GameObject player;
    private EnemyModel model;
    private EnemyView view;

    public void Initialize(EnemyModel model, EnemyView view) 
    {
        this.model = model;
        this.view = view; 

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
            model.cooldownTimer = model.shootingCooldown;
        }
        else
        {
            model.cooldownTimer -= Time.deltaTime;
        }
    }

    public void ShootProjectile()
    {
        Vector3 projectileSpawnPosition = model.statueTransform.position + model.statueTransform.forward * model.projectileRadius + new Vector3(0, model.shootingHeight, 0);

        GameObject projectile = Instantiate(view.projectilePrefab, projectileSpawnPosition, model.statueTransform.rotation);

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.maxReflectCount = model.maxReflectCount;
        projectileScript.reflectCount = 0;
        projectileScript.speed = model.projectileSpeed;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = model.statueTransform.forward * model.projectileSpeed;
        }
    }
}
