using Godot;

public partial class AddHighScore : Control
{
	[Export] private HighScores highScores;
	
	
	public override void _Ready()
	{
		base._Ready();

		var row = highScores.SaveHighScore();
		if (row < 0)
		{
			return;
		}

		for (var i = 0; i < 4; i++)
		{
			highScores.grid.GetChild<Label>(row * 4 + i).Modulate = Colors.Green;
		}
	}
}
