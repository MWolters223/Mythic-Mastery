using UnityEngine; 

public class SfinxView : MonoBehaviour
{
    [HideInInspector]
    public Transform StatueTransform;

    [HideInInspector]
    public Transform DiskTransform;

    public void Initialize(Transform statueTransform, Transform diskTransform)
    {
        StatueTransform = statueTransform;
        DiskTransform = diskTransform;
    }

    public Vector3 GetRotationPoint()
    {
        float diskHeight = DiskTransform.localScale.y;
        Vector3 rotationPoint = DiskTransform.position + new Vector3(0, diskHeight / 2, 0);
        return rotationPoint;
    }

    public void RotateStatue(Transform statueTransform, float idleRotationSpeed)
    {
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2) * idleRotationSpeed;
        statueTransform.Rotate(Vector3.up, angle * Time.deltaTime);
    }
}