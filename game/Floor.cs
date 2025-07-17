using Godot;

public partial class Floor : MeshInstance3D
{
	[Export] private PackedScene clickScene;
	[Export] private Player player;
	[Export] private float heightOffset;


	public override void _Ready()
	{
		base._Ready();

		player.TargetChanged += CreateClick;
	}


	private void CreateClick()
	{
		var click = clickScene.Instantiate<Node3D>();
		click.Position = player.target + new Vector3(0, heightOffset, 0);
		AddChild(click);
	}
}
