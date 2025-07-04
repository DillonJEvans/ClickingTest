using Godot;

public partial class AddHighScore : Control
{
	[Export] private HighScores highScores;
	
	
	public override void _Ready()
	{
		base._Ready();

		highScores.SaveHighScore();
	}
}
