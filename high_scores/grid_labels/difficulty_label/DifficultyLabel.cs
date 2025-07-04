using Godot;

public partial class DifficultyLabel : Label
{
	[Export] private Color easyColor = Colors.Green;
	[Export] private Color normalColor = Colors.Yellow;
	[Export] private Color hardColor = Colors.Red;
	
	
	public void SetDifficulty(GameManager.Difficulty difficulty)
	{
		Text = difficulty.ToString();

		Modulate = difficulty switch
		{
			GameManager.Difficulty.Easy => easyColor,
			GameManager.Difficulty.Normal => normalColor,
			GameManager.Difficulty.Hard => hardColor,
			_ => normalColor
		};
	}
}
