using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    private EnemyModel model; 
    private EnemyView view;
    private AimAtPlayerController aimAtPlayerController; 
    private CollisionController collisionController;
    private EnemyMovementController followPlayerController;
    private ShootAtPlayerController shootAtPlayerController;

    void Start()
    {
        model = GetComponent<EnemyModel>();
        model.Initialize();

        view = GetComponent<EnemyView>();
        view.Initialize(model);

        aimAtPlayerController = gameObject.AddComponent<AimAtPlayerController>();
        collisionController = gameObject.AddComponent<CollisionController>();
        followPlayerController = gameObject.AddComponent<EnemyMovementController>();
        shootAtPlayerController = gameObject.AddComponent<ShootAtPlayerController>();

        aimAtPlayerController.Initialize(model, view);
        collisionController.Initialize(model, view);
        followPlayerController.Initialize(model, view); 
        shootAtPlayerController.Initialize(model, view);

        EnemyManagerController.Instance.EnemyManagerRegistrationController.RegisterEnemy();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (view.ObjectWithTagInView("Player"))
            {
                aimAtPlayerController.AimAtPlayer();
                followPlayerController.MoveToPlayer();

                if (model.cooldownTimer <= 0)
                {
                    shootAtPlayerController.ShootAtPlayer();
                }
            }
            else
            {
                view.RotateStatue(model.config.idleRotationSpeed);
                followPlayerController.setRandomPoint();
            }
        }
        else
        {
            followPlayerController.StopAiMovement();
        }
    }

    void FixedUpdate()
    {
        collisionController.PreventSfinxCollision();
    }
}
