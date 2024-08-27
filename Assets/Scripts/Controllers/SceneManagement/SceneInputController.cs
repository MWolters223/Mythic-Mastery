using UnityEngine;

public class SceneInputController : MonoBehaviour 
{
    private SceneModel model;
    private SceneView view;

    public void Initialize(SceneModel model, SceneView view)
    {
        this.model = model;
        this.view = view;
        this.view.SetDoorTrigger("tekst fade in");
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !model.EnterIsPressed)
        {
            model.EnterIsPressed = true;
            view.SetDoorTrigger("tekst fade out");
        }
    }
}
