using Godot;

public partial class MainMenu : Control
{
	[Export] private Button playButton;
	[Export] private Button quitGameButton;
	
	[ExportGroup("Difficulty Buttons")]
	[Export] private Button easyButton;
	[Export] private Button normalButton;
	[Export] private Button hardButton;


	public override void _Ready()
	{
		base._Ready();

		playButton.Pressed += GameManager.instance.PlayGame;
		quitGameButton.Pressed += GameManager.instance.QuitGame;
		
		SetupDifficultyButton(easyButton, GameManager.Difficulty.Easy);
		SetupDifficultyButton(normalButton, GameManager.Difficulty.Normal);
		SetupDifficultyButton(hardButton, GameManager.Difficulty.Hard);
	}


	private void SetupDifficultyButton(Button button, GameManager.Difficulty difficulty)
	{
		button.Pressed += () => GameManager.instance.difficulty = difficulty;

		if (GameManager.instance.difficulty == difficulty)
		{
			button.ButtonPressed = true;
			button.GrabFocus();
		}
		
		var colorName = difficulty switch
		{
			GameManager.Difficulty.Easy => "easy_color",
			GameManager.Difficulty.Normal => "normal_color",
			GameManager.Difficulty.Hard => "hard_color",
			_ => "normal_color"
		};
		button.Modulate = GetThemeColor(colorName, "DifficultyColors");
	}
}
