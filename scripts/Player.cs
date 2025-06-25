using Godot;

public partial class Player : Area3D
{
	[Export] private Camera3D camera;
	[Export] private float speed = 5f;
	
	private Vector3 target;
	private Plane movementPlane;
	
	
	public override void _Ready()
	{
		base._Ready();

		if (camera is null)
		{
			GD.PrintErr("The player doesn't have a camera.");
			SetProcess(false);
		}
		
		target = Position;
		movementPlane = new Plane(Vector3.Up, Position);
	}
	
	public override void _Process(double dt)
	{
		base._Process(dt);

		var delta = (float)dt;
		
		HandleInput();
		Position = Position.Lerp(target, speed * delta * 2);
	}


	private void HandleInput()
	{
		if (!Input.IsActionPressed("move_unit"))
		{
			return;
		}
		
		var mousePos = GetViewport().GetMousePosition();
		var rayOrigin = camera.ProjectRayOrigin(mousePos);
		var rayNormal = camera.ProjectRayNormal(mousePos);

		var worldPos = movementPlane.IntersectsRay(rayOrigin, rayNormal);
		if (worldPos is null)
		{
			return;
		}

		target = worldPos.Value;
	}
}
