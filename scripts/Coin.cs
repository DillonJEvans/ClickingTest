using Godot;

public partial class Coin : Area3D
{
	[Signal] public delegate void CollectedEventHandler();
	
	[Export] private float secondsPerRotation = 3f;

	[Export] private float secondsPerBounce = 2f;
	[Export] private float bounceDistance = 0.0625f;


	private float initialY;
	private float totalTime = 0f;


	public override void _Ready()
	{
		base._Ready();
		
		initialY = Position.Y;
	}
	
	public override void _Process(double dt)
	{
		base._Process(dt);
		var delta = (float)dt;
		totalTime += delta;
		
		RotateY((delta / secondsPerRotation) * Mathf.Tau);
		
		var position = Position;
		var bounce = Mathf.Sin((totalTime / secondsPerBounce) * Mathf.Pi);
		position.Y = initialY + bounceDistance * bounce;
		Position = position;
	}

	public void OnAreaEntered(Area3D area)
	{
		EmitSignal(SignalName.Collected);
		QueueFree();
	}
}
