using System;
using System.Globalization;

public class HighScore : IComparable<HighScore>
{
	public readonly int score;
	public readonly GameManager.Difficulty difficulty;
	public readonly DateTime dateTime;
	
	
	public HighScore(int score, GameManager.Difficulty difficulty, DateTime dateTime)
	{
		this.score = score;
		this.difficulty = difficulty;
		this.dateTime = dateTime;
	}

	
	public int CompareTo(HighScore other)
	{
		var scoreComparison = other.score.CompareTo(score);
		if (scoreComparison != 0) return scoreComparison;
		
		var difficultyComparison = other.difficulty.CompareTo(difficulty);
		if (difficultyComparison != 0) return difficultyComparison;
		
		return dateTime.CompareTo(other.dateTime);
	}

	public override string ToString()
	{
		return $"{score} ({difficulty}, {dateTime})";
	}

	
	public string[] ToCsvLine()
	{
		return new[]
		{
			score.ToString(),
			difficulty.ToString(),
			dateTime.ToString(CultureInfo.InvariantCulture),
		};
	}

	
	public static HighScore FromCsvLine(string[] csvLine)
	{
		if (!int.TryParse(csvLine[0], out var score))
		{
			return null;
		}
		if (!Enum.TryParse(csvLine[1], true, out GameManager.Difficulty difficulty))
		{
			return null;
		}
		if (!DateTime.TryParse(csvLine[2], out var date))
		{
			return null;
		}
		
		return new HighScore(score, difficulty, date);
	}
}
