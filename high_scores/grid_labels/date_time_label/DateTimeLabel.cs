using System;
using Godot;

public partial class DateTimeLabel : Label
{
	[Export] private string recently = "Just Now";
	[Export] private bool lowercase = false;


	private const int RecentlySeconds = 20;
	private const int DaysInAMonth = 30;
	private const int DaysInAYear = 365;
	
	
	public void SetDateTime(DateTime dateTime)
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

		Text = text;
	}
}
