using Godot;

public partial class ScoreKeeper : Node3D
{
    [Signal]
    public delegate void ScoreChangedEventHandler(int scoreChange);
    
    
    private int nextScoreChange = 1;


    public void ResetScoreChange()
    {
        nextScoreChange = 1;
    }

    public void CollectCoin()
    {
        GameManager.instance.score += nextScoreChange;
        EmitSignal(SignalName.ScoreChanged, nextScoreChange);
        nextScoreChange++;
    }
}
