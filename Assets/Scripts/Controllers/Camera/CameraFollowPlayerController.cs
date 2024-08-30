using UnityEngine;

public class CameraFollowPlayerController : MonoBehaviour
{
    private CameraModel mainCameraModel;
    private CameraModel minimapCameraModel;
    private CameraView mainCameraView;
    private CameraView minimapCameraView;

    public void Initialize(CameraModel mainCameraModel, CameraModel minimapCameraModel, CameraView mainCameraView, CameraView minimapCameraView)
    {
        this.mainCameraModel = mainCameraModel;
        this.minimapCameraModel = minimapCameraModel;
        this.mainCameraView = mainCameraView;
        this.minimapCameraView = minimapCameraView;
    }


    private void UpdateCamera(CameraModel cameraModel, CameraView cameraView)
    {
        if (cameraModel != null && cameraView != null && cameraModel.Player != null)
        {
            Vector3 desiredPosition = cameraModel.Player.position + cameraModel.Offset;
            Vector3 smoothedPosition = Vector3.Lerp(cameraModel.Camera.transform.position, desiredPosition, cameraModel.SmoothSpeed);
            cameraView.UpdateCameraPosition(smoothedPosition);
        }
    }

    public void FollowPlayer()
    {
        UpdateCamera(mainCameraModel, mainCameraView);
        UpdateCamera(minimapCameraModel, minimapCameraView);
    }
}
