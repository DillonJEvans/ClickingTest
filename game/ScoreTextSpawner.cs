using Godot;

public partial class ScoreTextSpawner : Node3D
{
    [Export] private PackedScene scoreTextScene;
    [Export] private Node3D player;
    [Export] private Vector3 spawnOffset = Vector3.Up;


    public override void _Ready()
    {
        base._Ready();

        player ??= GetNode("../Player") as Node3D;
    }


    private void SpawnScoreText(int scoreChange)
    {
        if (scoreTextScene.Instantiate() is not Label3D scoreText)
        {
            return;
        }
        
        // Name.
        scoreText.Text = $"+{scoreChange}";
        scoreText.Position = player.Position + spawnOffset;
        AddChild(scoreText);
    }
}
