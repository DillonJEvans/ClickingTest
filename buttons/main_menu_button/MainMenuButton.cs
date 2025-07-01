using Godot;

public partial class MainMenuButton : Button
{
	private void OnPressed() => GameManager.instance.MainMenu();
}
