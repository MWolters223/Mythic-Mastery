using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class PlayerView : MonoBehaviour
{
    [HideInInspector]
    public Transform StatueTransform;

    [HideInInspector]
    public Transform DiskTransform;

    private LineRenderer lineRenderer;

    private PlayerModel playerModel;

    public void Initialize(PlayerModel model)
    {
        playerModel = model;
        StatueTransform = playerModel.statueTransform;
        DiskTransform = playerModel.diskTransform;

        lineRenderer = GameObject.Find("Line")?.GetComponent<LineRenderer>();
    }

    public void Move(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public Vector3 GetRotationPoint()
    {
        float diskHeight = DiskTransform.localScale.y;
        return DiskTransform.position + new Vector3(0, diskHeight / 2, 0);
    }

    public void HandleStatueRotation(Vector3 direction)
    {
        if (direction.magnitude < 1f || direction == Vector3.zero) return;

        Vector3 statueForward = StatueTransform.forward;
        float angle = Vector3.SignedAngle(statueForward, direction, Vector3.up);
        StatueTransform.RotateAround(GetRotationPoint(), Vector3.up, angle);
    }

    public void SetLaserPosition(Vector3 direction, float shootingHeight)
    {
        if (lineRenderer == null) return;

        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Obstacle", "Enemy")))
        {
            Vector3 start = transform.position + new Vector3(0, shootingHeight, 0);
            Vector3 end = hit.point + new Vector3(0, shootingHeight, 0);
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
