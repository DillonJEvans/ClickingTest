using Godot;

public partial class MainMenu : Control
{
	[Export] private PackedScene gameScene;
	
	
	private void PlayGame() => GameManager.instance.ChangeScene(gameScene);
	
	
	private static void QuitGame() => GameManager.instance.QuitGame();
}
