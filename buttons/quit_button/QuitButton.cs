using Godot;

public partial class QuitButton : Button
{
	private static void OnPressed() => GameManager.instance.QuitGame();
}
