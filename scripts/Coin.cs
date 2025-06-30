using Godot;

public partial class Coin : Area3D
{
	[Signal] public delegate void CollectedEventHandler();

	
	public void OnAreaEntered(Area3D area)
	{
		EmitSignal(SignalName.Collected);
		QueueFree();
	}
}
