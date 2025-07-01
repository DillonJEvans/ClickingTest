using Godot;
using System;

public partial class DifficultySelectButton : Button
{
	[Export] private GameManager.Difficulty difficulty;
	
	
	public override void _Ready()
	{
		base._Ready();

		Pressed += OnPressed;
	}
	
	
	private void OnPressed()
	{
		GameManager.instance.difficulty = difficulty;
	}
}
