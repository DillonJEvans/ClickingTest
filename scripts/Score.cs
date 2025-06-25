using Godot;

public partial class Score : Label
{
	private void UpdateScore(int score)
	{
		Text = score.ToString();
	}
}
