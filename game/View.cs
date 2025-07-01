using Godot;

public partial class View : Node3D
{
	[Export] private Camera3D camera;

	[ExportGroup("Movement")]
	[Export] private float speed = 5f;
	[Export] private float movementSnappiness = 10f;
	
	[ExportGroup("Zoom")]
	[Export] private float minZoomDistancePerTick = 0.5f;
	[Export] private float maxZoomDistancePerTick = 2f;
	[Export(PropertyHint.Range, "0.1,20,0.1")] private float zoomSnappiness = 10f;
	[Export] private float minZoom = 2f;
	[Export] private float maxZoom = 40f;


	private Vector3 targetPosition;
	private float targetZoom;
	
	private Plane dragPlane = Plane.PlaneXZ;
	private Vector3? dragPoint;

	// Used by Reset().
	private Vector3 initialPosition;
	private float initialZoom;
	
	
	public override void _Ready()
	{
		base._Ready();

		camera ??= GetNode<Camera3D>("Camera3D");
		targetPosition = Position;
		targetZoom = camera.Position.Z;
		
		initialPosition = Position;
		initialZoom = camera.Position.Z;
	}
	
	public override void _Process(double dt)
	{
		base._Process(dt);
		var delta = (float)dt;

		Zoom();
		Move(delta);
		Reset();
		Drag();

		Position = Position.Lerp(targetPosition, delta * speed * movementSnappiness);
		
		var cameraPosition = camera.Position;
		cameraPosition.Z = Mathf.Lerp(cameraPosition.Z, targetZoom, delta * zoomSnappiness);
		camera.Position = cameraPosition;
	}


	private void Zoom()
	{
		var zoom = 0;
		if (Input.IsActionJustPressed("camera_zoom_in")) zoom--;
		if (Input.IsActionJustPressed("camera_zoom_out")) zoom++;

		var zoomPercent = (targetZoom - minZoom) / (maxZoom - minZoom);
		var zoomDistancePerTick = Mathf.Lerp(minZoomDistancePerTick, maxZoomDistancePerTick, zoomPercent);
		targetZoom += zoom * zoomDistancePerTick;
		targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
	}

	private void Move(float delta)
	{
		var input = Vector3.Zero;
		
		input.X = Input.GetAxis("camera_left", "camera_right");
		input.Z = Input.GetAxis("camera_forward", "camera_backward");
		
		input = input.Rotated(Vector3.Up, Rotation.Y);
		input = input.Normalized();

		targetPosition += input * speed * delta;
	}

	private void Reset()
	{
		if (!Input.IsActionJustPressed("camera_reset"))
		{
			return;
		}

		targetPosition = initialPosition;
		targetZoom = initialZoom;
	}
	
	private void Drag()
	{
		// When the user starts dragging, save the point on the plane that their mouse is over.
		if (Input.IsActionJustPressed("camera_drag"))
		{
			var mousePosition = GetViewport().GetMousePosition();
			var rayOrigin = camera.ProjectRayOrigin(mousePosition);
			var rayNormal = camera.ProjectRayNormal(mousePosition);
			dragPoint = dragPlane.IntersectsRay(rayOrigin, rayNormal);
		}

		// If the user started dragging (and their mouse was over the plane), then:
		//  - Move the camera on a parallel plane that contains the current location of the camera.
		//  - Keep the users mouse over the point they first started dragging on.
		if (dragPoint is not null)
		{
			var mousePosition = GetViewport().GetMousePosition();
			var rayOrigin = camera.ProjectRayOrigin(mousePosition);
			var rayNormal = camera.ProjectRayNormal(mousePosition);

			// Create the plane that the camera moves on.
			// Must create it every frame in case the user zooms or rotates the camera.
			var cameraPlane = new Plane(dragPlane.Normal, rayOrigin);
			
			// Create a ray starting at the drag point and going towards the plane of the camera,
			// and see where it hits.
			var desiredRayOrigin = cameraPlane.IntersectsRay(dragPoint.Value, -rayNormal);
			// If the ray hit, move the camera to that point (kinda).
			if (desiredRayOrigin is not null)
			{
				Position += desiredRayOrigin.Value - rayOrigin;
				// When moving with the mouse, disable movement smoothing.
				targetPosition = Position;
			}
		}
		
		// When the user stops dragging, forget the point on the plane that their mouse is over.
		if (Input.IsActionJustReleased("camera_drag"))
		{
			dragPoint = null;
		}
	}
}
