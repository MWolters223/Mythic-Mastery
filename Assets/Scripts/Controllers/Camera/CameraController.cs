using UnityEditor.SceneManagement;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CameraModel mainCameraModel;
    private CameraModel minimapCameraModel;
    private CameraView mainCameraView;
    private CameraView minimapCameraView;

    private CameraFollowPlayerController cameraFollowPlayerController;

    void Start()
    {
        mainCameraModel = GetComponent<MainCameraModel>();
        mainCameraModel.Initialize();

        minimapCameraModel = GetComponent<MinimapCameraModel>();
        minimapCameraModel.Initialize();

        mainCameraView = GetComponent<MainCameraView>();  
        mainCameraView.Initialize(mainCameraModel);

        minimapCameraView = GetComponent<MinimapCameraView>();
        minimapCameraView.Initialize(minimapCameraModel);

        cameraFollowPlayerController = gameObject.AddComponent<CameraFollowPlayerController>();
        cameraFollowPlayerController.Initialize(mainCameraModel, minimapCameraModel, mainCameraView, minimapCameraView);
    }

    private void LateUpdate()
    {
        cameraFollowPlayerController.FollowPlayer();
    }
}
