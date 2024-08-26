using UnityEngine;

public class MinimapCameraView : CameraView
{
    public override void UpdateCameraRotation(Quaternion newRotation)
    {
        // Fixed rotation for the minimap camera
        transform.rotation = Quaternion.Euler(90, 0, 0); // Always look downwards
    }
}
