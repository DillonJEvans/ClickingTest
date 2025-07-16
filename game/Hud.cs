using Godot;

public partial class Hud : Control
{
	[Export] private Label scoreLabel;
	[Export] private Label timeRemainingLabel;
	[Export] private Timer timeLimit;
	
	[ExportGroup("Low Time Animation")]
	[Export] private AnimationPlayer animationPlayer;
	[Export] private string animationName = "low_time";


	private Animation animation;
	

	public override void _Ready()
	{
		base._Ready();

		scoreLabel ??= GetNode<Label>("Score");
		timeRemainingLabel ??= GetNode<Label>("TimeRemaining");
		animation = animationPlayer?.GetAnimation(animationName);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		UpdateTimeRemaining();
	}


	private void UpdateTimeRemaining()
	{
		// Display the time left with 0 decimal places until it gets below 10 seconds,
		// then display it with one decimal place.
		// Due to rounding, if you display a number in the range [9.95, 10) with 1 decimal place, it is "10.0".
		// To get the desired behavior, we check for 9.95 seconds instead of 10.
		var decimalPlaces = timeLimit.TimeLeft >= 9.95f ? 0 : 1;
		timeRemainingLabel.Text = timeLimit.TimeLeft.ToString($"N{decimalPlaces}");
		
		if (animation is not null && !animationPlayer.IsPlaying() && timeLimit.TimeLeft <= animation.Length)
		{
			animationPlayer.Play(animationName);
		}
	}
	
	private void UpdateScore()
	{
		scoreLabel.Text = GameManager.instance.score.ToString();
	}
}
