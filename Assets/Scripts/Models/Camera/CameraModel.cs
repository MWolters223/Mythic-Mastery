using UnityEngine;

public class CameraModel : MonoBehaviour
{
    protected Vector3 offset = new Vector3(0, 100, -40);

    protected float smoothSpeed = 0.125f;

    protected Transform player;
    protected Camera currentCamera; 

    public Vector3 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public float SmoothSpeed
    {
        get { return smoothSpeed; }
        set { smoothSpeed = value; }
    }

    public Transform Player
    {
        get { return player; }
    }

    public Camera Camera
    { 
        get { return currentCamera; } 
        set { currentCamera = value; }
    }

    public virtual void Initialize()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
}
