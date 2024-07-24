using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;

    private PlayerConfig config; 

    public void Initialize(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;
        this.config = model.config;
    }

    public void HandleShooting(Vector3 direction)
    {
        if (direction.magnitude < 1f || direction == Vector3.zero)
            return;

        ShootProjectile(direction);
    }

    private void ShootProjectile(Vector3 direction)
    {
        direction.y = 0;
        GameObject projectileObj = SpawnProjectile(direction);
        Rigidbody rb = projectileObj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = direction * config.projectileSpeed;
        }
    }

    private GameObject SpawnProjectile(Vector3 direction)
    {
        if (config.projectilePrefab == null)
        {
            Debug.LogError("PlayerShootingController: Projectile prefab is not assigned.");
            return null;
        }

        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 projectilePosition = view.StatueTransform.position + view.StatueTransform.forward * config.projectileRadius + new Vector3(0, config.shootingHeight, 0);
        GameObject projectileObj = Instantiate(config.projectilePrefab, projectilePosition, rotation);
        ConfigureProjectile(projectileObj);
        return projectileObj;
    }

    private void ConfigureProjectile(GameObject projectileObj)
    {
        ScarabeeModel projectile = projectileObj.GetComponent<ScarabeeModel>();
        projectile.maxReflectCount = config.maxReflectCount;
        projectile.speed = config.projectileSpeed;
    }
}
