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

    public void Initialize(Transform statue, Transform disk, NavMeshAgent navMeshAgent)
    {
        this.statueTransform = statue;
        this.diskTransform = disk;
        this.navMeshAgent = navMeshAgent;
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
}