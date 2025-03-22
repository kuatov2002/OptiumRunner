using Scellecs.Morpeh;

public sealed class ObstacleMovementSystem : ISystem
{
    private Filter _playerFilter;
    private Filter _obstacleFilter;
    private Filter _gameStateFilter;
    private World _world;

    public World World { get => _world; set => _world = value; }

    public void OnAwake()
    {
        _playerFilter = World.Filter.With<PlayerTag>().With<Movement>().Build();
        _obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
        _gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        float playerSpeed = 0f;
        
        foreach (var gameStateEntity in _gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.isGameOver)
                return;
        }
        
        foreach (var playerEntity in _playerFilter)
        {
            ref var movement = ref playerEntity.GetComponent<Movement>();
            playerSpeed = movement.forwardSpeed;
        }
        
        foreach (var obstacleEntity in _obstacleFilter)
        {
            ref var position = ref obstacleEntity.GetComponent<Position>();
            
            // Move obstacle towards player (in opposite direction of player movement)
            position.value.z -= playerSpeed * deltaTime;
            
            // Remove obstacles that are behind the player
            if (position.value.z < -10f)
            {
                World.RemoveEntity(obstacleEntity);
            }
        }
    }

    public void Dispose()
    {
    }
}