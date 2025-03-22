using Scellecs.Morpeh;
using UnityEngine;

public sealed class SpeedIncreaseSystem : ISystem
{
    private Filter _playerFilter;
    private Filter _gameStateFilter;
    private World _world;

    public World World { get => _world; set => _world = value; }

    public void OnAwake()
    {
        _playerFilter = World.Filter.With<PlayerTag>().With<Movement>().Build();
        _gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var gameStateEntity in _gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.isGameOver)
                return;
            
            if (gameState.gameTime - gameState.lastSpeedIncreaseTime >= gameState.speedIncreaseInterval)
            {
                // Time to increase speed
                gameState.lastSpeedIncreaseTime = gameState.gameTime;
                
                foreach (var playerEntity in _playerFilter)
                {
                    ref var movement = ref playerEntity.GetComponent<Movement>();
                    movement.forwardSpeed += movement.forwardSpeed * gameState.speedIncreasePercentage;
                    
                    Debug.Log($"Speed increased to: {movement.forwardSpeed}");
                }
            }
        }
    }

    public void Dispose()
    {
    }
}