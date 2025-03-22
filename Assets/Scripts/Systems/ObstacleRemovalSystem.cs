using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

public sealed class ObstacleRemovalSystem : ISystem
{
    private Filter _obstacleFilter;
    private Filter _playerFilter;
    private World _world;
    private float _playerPosZ = 0;
    public World World { get => _world; set => _world = value; }

    public void OnAwake()
    {
        _obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
        _playerFilter = World.Filter.With<PlayerTag>().With<Position>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        // Create a list to store entities that need to be removed
        List<Entity> entitiesToRemove = new List<Entity>();

        // Check obstacles that are behind the player
        foreach (var obstacleEntity in _obstacleFilter)
        {
            ref var obstaclePosition = ref obstacleEntity.GetComponent<Position>();
            
            // Check if obstacle is far behind the player (adjust this threshold as needed)
            if (obstaclePosition.value.z < _playerPosZ - 10f)
            {
                entitiesToRemove.Add(obstacleEntity);
            }
        }
        
        // Remove the entities outside the iteration to avoid collection modification issues
        foreach (var entity in entitiesToRemove)
        {
            // Use the static method to remove both the entity and its GameObject
            EntityProvider.RemoveEntityAndGameObject(entity);
        }
    }

    public void Dispose()
    {
    }
}