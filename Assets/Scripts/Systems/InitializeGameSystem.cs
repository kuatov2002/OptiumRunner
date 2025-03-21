using Scellecs.Morpeh;
public sealed class InitializeGameSystem : ISystem
{
    private Filter filter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        Entity gameEntity = World.CreateEntity();
        ref GameState gameState = ref gameEntity.AddComponent<GameState>();
        gameState.IsGameOver = false;
        gameState.GameTime = 0f;
        gameState.LastSpeedIncreaseTime = 0f;
        gameState.SpeedIncreaseInterval = 10f; // Speed increase every 10 seconds
        gameState.SpeedIncreasePercentage = 0.1f; // 10% speed increase

        Entity scoreEntity = World.CreateEntity();
        ref Score score = ref scoreEntity.AddComponent<Score>();
        score.Value = 0f;
    }

    public void OnUpdate(float deltaTime)
    {
        // This system only initializes the game once
    }

    public void Dispose()
    {
    }
}