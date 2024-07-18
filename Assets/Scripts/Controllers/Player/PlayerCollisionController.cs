using System;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;

    private PlayerConfig config;

    public void Initialize(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;
        this.config = model.config;
    }

    public void PreventSfinxCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, config.avoidEnemyRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") && collider.gameObject.name.Contains("sfinx", StringComparison.OrdinalIgnoreCase))
            {
                if (Physics.ComputePenetration(
                    GetComponent<Collider>(), transform.position, transform.rotation,
                    collider, collider.transform.position, collider.transform.rotation,
                    out Vector3 direction, out float distance))
                {
                    Vector3 targetPosition = transform.position + direction * (distance + 1f);
                    view.SetPosition(Vector3.SmoothDamp(transform.position, targetPosition, ref model.velocity, config.smoothTime));
                }
            }
        }
    }
}
