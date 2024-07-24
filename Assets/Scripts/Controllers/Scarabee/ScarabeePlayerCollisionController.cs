using UnityEngine;

public class ScarabeePlayerCollisionController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;
    
    private ScarabeeDestructionController destructionController;

    public void Initialize(ScarabeeModel model, ScarabeeView view, ScarabeeDestructionController destructionController)
    {
        this.model = model;
        this.view = view;
        this.destructionController = destructionController;
    }

    public void HandlePlayerCollision(GameObject player)
    {
        destructionController.DestroyObject(player);
        destructionController.DestroyProjectile(gameObject);
        AudioManager.instance.PlaySFX("Standbeeld neer");

        GameObject laser = GameObject.Find("Line");
        if (laser != null)
        {
            Destroy(laser);
        }
    }
}
