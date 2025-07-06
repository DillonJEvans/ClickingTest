using Godot;
using System.Collections.Generic;

public class HighScoresIO
{
	public const string DefaultSaveFile = "user://high_scores.csv";
	
	
	public static void SaveHighScores(List<HighScore> highScores, string filename = DefaultSaveFile)
	{
		using var saveFile = FileAccess.Open(filename, FileAccess.ModeFlags.Write);

		foreach (var highScore in highScores)
		{
			saveFile.StoreCsvLine(highScore.ToCsvLine());
		}
	}
	
	
	public static List<HighScore> LoadHighScores(string filename = DefaultSaveFile)
	{
		var highScores = new List<HighScore>();

		if (!FileAccess.FileExists(filename))
		{
			return highScores;
		}

		using var saveFile = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			var csvLine = saveFile.GetCsvLine();
			var highScore = HighScore.FromCsvLine(csvLine);
			if (highScore is not null)
			{
				highScores.Add(highScore);
			}
		}
		
		highScores.Sort();
		return highScores;
	}


	public static void ResetHighScores(string filename = DefaultSaveFile)
	{
		DirAccess.RemoveAbsolute(filename);
	}
}
