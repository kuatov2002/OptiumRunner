using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

public sealed class CollisionDetectionSystem : ISystem
{
    private Filter playerFilter;
    private Filter obstacleFilter;
    private Filter gameStateFilter;
    private World world;
    private float collisionDistance = 1f;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        playerFilter = World.Filter.With<PlayerTag>().With<Position>().With<Collision>().Build();
        obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
        gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver)
                return;
        }
        
        foreach (var playerEntity in playerFilter)
        {
            ref var playerPosition = ref playerEntity.GetComponent<Position>();
            ref var collision = ref playerEntity.GetComponent<Collision>();
            
            foreach (var obstacleEntity in obstacleFilter)
            {
                ref var obstaclePosition = ref obstacleEntity.GetComponent<Position>();
                
                // Check collision based on distance
                float horizontalDistance = math.abs(playerPosition.Value.x - obstaclePosition.Value.x);
                float forwardDistance = math.abs(playerPosition.Value.z - obstaclePosition.Value.z);
                
                if (horizontalDistance < collisionDistance && forwardDistance < collisionDistance)
                {
                    collision.HasCollided = true;
                    
                    foreach (var gameEntity in gameStateFilter)
                    {
                        ref var gameState = ref gameEntity.GetComponent<GameState>();
                        gameState.IsGameOver = true;
                    }
                    
                    // End game
                    Debug.Log("Game Over! Collision detected.");
                    return;
                }
            }
        }
    }

    public void Dispose()
    {
    }
}