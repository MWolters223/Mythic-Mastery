using UnityEngine;

public class SfinxAimAtPlayerController : MonoBehaviour
{
    private GameObject player;
    private SfinxModel model;
    private SfinxView view;

    private SfinxConfig config;

    public void Initialize(SfinxModel model, SfinxView view)
    {
        this.model = model;
        this.view = view;
        this.config = model.SfinxConfig;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not in scene or player has no tag Player");
        }
    }

    public void AimAtPlayer()
    {
        if (player != null)
        {
            Vector3 rotationPoint = view.GetRotationPoint();

            Vector3 direction = player.transform.position - rotationPoint;
            direction.y = 0;

            Quaternion rotation = Quaternion.LookRotation(direction);
            view.StatueTransform.rotation = Quaternion.Slerp(view.StatueTransform.rotation, rotation, Time.deltaTime * config.rotationSpeed);
        }
    }
}