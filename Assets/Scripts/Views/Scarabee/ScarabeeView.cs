using UnityEngine;

public class ScarabeeView : MonoBehaviour
{
    private Rigidbody rb;
    private ScarabeeModel model;

    public void Initialize(ScarabeeModel model)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        this.model = model;
    }

    public void SetRotation(Vector3 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}