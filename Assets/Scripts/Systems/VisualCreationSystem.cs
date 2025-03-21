using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

public sealed class VisualCreationSystem : ISystem
{
    private Filter playerFilter;
    private Filter obstacleFilter;
    private HashSet<int> processedObstacleIds = new HashSet<int>();
    private World world;
    private EntityFactory entityFactory;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        playerFilter = World.Filter.With<PlayerTag>().With<Position>().Build();
        obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
    
        // Get EntityFactory after creation
        entityFactory = GameObject.FindObjectOfType<EntityFactory>();
    
        if (entityFactory != null)
        {
            entityFactory.Initialize(World);
            entityFactory.CreatePlayerVisual();
        }
        else
        {
            Debug.LogError("EntityFactory not found!");
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if (entityFactory == null)
            return;
        
        // Create a set of current obstacle IDs for faster lookup
        HashSet<int> currentObstacleIds = new HashSet<int>();
        
        // Check for new obstacles
        foreach (var obstacleEntity in obstacleFilter)
        {
            int entityId = obstacleEntity.GetHashCode();
            currentObstacleIds.Add(entityId);
            
            if (!processedObstacleIds.Contains(entityId))
            {
                entityFactory.CreateObstacleVisual(obstacleEntity);
                processedObstacleIds.Add(entityId);
            }
        }
        
        // Clean up references to removed obstacles by only keeping IDs that still exist
        processedObstacleIds.IntersectWith(currentObstacleIds);
    }

    public void Dispose()
    {
    }
}