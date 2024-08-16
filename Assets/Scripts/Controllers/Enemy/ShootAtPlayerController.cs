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

    void Update()
    {
        if (model.cooldownTimer > 0)
        {
            model.cooldownTimer -= Time.deltaTime;
        }
    }

    public void ShootAtPlayer()
    {
        if (player != null && model.cooldownTimer <= 0)
        {
            if (!ObjectWithTagInView("Enemy"))
            {
                ShootProjectile();
                AudioManager.instance.PlaySFX("Scarabee afgevuurt");
                model.cooldownTimer = config.shootingCooldown;
            }
        }
    }

    private void ShootProjectile()
    {
        Transform statueTransform = model.statueTransform;
        Vector3 projectileSpawnPosition = statueTransform.position + statueTransform.forward * config.projectileRadius + new Vector3(0, config.shootingHeight, 0);

        GameObject projectile = Instantiate(config.projectilePrefab, projectileSpawnPosition, statueTransform.rotation);

        ScarabeeModel projectileScript = projectile.GetComponent<ScarabeeModel>();
        projectileScript.maxReflectCount = config.maxReflectCount;
        projectileScript.reflectCount = 0;
        projectileScript.speed = config.projectileSpeed;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.velocity = statueTransform.forward * config.projectileSpeed;
        }
    }

    public bool ObjectWithTagInView(string tag)
    {
        RaycastHit hit;

        Vector3 direction = player.transform.position - view.statueTransform.position;
        Vector3 raycastStart = new Vector3(view.statueTransform.position.x, config.shootingHeight, view.statueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                Debug.DrawRay(raycastStart, direction, Color.green);
                return true;
            }
        }
        Debug.DrawRay(raycastStart, direction, Color.red);

        return false;
    }
}
