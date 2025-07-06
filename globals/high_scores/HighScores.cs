using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class HighScores : Node
{
	public static HighScores instance { get; private set; }
	
	
	[Signal]
	public delegate void HighScoreAddedEventHandler(int index);

	[Signal]
	public delegate void HighScoredClearedEventHandler();
	
	
	[Export] public int maximumHighScores = 10;


	public ReadOnlyCollection<HighScore> highScores => highScoresList.AsReadOnly();
	
	private List<HighScore> highScoresList;


	public override void _Ready()
	{
		base._Ready();

		instance = this;
		
		highScoresList = HighScoresIO.LoadHighScores();
	}


	public int SaveHighScore()
	{
		return SaveHighScore(GameManager.instance.score, GameManager.instance.difficulty);
	}
	
	public int SaveHighScore(int score, GameManager.Difficulty difficulty)
	{
		// Create the high score.
		var highScore = new HighScore(score, difficulty, DateTime.Now);
		
		// Figure out the index where it should go to maintain sorted order.
		var index = highScoresList.BinarySearch(highScore);
		index = index < 0 ? ~index : index + 1;
		
		// If the high score is not high enough, do nothing.
		if (index >= maximumHighScores)
		{
			return -1;
		}
		
		// Add the high score and make room for it.
		highScoresList.Insert(index, highScore);
		while (highScoresList.Count > maximumHighScores)
		{
			highScoresList.RemoveAt(highScoresList.Count - 1);
		}
		
		HighScoresIO.SaveHighScores(highScoresList);
		EmitSignal(SignalName.HighScoreAdded, index);
		return index;
	}
	
	
	public void ResetHighScores()
	{
		highScoresList.Clear();
		HighScoresIO.ResetHighScores();
		EmitSignal(SignalName.HighScoredCleared);
	}
}
