using Godot;

public partial class GameManager : Node3D
{
    [Signal]
    public delegate void CoinCollectedEventHandler();
    [Signal]
    public delegate void ScoreUpdatedEventHandler(int score);
    
    [Export] private PackedScene coin;
    [Export] private Vector3 minSpawnPos = new(-20f, 0.5f, -20f);
    [Export] private Vector3 maxSpawnPos = new(20f, 0.5f, 20f);
    [Export] private int coinsAtOnce = 2;


    public int Score { get; private set; } = 0;
    public int CoinsSpawned { get; private set; } = 0;


    private RandomNumberGenerator random = new();


    public override void _Ready()
    {
        base._Ready();

        for (var i = 0; i < coinsAtOnce; i++)
        {
            SpawnCoin();
        }
    }


    public void CollectCoin()
    {
        Score++;
        SpawnCoin();
        EmitSignal(SignalName.CoinCollected);
        EmitSignal(SignalName.ScoreUpdated, Score);
    }
    

    private void SpawnCoin()
    {
        if (coin.Instantiate() is not Coin newCoin)
        {
            return;
        }

        newCoin.Name = $"Coin {CoinsSpawned + 1}";
        newCoin.Position = new Vector3(
            random.RandfRange(minSpawnPos.X, maxSpawnPos.X),
            random.RandfRange(minSpawnPos.Y, maxSpawnPos.Y),
            random.RandfRange(minSpawnPos.Z, maxSpawnPos.Z)
        );
        newCoin.Connect(Coin.SignalName.Collected, Callable.From(CollectCoin), (uint)ConnectFlags.OneShot);
        AddChild(newCoin);
        CoinsSpawned++;
    }
}
