using Godot;

public partial class TimeLimit : Timer
{
	[Export] private double easyTime = 60f;
	[Export] private double normalTime = 50f;
	[Export] private double hardTime = 40f;
	
	
	public override void _Ready()
	{
		base._Ready();

		WaitTime = GameManager.instance.difficulty switch
		{
			GameManager.Difficulty.Easy => easyTime,
			GameManager.Difficulty.Normal => normalTime,
			GameManager.Difficulty.Hard => hardTime,
			_ => normalTime
		};
		
		Start();
	}
}
