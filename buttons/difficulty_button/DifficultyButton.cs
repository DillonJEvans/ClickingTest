using Godot;

public partial class DifficultyButton : Button
{
	[Export] private GameManager.Difficulty difficulty;


	public override void _Ready()
	{
		base._Ready();

		ButtonPressed = (difficulty == GameManager.instance.difficulty);
	}
	
	
	private void OnPressed() => GameManager.instance.difficulty = difficulty;
}
