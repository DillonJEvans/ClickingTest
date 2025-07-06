using System;
using Godot;
using System.Collections.Generic;

public partial class DifficultySettings : Node
{
	public const string DefaultDifficultySettingsFile = "res://globals/difficulty_settings/difficulty_settings.json";
	
	
	public static DifficultySettings instance { get; private set; }


	private Dictionary<GameManager.Difficulty, double> timeLimits;
	private Dictionary<GameManager.Difficulty, int> coinsAtOnce;


	public override void _Ready()
	{
		base._Ready();
		
		instance = this;
		
		LoadDifficultySettings();
	}


	public double GetTimeLimit()
	{
		return GetTimeLimit(GameManager.instance.difficulty);
	}
	
	public double GetTimeLimit(GameManager.Difficulty difficulty)
	{
		return timeLimits[difficulty];
	}

	public int GetCoinAtOnce()
	{
		return GetCoinAtOnce(GameManager.instance.difficulty);
	}
	
	public int GetCoinAtOnce(GameManager.Difficulty difficulty)
	{
		return coinsAtOnce[difficulty];
	}
	

	private void LoadDifficultySettings(string filename = DefaultDifficultySettingsFile)
	{
		// What if it doesn't exist?
		if (!FileAccess.FileExists(filename))
		{
			GD.PrintErr("Difficulty settings don't exist???");
		}
		
		var fileText = FileAccess.GetFileAsString(filename);
		
		var json = Json.ParseString(fileText);
		var root = json.AsGodotDictionary<string, Variant>();
		
		timeLimits = JsonToDifficultyDictionary<double>(root["timeLimit"]);
		coinsAtOnce = JsonToDifficultyDictionary<int>(root["coinsAtOnce"]);
	}


	private static Dictionary<GameManager.Difficulty, TValue> JsonToDifficultyDictionary<[MustBeVariant] TValue>(Variant json)
	{
		var dictionary = new Dictionary<GameManager.Difficulty, TValue>();
		
		var jsonDictionary = json.AsGodotDictionary<string, TValue>();
		foreach (var entry in jsonDictionary)
		{
			if (Enum.TryParse<GameManager.Difficulty>(entry.Key, true, out var difficulty))
			{
				dictionary[difficulty] = entry.Value;
			}
		}
		
		return dictionary;
	}
}
