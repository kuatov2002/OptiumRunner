using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

public sealed class InitializePlayerSystem : ISystem
{
    private Filter filter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        Entity playerEntity = World.CreateEntity();
        
        ref PlayerTag playerTag = ref playerEntity.AddComponent<PlayerTag>();
        
        ref Position position = ref playerEntity.AddComponent<Position>();
        position.Value = new float3(0f, 0.5f, 0f);
        
        ref Movement movement = ref playerEntity.AddComponent<Movement>();
        movement.ForwardSpeed = 5f; // Initial forward speed
        movement.LaneChangeSpeed = 10f;
        movement.CurrentLane = 0; // Middle lane
        movement.TargetLane = 0;
        movement.InitialSpeed = 5f;
        
        ref Collision collision = ref playerEntity.AddComponent<Collision>();
        collision.HasCollided = false;
    }

    public void OnUpdate(float deltaTime)
    {
        // This system only initializes the player once
    }

    public void Dispose()
    {
    }
}