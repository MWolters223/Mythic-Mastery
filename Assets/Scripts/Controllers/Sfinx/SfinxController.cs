using UnityEngine; 

public class SfinxController : MonoBehaviour
{
    private GameObject player;

    private SfinxModel model;
    private SfinxView view;
    private SfinxAimAtPlayerController aimAtPlayerController;
    private SfinxShootAtPlayerController shootAtPlayerController;

    private SfinxConfig config;

    void Start()
    {
        model = GetComponent<SfinxModel>();
        this.config = model.SfinxConfig;

        view = GetComponent<SfinxView>();
        InitializeView();

        aimAtPlayerController = gameObject.AddComponent<SfinxAimAtPlayerController>();
        shootAtPlayerController = gameObject.AddComponent<SfinxShootAtPlayerController>();

        aimAtPlayerController.Initialize(model, view);
        shootAtPlayerController.Initialize(model, view); 
    }

    private void InitializeView()
    {
        GameObject godPrefab = model.GodPrefab;

        if (!config.projectilePrefab)
        {
            Debug.LogError("config not loaded");
            return;
        }

        if (!godPrefab)
        {
            Debug.LogError("SfinxController: GodPrefab or ProjectilePrefab is not assigned.");
            return;
        }

        Transform statueTransform = godPrefab.transform.Find("standbeeld");
        Transform diskTransform = godPrefab.transform.Find("grondplaat");

        if (statueTransform == null || diskTransform == null)
        {
            Debug.LogError("SfinxController: Statue or Disk part not found in the god prefab.");
            return;
        }
        view.Initialize(statueTransform, diskTransform);
    }

        void Update()
        {
            model.Player = GameObject.FindGameObjectWithTag("Player");

            if (model.Player != null)
            {
                if (ObstacleBetween("Player"))
                {
                    aimAtPlayerController.AimAtPlayer();

                    if (model.cooldownTimer > 0)
                    {
                        model.cooldownTimer -= Time.deltaTime;
                    }
                    else
                    {
                        if (!ObstacleBetween("Enemy"))
                        {
                            shootAtPlayerController.ShootProjectile();
                            AudioManager.instance.PlaySFX("Scarabee afgevuurt");
                        }
                        model.cooldownTimer = config.shootingCooldown;
                    }
                }
                else
                {
                    view.RotateStatue(model.StatueTransform, config.idleRotationSpeed);
                }
            }
    }

    bool ObstacleBetween(string tag)
    {
        RaycastHit hit;

        Vector3 direction = model.Player.transform.position - model.StatueTransform.position;
        Vector3 raycastStart = new Vector3(model.StatueTransform.position.x, config.shootingHeight, model.StatueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                return true;
            }
        }

        return false; 
    }
}