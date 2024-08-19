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
        view.Initialize(model);

        aimAtPlayerController = gameObject.AddComponent<SfinxAimAtPlayerController>();
        shootAtPlayerController = gameObject.AddComponent<SfinxShootAtPlayerController>();

        aimAtPlayerController.Initialize(model, view);
        shootAtPlayerController.Initialize(model, view); 
    }
        void Update()
        {
            model.Player = GameObject.FindGameObjectWithTag("Player");

            if (model.Player != null)
            {
                if (view.ObjectWithTagInView("Player"))
                {
                    aimAtPlayerController.AimAtPlayer();

                    if (model.cooldownTimer > 0)
                    {
                        model.cooldownTimer -= Time.deltaTime;
                    }
                    else
                    {
                        if (!view.ObjectWithTagInView("Enemy"))
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
}