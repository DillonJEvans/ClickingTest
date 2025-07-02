using Godot;

public partial class ScoreKeeper : Node3D
{
    [Signal]
    public delegate void ScoreChangedEventHandler(int multiplier);
    
    
    private int multiplier = 1;


    public void ResetMultiplier()
    {
        multiplier = 1;
    }

    public void CollectCoin()
    {
        GameManager.instance.score += multiplier;
        EmitSignal(SignalName.ScoreChanged, multiplier);
        multiplier *= 2;
    }
}
