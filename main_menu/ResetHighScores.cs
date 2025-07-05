using Godot;

public partial class ResetHighScores : Button
{
	[Export] private HighScores highScores;
	
	
	public override void _Ready()
	{
		base._Ready();
		
		Pressed += Reset;
	}


	public void Reset()
	{
		highScores.ResetHighScores();
	}
}
