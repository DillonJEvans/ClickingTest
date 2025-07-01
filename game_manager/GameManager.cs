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


	public void MainMenu() => GetTree().ChangeSceneToPacked(mainMenu);
	
	public void PlayGame()
	{
		score = 0;
		GetTree().ChangeSceneToPacked(game);
	}
	
	public void QuitGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/inputs/handling_quit_requests.html
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
}
