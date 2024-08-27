using UnityEngine;
using UnityEngine.UI;

public class SceneView : MonoBehaviour
{
    public Animator DoorAnimator;
    public Animator MusicAnimator;
    public GameObject LoadingPanel;
    public Slider LoadingBar;

    public void SetDoorTrigger(string trigger)
    {
        DoorAnimator.SetTrigger(trigger);
    }

    public void SetMusicTrigger(string trigger)
    {
        MusicAnimator.SetTrigger(trigger);
    }

    public void ShowLoadingPanel(bool show)
    {
        LoadingPanel.SetActive(show);
    }

    public void UpdateLoadingBar(float progress)
    {
        LoadingBar.value = progress;
    }
}
