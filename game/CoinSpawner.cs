using Godot;

public partial class CoinSpawner : Node3D
{
    [Signal]
    public delegate void CoinCollectedEventHandler();
    
    
    [Export] private PackedScene coinScene;
    [Export] private Vector3 minSpawnPos = new(-20f, 0.5f, -20f);
    [Export] private Vector3 maxSpawnPos = new(20f, 0.5f, 20f);

    
    private int coinsSpawned = 0;
    private RandomNumberGenerator random = new();


    public override void _Ready()
    {
        base._Ready();

        for (var i = 0; i < DifficultySettings.instance.GetCoinAtOnce(); i++)
        {
            SpawnCoin();
        }
    }


    public void CollectCoin()
    {
        SpawnCoin();
        EmitSignal(SignalName.CoinCollected);
    }
    
    
    private void SpawnCoin()
    {
        if (coinScene.Instantiate() is not Area3D coin)
        {
            GD.PrintErr("Couldn't spawn coin, or it wasn't a coin.");
            return;
        }

        coinsSpawned++;
        coin.Name = $"Coin {coinsSpawned}";
        
        coin.Position = new Vector3(
            random.RandfRange(minSpawnPos.X, maxSpawnPos.X),
            random.RandfRange(minSpawnPos.Y, maxSpawnPos.Y),
            random.RandfRange(minSpawnPos.Z, maxSpawnPos.Z)
            );

        coin.AreaEntered += _ => CollectCoin();
        AddChild(coin);
    }
}
