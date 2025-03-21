using Scellecs.Morpeh;
using UnityEngine;

public sealed class SpeedIncreaseSystem : ISystem
{
    private Filter playerFilter;
    private Filter gameStateFilter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        playerFilter = World.Filter.With<PlayerTag>().With<Movement>().Build();
        gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver)
                return;
            
            if (gameState.GameTime - gameState.LastSpeedIncreaseTime >= gameState.SpeedIncreaseInterval)
            {
                // Time to increase speed
                gameState.LastSpeedIncreaseTime = gameState.GameTime;
                
                foreach (var playerEntity in playerFilter)
                {
                    ref var movement = ref playerEntity.GetComponent<Movement>();
                    movement.ForwardSpeed += movement.ForwardSpeed * gameState.SpeedIncreasePercentage;
                    
                    Debug.Log($"Speed increased to: {movement.ForwardSpeed}");
                }
            }
        }
    }

    public void Dispose()
    {
    }
}