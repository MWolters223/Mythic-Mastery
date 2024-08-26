using UnityEngine;

public abstract class CameraView : MonoBehaviour
{ 
    protected CameraModel model;

    public virtual void UpdateCameraPosition(Vector3 newPosition)
    {
        model.Camera.transform.position = newPosition;
    }

    public abstract void UpdateCameraRotation(Quaternion newRotation);

    public virtual void Initialize(CameraModel model)
    {
        this.model = model;
    }
}