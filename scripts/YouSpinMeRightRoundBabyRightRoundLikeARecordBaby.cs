using Godot;

[Tool]
public partial class YouSpinMeRightRoundBabyRightRoundLikeARecordBaby : MeshInstance3D
{
	[Export(PropertyHint.Range, "0,30,0.1")] private float secondsPerQuarterRotation = 5f;
	[Export] private bool reverseRotation = false;


	public override void _Process(double dt)
	{
		base._Process(dt);

		var delta = (float)dt;

		if (secondsPerQuarterRotation == 0f)
		{
			return;
		}
		
		var radiansPerSecond = Mathf.Pi / (2f * secondsPerQuarterRotation);
		if (reverseRotation)
		{
			radiansPerSecond = -radiansPerSecond;
		}
		RotateY(radiansPerSecond * delta);
	}
}
