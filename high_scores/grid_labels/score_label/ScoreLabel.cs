using Godot;

public partial class ScoreLabel : Label
{
	public void SetScore(int score)
	{
		Text = score.ToString();
	}
}
