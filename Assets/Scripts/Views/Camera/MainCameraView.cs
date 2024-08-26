using UnityEngine;

public class MainCameraView : CameraView
{
    private float smoothRotationSpeed = 5f;

    public override void UpdateCameraRotation(Quaternion newRotation)
    {
        // Smoothly interpolate the camera's current rotation to the new rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, smoothRotationSpeed * Time.deltaTime);
    }
}