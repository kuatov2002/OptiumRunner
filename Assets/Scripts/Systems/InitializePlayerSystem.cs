using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

public sealed class InitializePlayerSystem : ISystem
{
    private Filter _filter;
    private World _world;

    public World World { get => _world; set => _world = value; }

    public void OnAwake()
    {
        Entity playerEntity = World.CreateEntity();
        
        ref PlayerTag playerTag = ref playerEntity.AddComponent<PlayerTag>();
        
        ref Position position = ref playerEntity.AddComponent<Position>();
        position.value = new float3(0f, 0.5f, 0f);
        
        ref Movement movement = ref playerEntity.AddComponent<Movement>();
        movement.forwardSpeed = 5f; // Initial forward speed
        movement.laneChangeSpeed = 10f;
        movement.currentLane = 0; // Middle lane
        movement.targetLane = 0;
        movement.initialSpeed = 5f;
        
        ref Collision collision = ref playerEntity.AddComponent<Collision>();
        collision.hasCollided = false;
    }

    public void OnUpdate(float deltaTime)
    {
        // This system only initializes the player once
    }

    public void Dispose()
    {
    }
}