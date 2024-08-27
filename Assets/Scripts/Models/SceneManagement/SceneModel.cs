using UnityEngine;

public class SceneModel : MonoBehaviour
{
    public int TransistionSceneIndex { get; set; } = 7;
    public int FirstLevelIndex { get; set; } = 8;
    public int LastMenuIndex { get; set; } = 5;

    public bool EnterIsPressed { get; set; } = false;

    public bool IsMenuScene(int sceneBuilderIndex)
    {
        return sceneBuilderIndex <= LastMenuIndex;
    }

    public bool IsTransitionScene(int sceneBuilderIndex)
    {
        return sceneBuilderIndex == TransistionSceneIndex;
    }

    public bool IsLevelChange(int sceneBuilderIndex)
    {
        return sceneBuilderIndex > FirstLevelIndex;
    }


}