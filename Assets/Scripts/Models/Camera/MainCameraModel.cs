using UnityEngine;

public class MainCameraModel : CameraModel
{
    public override void Initialize()
    {
        base.Initialize();

        Camera = GetComponent<Camera>();
        if (Camera == null)
        {
            Debug.LogError("MainCameraModel: No Camera component found on the current GameObject.");
        }

        Offset = new Vector3(0, 100, -40);
        SmoothSpeed = 0.1f;
    }
}
