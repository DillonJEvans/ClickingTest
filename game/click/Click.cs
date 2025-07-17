using Godot;

[Tool]
public partial class Click : MeshInstance3D
{
    [Export] private string timeElapsedShaderParameter = "time_elapsed";
    [Export] private string durationShaderParameter = "duration";

    [Export]
    private bool playAnimation
    {
        get => false;
        set { if (value) Play(); }
    }


    public override void _Ready()
    {
        base._Ready();

        Play().TweenCallback(Callable.From(QueueFree));
    }


    private Tween Play()
    {
        SetInstanceShaderParameter(timeElapsedShaderParameter, 0.0f);

        var material = (ShaderMaterial)Mesh.SurfaceGetMaterial(0);
        var duration = material.GetShaderParameter(durationShaderParameter).AsSingle();
        var timeElapsedProperty = $"instance_shader_parameters/{timeElapsedShaderParameter}";
        
        var tween = CreateTween();
        tween.TweenProperty(this, timeElapsedProperty, duration, duration);
        tween.Play();
        return tween;
    }
}
