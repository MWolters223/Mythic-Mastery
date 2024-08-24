using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    [HideInInspector]
    public Transform statueTransform;

    [HideInInspector]
    public Transform diskTransform;

    [HideInInspector]
    public NavMeshAgent navMeshAgent; 

    private EnemyModel EnemyModel;

    public void Initialize(EnemyModel model)
    {
        EnemyModel = model; 
        statueTransform = EnemyModel.statueTransform;
        diskTransform = EnemyModel.diskTransform;
        navMeshAgent = EnemyModel.agent;
    }

    public Vector3 GetRotationPoint()
    {
        float diskHeight = diskTransform.localScale.y;
        Vector3 rotationPoint = diskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    public void RotateStatue(float idleRotationSpeed)
    {
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2) * idleRotationSpeed;
        statueTransform.Rotate(Vector3.up, angle * Time.deltaTime);
    }

    public void SetDestination(Vector3 position)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(position);
        }
    }

    public bool ObjectWithTagInView(string tag)
    {
        RaycastHit hit;

        Vector3 direction = EnemyModel.Player.transform.position - statueTransform.position;
        Vector3 raycastStart = new Vector3(statueTransform.position.x, EnemyModel.config.shootingHeight, statueTransform.position.z);

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