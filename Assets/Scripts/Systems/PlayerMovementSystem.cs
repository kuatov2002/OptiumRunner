using Scellecs.Morpeh;
using Unity.Mathematics;

public sealed class PlayerMovementSystem : ISystem
{
    private Filter playerFilter;
    private Filter gameStateFilter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        playerFilter = World.Filter.With<PlayerTag>().With<Movement>().With<Position>().Build();
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
            ref var position = ref playerEntity.GetComponent<Position>();
            
            // Forward movement
            //position.Value.z += movement.ForwardSpeed * deltaTime;
            position.Value.z += 0;
            // Lane-based movement (horizontal)
            float targetX = movement.TargetLane * 2f; // 2 units between lanes
            float currentX = position.Value.x;
            
            if (math.abs(targetX - currentX) > 0.1f)
            {
                // Move towards target lane
                float step = movement.LaneChangeSpeed * deltaTime;
                position.Value.x = math.lerp(currentX, targetX, step);
            }
            else
            {
                // Reached target lane
                position.Value.x = targetX;
                movement.CurrentLane = movement.TargetLane;
            }
        }
    }

    public void Dispose()
    {
    }
}