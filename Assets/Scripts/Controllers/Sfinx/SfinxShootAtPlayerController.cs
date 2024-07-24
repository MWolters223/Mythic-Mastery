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

        ScarabeeModel scarabee = projectile.GetComponent<ScarabeeModel>();
        scarabee.maxReflectCount = config.maxReflectCount;
        scarabee.reflectCount = 0;
        scarabee.speed = config.projectileSpeed;

        Rigidbody projectileRb = scarabee.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = model.StatueTransform.forward * config.projectileSpeed;
        }
    }


}