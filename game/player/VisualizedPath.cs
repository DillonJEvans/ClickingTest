using Godot;

public partial class VisualizedPath : MeshInstance3D
{
	private Player player;
	private BoxMesh mesh;


	public override void _Ready()
	{
		base._Ready();
		
		player = GetParent() as Player;
		mesh = Mesh as BoxMesh;
	}
	
	public override void _Process(double delta)
	{
		base._Process(delta);

		var target = player.GetMousePositionOnMovementPlane();
		if (target is null)
		{
			return;
		}

		// Position.
		var localEndPoint = target.Value - player.Position;
		Position = localEndPoint / 2f;
		
		// Rotation.
		if (GlobalPosition.IsEqualApprox(target.Value))
		{
			return;
		}
		LookAt(target.Value);
		
		// Width and length.
		var angle = Mathf.Abs(Rotation.Y) % (Mathf.Pi / 2f);
		var width = Mathf.Sqrt2 * Mathf.Sin(angle + Mathf.Pi / 4f);
		
		var size = mesh.Size;
		size.Z = localEndPoint.Length();
		size.X = width;
		mesh.Size = size;
	}
}
