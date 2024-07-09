using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Transform StatueTransform { get; private set; }
    public Transform DiskTransform { get; private set; }
    private LineRenderer lineRenderer;

    public void Initialize(Transform statueTransform, Transform diskTransform, LineRenderer lineRenderer)
    {
        StatueTransform = statueTransform;
        DiskTransform = diskTransform;
        this.lineRenderer = lineRenderer;
    }

    public void Move(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetVelocity(Vector3 velocity)
    {
        GetComponent<Rigidbody>().velocity = velocity;
    }

    public void RotateStatue(Vector3 point, Vector3 axis, float angle)
    {
        StatueTransform.RotateAround(point, axis, angle);
    }

    public void UpdateLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
