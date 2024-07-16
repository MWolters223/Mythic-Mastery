using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private EnemyModel model;
    private EnemyView view;

    public void Initialize(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;
    }

    public void PreventSfinxCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, model.avoidEnemyRadius);

        foreach (Collider collider in colliders)
        {
            bool isEnemy = collider.gameObject.CompareTag("Enemy");
            bool isSfinx = collider.gameObject.name.Contains("sfinx", StringComparison.OrdinalIgnoreCase);

            if (isEnemy && isSfinx)
            {
                Vector3 direction;
                float distance;

                if (Physics.ComputePenetration(
                    GetComponent<Collider>(), transform.position, transform.rotation,
                    collider, collider.transform.position, collider.transform.rotation,
                    out direction, out distance))
                {
                    float offset = 1f;
                    Vector3 targetPosition = transform.position + direction * (distance + offset);
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref model.velocity, model.smoothTime);
                }
            }
        }
    }
}
