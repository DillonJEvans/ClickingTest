using Godot;

public partial class GameOver : Control
{
	[Export] private Button playAgainButton;
	[Export] private Button mainMenuButton;
	[Export] private Label scoreLabel;
	[Export] private HighScores highScores;


	public override void _Ready()
	{
		base._Ready();

		scoreLabel.Text = $"Score: {GameManager.instance.score}";
		highScores.SaveHighScore();
		
		playAgainButton.Pressed += GameManager.instance.PlayGame;
		mainMenuButton.Pressed += GameManager.instance.MainMenu;
	}
}
