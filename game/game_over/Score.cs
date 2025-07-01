using Godot;

public partial class Score : Label
{
	public override void _Ready()
	{
		base._Ready();

		Text = $"Score: {GameManager.instance.score}";
	}
}
