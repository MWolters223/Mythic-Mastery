using Unity.VisualScripting.FullSerializer;
using UnityEngine; 

public class SfinxView : MonoBehaviour
{
    [HideInInspector]
    public Transform StatueTransform;

    [HideInInspector]
    public Transform DiskTransform;

    private SfinxModel model;

    public void Initialize(SfinxModel model)
    {
        this.model = model;

        GameObject godPrefab = model.GodPrefab;
        StatueTransform = godPrefab.transform.Find("standbeeld");
        DiskTransform = godPrefab.transform.Find("grondplaat");
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

    public bool ObjectWithTagInView(string tag)
    {
        RaycastHit hit;

        Vector3 direction = model.Player.transform.position - model.StatueTransform.position;
        Vector3 raycastStart = new Vector3(model.StatueTransform.position.x, model.SfinxConfig.shootingHeight, model.StatueTransform.position.z);

        if (Physics.Raycast(raycastStart, direction, out hit))
        {
            if (hit.collider.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }
}