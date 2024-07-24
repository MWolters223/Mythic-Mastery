using System.Linq;
using UnityEngine;

public class ScarabeeCollisionController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;

    private ScarabeeEnemyCollisionController enemyCollisionController;
    private ScarabeePlayerCollisionController playerCollisionController;
    private ScarabeeDestructionController destructionController;

    private Rigidbody rb;

    public void Initialize(ScarabeeModel model, ScarabeeView view, ScarabeeEnemyCollisionController enemyCollisionController, ScarabeePlayerCollisionController playerCollisionController, ScarabeeDestructionController destructionController)
    {
        this.model = model;
        this.view = view; 
        this.enemyCollisionController = enemyCollisionController;
        this.playerCollisionController = playerCollisionController;
        this.destructionController = destructionController;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {

        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);

        Vector3 contact = contacts[0].normal;

        GameObject collidedObject = collision.gameObject;
        string collidedTag = collidedObject.tag;

        switch (collidedTag)
        {
            case "Scarabee":
                destructionController.DestroyObject(collidedObject);
                destructionController.DestroyProjectile(gameObject);
                break;

            case "Player":
                if (!model.isIgnoringPlayer)
                {
                    playerCollisionController.HandlePlayerCollision(collidedObject);
                }
                break;

            case "Enemy":
                enemyCollisionController.HandleEnemyCollision(collidedObject);
                break;

            default:
                HandleDefaultCollision(contact);
                break;
        }
    }

    private void HandleDefaultCollision(Vector3 collision)
    {
        if (model.reflectCount >= model.maxReflectCount)
        {
            AudioManager.instance.PlaySFX("Scarabee raakt muur");
            destructionController.DestroyProjectile(gameObject);
        }
        else
        {
            model.reflectCount++;
            AudioManager.instance.PlaySFX("Scarabee raakt muur");
            view.Bounce(collision); 
        }
    }
}
