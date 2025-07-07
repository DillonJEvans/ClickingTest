using System;
using Godot;

public partial class LeaderboardRow : HBoxContainer
{
	[ExportGroup("Labels")]
	[Export] private Label rowNumberLabel;
	[Export] private Label scoreLabel;
	[Export] private Label difficultyLabel;
	[Export] private Label dateTimeLabel;
	
	[ExportGroup("DateTime Settings")]
	[Export] private string recently = "Just Now";
	[Export] private bool lowercase = false;


	private const int RecentlySeconds = 20;
	private const int DaysInAMonth = 30;
	private const int DaysInAYear = 365;


	private DateTime dateTime;


	public void SetRowNumber(int rowNumber)
	{
		rowNumberLabel.Text = rowNumber.ToString();
	}

	public void SetHighScore(HighScore highScore)
	{
		SetScore(highScore.score);
		SetDifficulty(highScore.difficulty);
		SetDateTime(highScore.dateTime);
	}

	public void SetScore(int score)
	{
		scoreLabel.Text = score.ToString();
	}

	public void SetDifficulty(GameManager.Difficulty difficulty)
	{
		difficultyLabel.Text = difficulty.ToString();

		var colorName = difficulty switch
		{
			GameManager.Difficulty.Easy => "easy_color",
			GameManager.Difficulty.Normal => "normal_color",
			GameManager.Difficulty.Hard => "hard_color",
			_ => "normal_color"
		};
		difficultyLabel.Modulate = GetThemeColor(colorName, "DifficultyColors");
	}

	public void SetDateTime(DateTime newDateTime)
	{
		dateTime = newDateTime;
		UpdateDateTimeLabel();
	}


	// This can be uncommented to cause the date time label to update every frame.
	/*
	public override void _Process(double delta)
	{
		base._Process(delta);
		
		UpdateDateTimeLabel();
	}
	*/
	
	
	private void UpdateDateTimeLabel()
	{
		var timeSince = DateTime.Now - dateTime;
		
		var text = timeSince switch
		{
			{ TotalSeconds: < RecentlySeconds } => recently,
			{ TotalSeconds: < 60 } => $"{timeSince.Seconds} Seconds Ago",
			
			{ TotalMinutes: < 2 } => "1 Minute Ago",
			{ TotalMinutes: < 60 } => $"{timeSince.Minutes} Minutes Ago",
			
			{ TotalHours: < 2 } => "1 Hour Ago",
			{ TotalHours: < 24 } => $"{timeSince.Hours} Hours Ago",
			
			{ TotalDays: < 2 } => "Yesterday",
			{ TotalDays: < 7 } => $"{timeSince.Days} Days Ago",
			
			{ TotalDays: < 14 } => "1 Week Ago",
			{ TotalDays: < 7 * 4} => $"{timeSince.Days / 7} Weeks Ago",
			
			{ TotalDays: < 2 * DaysInAMonth} => "1 Month Ago",
			{ TotalDays: < 12 * DaysInAMonth} => $"{timeSince.Days / DaysInAMonth} Months Ago",
			
			{ TotalDays: < 2 * DaysInAYear} => "1 Year Ago",
			_ => $"{timeSince.Days / DaysInAYear} Years Ago"
		};

		if (lowercase)
		{
			text = char.ToUpper(text[0]) + text[1..].ToLower();
		}

		dateTimeLabel.Text = text;
	}
}
