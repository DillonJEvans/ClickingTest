using Godot;

public partial class TimeLimit : Timer
{
	public override void _Ready()
	{
		base._Ready();

		WaitTime = DifficultySettings.instance.GetTimeLimit();
		
		Start();
	}
}
