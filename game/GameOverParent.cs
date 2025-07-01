using Godot;

public partial class GameOverParent : Control
{
	[Export] private PackedScene gameOverScene;


	public override void _Ready()
	{
		base._Ready();

		ProcessMode = ProcessModeEnum.WhenPaused;
	}
	
	
	private void GameOver()
	{
		GetTree().Paused = true;
		var gameOver = gameOverScene.Instantiate();
		AddChild(gameOver);
	}
}
