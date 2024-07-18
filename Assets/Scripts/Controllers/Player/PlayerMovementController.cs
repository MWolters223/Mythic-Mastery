using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;
    private Rigidbody rb;

    private PlayerConfig config;

    public void Initialize(PlayerModel model, PlayerView view, Rigidbody rb)
    {
        this.model = model;
        this.view = view;
        this.rb = rb;
        this.config = model.config;
    }

    public void HandleMovement(Vector2 movementInput)
    {
        if (movementInput != Vector2.zero)
        {
            Vector3 movement = transform.forward * movementInput.y * config.speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(Vector3.up * movementInput.x * config.rotationSpeed * Time.deltaTime);
            view.Move(rb.position + movement, rb.rotation * rotation);
        }
        else
        {
            view.SetVelocity(Vector3.zero);
        }
    }
}
