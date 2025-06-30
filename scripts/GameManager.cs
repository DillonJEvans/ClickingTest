using Godot;

public partial class GameManager : Node
{
	public static GameManager instance { get; private set; }
	
	
	public enum Difficulty { Easy, Medium, Hard }
	
	
	public Difficulty difficulty = Difficulty.Medium;


	public override void _Ready()
	{
		base._Ready();
		
		instance = this;
	}


	public void ChangeScene(PackedScene scene) => GetTree().ChangeSceneToPacked(scene);
	
	
	public void QuitGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/inputs/handling_quit_requests.html
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
}
