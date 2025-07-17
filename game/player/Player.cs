using Godot;

public partial class Player : Area3D
{
	[Signal]
	public delegate void TargetChangedEventHandler();
	
	
	[Export] private Camera3D camera;
	[Export] private float maxSpeed = 10f;
	[Export] private float acceleration = 10f;
	
	
	public Vector3 target { get; private set; }
	private Plane movementPlane;
	
	private Vector3 velocity = Vector3.Zero;
	
	
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
		
		UpdateTarget();
		
		if (Position == target)
		{
			return;
		}
		
		velocity += CalculateAcceleration() * delta;
		velocity = velocity.LimitLength(maxSpeed);
		Position += velocity * delta;
		
		// The cube always slightly overshoots the target,
		// so once its velocity is going away from the target,
		// it must have reached and past the target.
		if (velocity.Dot(target - Position) < 0f)
		{
			Position = target;
			velocity = Vector3.Zero;
		}
	}


	public Vector3? GetMousePositionOnMovementPlane()
	{
		var mousePosition = GetViewport().GetMousePosition();
		var rayOrigin = camera.ProjectRayOrigin(mousePosition);
		var rayNormal = camera.ProjectRayNormal(mousePosition);
		
		return movementPlane.IntersectsRay(rayOrigin, rayNormal);
	}
	
	private void UpdateTarget()
	{
		if (!Input.IsActionJustPressed("move_unit"))
		{
			return;
		}

		var newTarget = GetMousePositionOnMovementPlane();
		if (newTarget is null)
		{
			return;
		}

		target = newTarget.Value;
		velocity = Vector3.Zero;
		EmitSignal(SignalName.TargetChanged);
	}


	private Vector3 CalculateAcceleration()
	{
		var speedSq = velocity.LengthSquared();
		var distanceToStop = speedSq / (2f * acceleration);
		var distanceSqToTarget = Position.DistanceSquaredTo(target);

		var accelerationDirection = 0f;
		// If the target is close, decelerate.
		if (distanceSqToTarget <= distanceToStop * distanceToStop)
		{
			accelerationDirection = -1f;
		}
		// If not close to the target, and not at max speed, accelerate.
		else if (speedSq < maxSpeed * maxSpeed)
		{
			accelerationDirection = 1f;
		}
		
		var directionToTarget = Position.DirectionTo(target);
		return directionToTarget * acceleration * accelerationDirection;
	}
}
