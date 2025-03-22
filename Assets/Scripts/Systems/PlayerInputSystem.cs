using Scellecs.Morpeh;
using UnityEngine;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class PlayerInputSystem : ISystem
    {
        private Filter _playerFilter;
        private Filter _gameStateFilter;
        private World _world;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _playerFilter = World.Filter.With<PlayerTag>().With<Movement>().Build();
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
                ref var movement = ref playerEntity.GetComponent<Movement>();

                // Left input
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    if (movement.currentLane > -1) // Prevent going beyond left boundary
                    {
                        movement.targetLane = movement.currentLane - 1;
                    }
                }

                // Right input
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    if (movement.currentLane < 1) // Prevent going beyond right boundary
                    {
                        movement.targetLane = movement.currentLane + 1;
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}