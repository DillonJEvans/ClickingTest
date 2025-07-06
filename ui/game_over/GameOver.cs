using Godot;

public partial class GameOver : Control
{
	[Export] private Button playAgainButton;
	[Export] private Button mainMenuButton;
	[Export] private Label scoreLabel;


	public override void _Ready()
	{
		base._Ready();

		scoreLabel.Text = string.Format(scoreLabel.Text, GameManager.instance.score);
		HighScores.instance.SaveHighScore();
		
		playAgainButton.Pressed += GameManager.instance.PlayGame;
		mainMenuButton.Pressed += GameManager.instance.MainMenu;
	}
}
