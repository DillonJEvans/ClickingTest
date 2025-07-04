using Godot;
using System;
using System.Collections.Generic;

public partial class HighScores : Control
{
	[Export] private GridContainer grid;
	[Export] public int maximumHighScores = 10;

	[ExportGroup("Label Scenes")]
	[Export] private PackedScene rowNumberLabel;
	[Export] private PackedScene scoreLabel;
	[Export] private PackedScene difficultyLabel;
	[Export] private PackedScene dateTimeLabel;
	
	
	private List<HighScore> highScores;


	public override void _Ready()
	{
		base._Ready();

		highScores = HighScoresIO.LoadHighScores();
		UpdateChildren();
	}


	public int SaveHighScore()
	{
		return SaveHighScore(GameManager.instance.score, GameManager.instance.difficulty);
	}
	
	public int SaveHighScore(int score, GameManager.Difficulty difficulty)
	{
		var highScore = new HighScore(score, difficulty, DateTime.Now);
		
		var index = highScores.BinarySearch(highScore);
		index = index < 0 ? ~index : index + 1;
		
		if (index >= maximumHighScores)
		{
			return -1;
		}
		
		highScores.Insert(index, highScore);
		HighScoresIO.SaveHighScores(highScores);
		UpdateChildren();
		
		return index;
	}


	private void UpdateChildren()
	{
		RemoveChildren();

		var row = 1;
		foreach (var highScore in highScores)
		{
			AddRow(highScore, row);
			row++;
		}
	}

	private void AddRow(HighScore highScore, int row)
	{
		if (rowNumberLabel.Instantiate() is not RowNumberLabel rowNumber) return;
		if (scoreLabel.Instantiate() is not ScoreLabel score) return;
		if (difficultyLabel.Instantiate() is not DifficultyLabel difficulty) return;
		if (dateTimeLabel.Instantiate() is not DateTimeLabel dateTime) return;
		
		rowNumber.SetRowNumber(row);
		score.SetScore(highScore.score);
		difficulty.SetDifficulty(highScore.difficulty);
		dateTime.SetDateTime(highScore.dateTime);
		
		grid.AddChild(rowNumber);
		grid.AddChild(score);
		grid.AddChild(difficulty);
		grid.AddChild(dateTime);
	}
	
	private void RemoveChildren()
	{
		foreach (var child in grid.GetChildren())
		{
			child.QueueFree();
			grid.RemoveChild(child);
		}
	}
}
