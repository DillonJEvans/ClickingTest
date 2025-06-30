using Godot;

public partial class Bounce : Node3D
{
    [Export] private float secondsPerBounce = 1f;
    [Export] private float bounceDistance = 0.25f;

    [Export(PropertyHint.Range, "0, 1, 0.01")] private float initialPhase = 0f;
    [Export] private bool randomInitialPhase = false;


    private Node3D parent;
    private float initialY;
    private float elapsedSeconds = 0f;
    
    
    public override void _Ready()
    {
        base._Ready();

        parent = GetParent() as Node3D;
        initialY = parent is not null ? parent.Position.Y : Position.Y;

        if (randomInitialPhase)
        {
            initialPhase = GD.Randf();
        }
    }

    public override void _Process(double dt)
    {
        base._Process(dt);
        var delta = (float)dt;
        
        elapsedSeconds += delta;

        var phase = initialPhase + elapsedSeconds / secondsPerBounce;
        var sin = Mathf.Sin(Mathf.Tau * phase);
        
        var position = parent.Position;
        position.Y = initialY + sin * bounceDistance;
        parent.Position = position;
    }
}
