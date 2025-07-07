using Godot;

public partial class Leaderboard : VBoxContainer
{
	[Export] private PackedScene rowScene;
	[Export] private StyleBox rowHighlight;
	
	
	public override void _Ready()
	{
		base._Ready();

		// https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_signals.html#disconnecting-automatically-when-the-receiver-is-freed
		// Connecting with the `+=` operator to an autoload will not automatically disconnect when this node is freed.
		// The solution is to connect using the `Connect` method.
		HighScores.instance.Connect(HighScores.SignalName.HighScoreAdded, Callable.From<int>(_ => UpdateLeaderboard()));
		HighScores.instance.Connect(HighScores.SignalName.HighScoredCleared, Callable.From(UpdateLeaderboard));
		
		HighScores.instance.Connect(HighScores.SignalName.HighScoreAdded, Callable.From<int>(HighlightRow));
		
		UpdateLeaderboard();
	}
	
	
	private void UpdateLeaderboard()
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
	
	
	private void HighlightRow(int rowIndex)
	{
		// Create the highlight PanelContainer.
		var highlight = new PanelContainer();
		highlight.AddThemeStyleboxOverride("panel", rowHighlight);
		
		// Get the row to highlight.
		var row = GetChild(rowIndex);
		
		// Make the row a child of the PanelContainer (and have the PanelContainer take the rows place).
		row.AddSibling(highlight);
		row.Reparent(highlight);
	}
}
