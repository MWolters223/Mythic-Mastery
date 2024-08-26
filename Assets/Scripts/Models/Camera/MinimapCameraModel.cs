using UnityEngine;

public class MinimapCameraModel : CameraModel
{
    public float OrthographicSize
    {
        get { return Camera.orthographicSize; }
        set { Camera.orthographicSize = value; }
    }

    public override void Initialize()
    {
        base.Initialize();

        Camera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        if (Camera == null)
        {
            Debug.LogError("No GameObject with tag 'MiniMapCamera' found.");
        }

        Offset = new Vector3(0, 100, 0);
        SmoothSpeed = 0.2f;
        OrthographicSize = 100f;
    }
}
