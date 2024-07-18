using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
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

        view.navMeshAgent.speed = config.speed;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not in scene or player has no tag Player");
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void setRandomPoint()
    {
        NavMeshHit hit;
        if (view.navMeshAgent.remainingDistance <= view.navMeshAgent.stoppingDistance)
        {
            Vector3 randompoint = view.GetRotationPoint() + UnityEngine.Random.insideUnitSphere * config.range;
            Debug.DrawRay(randompoint, Vector3.up, Color.red, 10.0f);

            if (NavMesh.SamplePosition(randompoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                view.SetDestination(randompoint);
            }
        }
    }

    public void MoveToPlayer()
    {
        if (player != null)
        {
            view.navMeshAgent.destination = GetPlayerPosition(); 
        }
        else
        {
            StopAiMovement();
        }
    }

    public void StopAiMovement()
    {
        if (view.navMeshAgent != null)
        {
            view.SetDestination(transform.position);
        }

        Rigidbody aiRigidbody = GetComponent<Rigidbody>();
        if (aiRigidbody != null)
        {
            aiRigidbody.velocity = Vector3.zero;
            aiRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
