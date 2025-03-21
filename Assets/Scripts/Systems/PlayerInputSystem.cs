using Scellecs.Morpeh;
using UnityEngine;

public sealed class PlayerInputSystem : ISystem
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
        }

        foreach (var playerEntity in playerFilter)
        {
            ref var movement = ref playerEntity.GetComponent<Movement>();
            
            // Left input
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (movement.CurrentLane > -1) // Prevent going beyond left boundary
                {
                    movement.TargetLane = movement.CurrentLane - 1;
                }
            }
            
            // Right input
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (movement.CurrentLane < 1) // Prevent going beyond right boundary
                {
                    movement.TargetLane = movement.CurrentLane + 1;
                }
            }
        }
    }

    public void Dispose()
    {
    }
}