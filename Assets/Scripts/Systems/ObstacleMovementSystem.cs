using Scellecs.Morpeh;

public sealed class ObstacleMovementSystem : ISystem
{
    private Filter playerFilter;
    private Filter obstacleFilter;
    private Filter gameStateFilter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        playerFilter = World.Filter.With<PlayerTag>().With<Movement>().Build();
        obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
        gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        float playerSpeed = 0f;
        
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver)
                return;
        }
        
        foreach (var playerEntity in playerFilter)
        {
            ref var movement = ref playerEntity.GetComponent<Movement>();
            playerSpeed = movement.ForwardSpeed;
        }
        
        foreach (var obstacleEntity in obstacleFilter)
        {
            ref var position = ref obstacleEntity.GetComponent<Position>();
            
            // Move obstacle towards player (in opposite direction of player movement)
            position.Value.z -= playerSpeed * deltaTime;
            
            // Remove obstacles that are behind the player
            if (position.Value.z < -10f)
            {
                World.RemoveEntity(obstacleEntity);
            }
        }
    }

    public void Dispose()
    {
    }
}