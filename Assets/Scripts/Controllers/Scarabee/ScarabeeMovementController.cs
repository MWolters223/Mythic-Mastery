using UnityEngine;

public class ScarabeeMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private ScarabeeModel model;
    private ScarabeeView view;

    public Vector3 LastVelocity { get; private set; }
    public Vector3 Direction { get; private set; }
    public float CurSpeed { get; private set; }

    public void Initialize(ScarabeeModel model, ScarabeeView view)
    {
        this.rb = gameObject.GetComponent<Rigidbody>();
        this.model = model;
        this.view = view;
    }

    public void UpdateMovement()
    {
        LastVelocity = rb.velocity;
    }

    public void ReflectMovement(Collision collision)
    {
        CurSpeed = LastVelocity.magnitude;
        Direction = Vector3.Reflect(LastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = Direction * Mathf.Max(CurSpeed, 0);
        transform.rotation = Quaternion.LookRotation(Direction);
    }
}