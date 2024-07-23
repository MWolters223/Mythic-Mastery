using UnityEngine;

public class SfinxShootAtPlayerController : MonoBehaviour
{
    private SfinxModel model;
    private SfinxView view;

    private SfinxConfig config;

    public void Initialize(SfinxModel model, SfinxView view)
    {
        this.model = model;
        this.view = view;
        this.config = model.SfinxConfig;
    }


    public void ShootProjectile()
    {
        Vector3 projectileSpawnPosition = model.StatueTransform.position + model.StatueTransform.forward * config.projectileRadius + new Vector3(0, config.shootingHeight, 0);

        GameObject projectile = Instantiate(config.projectilePrefab, projectileSpawnPosition, model.StatueTransform.rotation);

        Projectile projectile1 = projectile.GetComponent<Projectile>();
        projectile1.maxReflectCount = config.maxReflectCount;
        projectile1.reflectCount = 0;
        projectile1.speed = config.projectileSpeed;

        Rigidbody projectileRb = projectile1.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = model.StatueTransform.forward * config.projectileSpeed;
        }
    }


}