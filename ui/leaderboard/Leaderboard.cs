using Godot;

public partial class Leaderboard : VBoxContainer
{
	[Export] private PackedScene rowScene;
	
	
	public override void _Ready()
	{
		base._Ready();

		// https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_signals.html#disconnecting-automatically-when-the-receiver-is-freed
		// Connecting with the `+=` operator to an autoload will not automatically disconnect when this node is freed.
		// The solution is to connect using the `Connect` method
		// and not to use a lambda (hence the pointless parameter for `UpdateLeaderboard`).
		HighScores.instance.Connect(HighScores.SignalName.HighScoreAdded, Callable.From<int>(UpdateLeaderboard));
		
		UpdateLeaderboard();
	}
	
	
	private void UpdateLeaderboard(int _ = 0)
	{
		// Remove children.
		foreach (var child in GetChildren())
		{
			child.QueueFree();
			RemoveChild(child);
		}

		// Add one row for each high score.
		var rowNumber = 1;
		foreach (var highScore in HighScores.instance.highScores)
		{
			var row = rowScene.Instantiate<LeaderboardRow>();
		
			row.SetRowNumber(rowNumber);
			row.SetHighScore(highScore);
		
			AddChild(row);
			
			rowNumber++;
		}
	}
}
