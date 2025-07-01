using Godot;

public partial class PlayButton : Button
{
	private static void OnPressed() => GameManager.instance.PlayGame();
}
