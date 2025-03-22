using OptimumRunner.Components;
using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace OptimumRunner.Systems
{
    public sealed class CollisionDetectionSystem : ISystem
    {
        private Filter _playerFilter;
        private Filter _obstacleFilter;
        private Filter _gameStateFilter;
        private World _world;
        private readonly float _collisionDistance = 0.5f;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _playerFilter = World.Filter.With<PlayerTag>().With<Position>().Build();
            _obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
            _gameStateFilter = World.Filter.With<GameState>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var gameStateEntity in _gameStateFilter)
            {
                ref var gameState = ref gameStateEntity.GetComponent<GameState>();

                if (gameState.isGameOver)
                    return;
            }

            foreach (var playerEntity in _playerFilter)
            {
                ref var playerPosition = ref playerEntity.GetComponent<Position>();

                foreach (var obstacleEntity in _obstacleFilter)
                {
                    ref var obstaclePosition = ref obstacleEntity.GetComponent<Position>();

                    // Check collision based on distance
                    float horizontalDistance = math.abs(playerPosition.value.x - obstaclePosition.value.x);
                    float forwardDistance = math.abs(playerPosition.value.z - obstaclePosition.value.z);

                    if (horizontalDistance < _collisionDistance && forwardDistance < _collisionDistance)
                    {
                        foreach (var gameEntity in _gameStateFilter)
                        {
                            ref var gameState = ref gameEntity.GetComponent<GameState>();
                            gameState.isGameOver = true;
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
}