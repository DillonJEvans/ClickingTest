using Godot;

public partial class GameManager : Node
{
	public enum Difficulty { Easy, Normal, Hard }
	
	public static GameManager instance { get; private set; }

	[Export] private PackedScene mainMenu;
	[Export] private PackedScene game;
	
	public Difficulty difficulty = Difficulty.Normal;
	public int score = 0;


	public override void _Ready()
	{
		base._Ready();
		
		instance = this;
	}


	public void MainMenu() => ChangeScene(mainMenu);

	public void PlayGame()
	{
		score = 0;
		ChangeScene(game);
	}
	
	public void QuitGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/inputs/handling_quit_requests.html
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
	
	public void ChangeScene(PackedScene scene)
	{
		GetTree().ChangeSceneToPacked(scene);
		GetTree().Paused = false;
	}
}
