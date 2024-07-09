using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Transform StatueTransform { get; private set; }
    public Transform DiskTransform { get; private set; }
    public LineRenderer LineRenderer { get; private set; }

    public void Initialize(Transform statueTransform, Transform diskTransform, LineRenderer lineRenderer)
    {
        StatueTransform = statueTransform;
        DiskTransform = diskTransform;
        LineRenderer = lineRenderer;
    }

    public void UpdateLaser(Vector3 startPosition, Vector3 endPosition)
    {
        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, startPosition);
        LineRenderer.SetPosition(1, endPosition);
    }

    public void DisableLaser()
    {
        LineRenderer.positionCount = 0;
    }

    public void RotateStatue(Vector3 point, Vector3 axis, float angle)
    {
        StatueTransform.RotateAround(point, axis, angle);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetVelocity(Vector3 velocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.rotation = rotation;
        }
    }

    public void Move(Vector3 position, Quaternion rotation)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.MovePosition(position);
            rb.MoveRotation(rotation);
        }
    }
}