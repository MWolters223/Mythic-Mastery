using UnityEngine;

public class ScarabeeScoreController : MonoBehaviour
{
    private ScarabeeModel model;
    private ScarabeeView view;

    public void Initialize(ScarabeeModel model, ScarabeeView view)
    {
        this.model = model;
        this.view = view;
        model.LoadScores();
    }

    public void AddPoints(int points)
    {
        model.AddPoints(points);
    }

    public int GetPoints()
    {
        return model.Points;
    }

    public int GetSessionPoints()
    {
        return model.SessionPoints;
    }
}
