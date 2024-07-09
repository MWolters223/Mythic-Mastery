using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;
    private Rigidbody rb;

    public void Initialize(PlayerModel model, PlayerView view, Rigidbody rb)
    {
        this.model = model;
        this.view = view;
        this.rb = rb;
    }

    public void HandleMovement(Vector2 input)
    {
        if (input.x != 0 || input.y != 0)
        {
            Vector3 movement = view.transform.forward * input.y * model.speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(Vector3.up * input.x * model.rotationSpeed * Time.deltaTime);
            view.Move(rb.position + movement, rb.rotation * rotation);
        }
        else
        {
            view.SetVelocity(Vector3.zero);
        }
    }
}
