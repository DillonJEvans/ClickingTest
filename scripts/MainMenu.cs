using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private PackedScene gameScene;
	
	
	private static void SetDifficulty(GameManager.Difficulty difficulty)
	{
		GameManager.instance.difficulty = difficulty;
	}
	
	// Connecting signals in the Godot editor doesn't let extra call arguments be of enum types.
	// I could add an extra call argument that is an int and pass 0, 1, or 2 for difficulty.
	// But that's ugly.
	// So this method is here so an extra call argument of type string can be used instead of an int.
	// E.g. an extra call argument of "Easy" instead of "0".
	private static void SetDifficulty(string difficulty)
	{
		SetDifficulty(Enum.Parse<GameManager.Difficulty>(difficulty));
	}
	
	
	private void PlayGame() => GameManager.instance.ChangeScene(gameScene);
	
	
	private static void QuitGame() => GameManager.instance.QuitGame();
}
