using Godot;

public partial class Spin : Node3D
{
    private enum Direction { Clockwise, CounterClockwise }


    [Export] private float secondsPerRotation = 5f;
    [Export] private Direction direction = Direction.CounterClockwise;
    
    [Export] private bool randomInitialRotation = false;


    private Node3D parent;


    public override void _Ready()
    {
        base._Ready();
        
        parent = GetParent() as Node3D;
        
        if (randomInitialRotation)
        {
            parent?.RotateY(GD.Randf() * Mathf.Tau);
        }
    }
    
    public override void _Process(double dt)
    {
        base._Process(dt);
        var delta = (float)dt;

        if (secondsPerRotation == 0f)
        {
            return;
        }
        
        var radiansPerSecond = Mathf.Tau / secondsPerRotation;
        if (direction == Direction.Clockwise)
        {
            radiansPerSecond = -radiansPerSecond;
        }
        
        parent.RotateY(radiansPerSecond * delta);
    }
}
