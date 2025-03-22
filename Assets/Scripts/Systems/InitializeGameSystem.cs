using Scellecs.Morpeh;
public sealed class InitializeGameSystem : ISystem
{
    private Filter _filter;
    private World _world;

    public World World { get => _world; set => _world = value; }

    public void OnAwake()
    {
        Entity gameEntity = World.CreateEntity();
        ref GameState gameState = ref gameEntity.AddComponent<GameState>();
        gameState.isGameOver = false;
        gameState.gameTime = 0f;
        gameState.lastSpeedIncreaseTime = 0f;
        gameState.speedIncreaseInterval = 10f; // Speed increase every 10 seconds
        gameState.speedIncreasePercentage = 0.1f; // 10% speed increase

        Entity scoreEntity = World.CreateEntity();
        ref Score score = ref scoreEntity.AddComponent<Score>();
        score.value = 0f;
    }

    public void OnUpdate(float deltaTime)
    {
        // This system only initializes the game once
    }

    public void Dispose()
    {
    }
}