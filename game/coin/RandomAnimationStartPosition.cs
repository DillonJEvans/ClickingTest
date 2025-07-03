using Godot;

public partial class RandomAnimationStartPosition : AnimationPlayer
{
	public override void _Ready()
	{
		base._Ready();
		
		// The AnimationPlayer might not have set its current animation.
		Callable.From(RandomizePosition).CallDeferred();
	}


	private void RandomizePosition()
	{
		Seek(GD.Randf() * CurrentAnimationLength);
	}
}
