using Godot;

public partial class PointSound : AudioStreamPlayer
{
	[Export] private Vector2 pitchScaleRange = new(0.9f, 1.5f);

	private RandomNumberGenerator random = new();
	
	
	private void PlayRandom()
	{
		PitchScale = random.RandfRange(pitchScaleRange.X, pitchScaleRange.Y);
		Play();
	}
}
