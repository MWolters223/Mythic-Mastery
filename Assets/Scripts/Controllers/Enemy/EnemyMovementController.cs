using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private EnemyModel model;
    private EnemyView view;

    public void Initialize(NavMeshAgent agent, EnemyModel model, EnemyView view)
    {
        this.agent = agent;
        this.model = model; 
        this.view = view;
        agent.speed = model.speed;

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
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randompoint = view.GetRotationPoint() + UnityEngine.Random.insideUnitSphere * model.range;
            Debug.DrawRay(randompoint, Vector3.up, Color.red, 10.0f);

            if (NavMesh.SamplePosition(randompoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(randompoint);
            }
        }
    }

    public void MoveToPlayer()
    {
        if (player != null)
        {
            agent.destination = GetPlayerPosition(); 
        }
        else
        {
            StopAiMovement();
        }
    }

    public void StopAiMovement()
    {
        if (agent != null)
        {
            agent.destination = transform.position;
        }

        Rigidbody aiRigidbody = GetComponent<Rigidbody>();
        if (aiRigidbody != null)
        {
            aiRigidbody.velocity = Vector3.zero;
            aiRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
