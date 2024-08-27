using System.Collections;
using UnityEngine;

public class SceneAnimationController : MonoBehaviour 
{
    private SceneModel model;
    private SceneView view;

    public void Initialize(SceneModel model, SceneView view)
    {
        this.model = model;
        this.view = view;
    }
    public IEnumerator OpenDoorAnimation()
    {
        Debug.Log("Opening door...");
        view.SetDoorTrigger("door open");

        while (!view.DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door open") || view.DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Door open animation completed.");
    }

    public IEnumerator CloseDoorAnimation()
    {
        Debug.Log("Closing door...");
        view.SetDoorTrigger("door close");

        while (!view.DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door close") || view.DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Debug.Log("Door close animation completed.");
    }
}
