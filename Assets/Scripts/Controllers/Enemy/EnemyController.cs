using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject projectilePrefab;

    public NavMeshAgent navMeshAgent;
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
        view = GetComponent<EnemyView>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        aimAtPlayerController = gameObject.AddComponent<AimAtPlayerController>();
        collisionController = gameObject.AddComponent<CollisionController>();
        followPlayerController = gameObject.AddComponent<EnemyMovementController>();
        shootAtPlayerController = gameObject.AddComponent<ShootAtPlayerController>();

        aimAtPlayerController.Initialise(model, view);
        collisionController.Initialize(model, view);
        followPlayerController.Initialize(navMeshAgent, model, view); 
        shootAtPlayerController.Initialize(model, view);

        InitializeView();
    }

    private void InitializeView()
    {
        if (!enemyPrefab || !projectilePrefab)
        {
            Debug.LogError("EnemyController: EnemyPrefab or ProjectilePrefab is not assigned.");
            return;
        }

        Transform statueTransform = enemyPrefab.transform.Find("standbeeld");
        Transform diskTransform = enemyPrefab.transform.Find("grondplaat");

        if (statueTransform == null || diskTransform == null)
        {
            Debug.LogError("EnemyController: Statue or Disk part not found in the god prefab.");
            return;
        }

        view.Initialize(statueTransform, diskTransform);
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (!ObjectWithTagInView("Player"))
            {
                aimAtPlayerController.AimAtPlayer();
                followPlayerController.MoveToPlayer();

                if (model.cooldownTimer > 0)
                {
                    model.cooldownTimer -= Time.deltaTime;
                }
                else
                {
                    if (ObjectWithTagInView("Enemy"))
                    {
                        shootAtPlayerController.ShootAtPlayer();
                        AudioManager.instance.PlaySFX("Scarabee afgevuurt");
                    }
                    model.cooldownTimer = model.shootingCooldown;

                }
            }
            else
            {
                view.RotateStatue(model.idleRotationSpeed);
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

    bool ObjectWithTagInView(string tag)
    {

        player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit hit;

        Vector3 direction = player.transform.position - view.statueTransform.position;
        Vector3 raycastStart = new Vector3(view.statueTransform.position.x, model.shootingHeight, view.statueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                Debug.DrawRay(raycastStart, direction, Color.green);
                return false;
            }
        }
        Debug.DrawRay(raycastStart, direction, Color.red);


        return true;
    }
}
