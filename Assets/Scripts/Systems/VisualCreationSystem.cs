using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class VisualCreationSystem : ISystem
    {
        private Filter _playerFilter;
        private Filter _obstacleFilter;
        private HashSet<int> _processedObstacleIds = new HashSet<int>();
        private World _world;
        private EntityFactory _entityFactory;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _playerFilter = World.Filter.With<PlayerTag>().With<Position>().Build();
            _obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();

            // Get EntityFactory after creation
            _entityFactory = GameObject.FindObjectOfType<EntityFactory>();

            if (_entityFactory != null)
            {
                _entityFactory.Initialize(World);
                _entityFactory.CreatePlayerVisual();
            }
            else
            {
                Debug.LogError("EntityFactory not found!");
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (_entityFactory == null)
                return;

            // Create a set of current obstacle IDs for faster lookup
            HashSet<int> currentObstacleIds = new HashSet<int>();

            // Check for new obstacles
            foreach (var obstacleEntity in _obstacleFilter)
            {
                int entityId = obstacleEntity.GetHashCode();
                currentObstacleIds.Add(entityId);

                if (!_processedObstacleIds.Contains(entityId))
                {
                    _entityFactory.CreateObstacleVisual(obstacleEntity);
                    _processedObstacleIds.Add(entityId);
                }
            }

            // Clean up references to removed obstacles by only keeping IDs that still exist
            _processedObstacleIds.IntersectWith(currentObstacleIds);
        }

        public void Dispose()
        {
        }
    }
}