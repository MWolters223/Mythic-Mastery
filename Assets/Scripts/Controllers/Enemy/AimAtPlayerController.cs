using UnityEngine;

public class AimAtPlayerController : MonoBehaviour
{
    private GameObject player;
    private EnemyModel model;
    private EnemyView view;

    private EnemyConfig config;

    public void Initialise(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;
        this.config = model.config;
    }

    public void AimAtPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 rotationPoint = model.diskTransform.position + new Vector3(0, model.diskTransform.localScale.y / 2, 0);
            Vector3 direction = player.transform.position - rotationPoint;
            direction.y = 0;

            Quaternion rotation = Quaternion.LookRotation(direction);
            model.statueTransform.rotation = Quaternion.Slerp(model.statueTransform.rotation, rotation, Time.deltaTime * config.rotationSpeed);
        }
    }
}
