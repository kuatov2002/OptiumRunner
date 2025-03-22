using Scellecs.Morpeh;
using Unity.Mathematics;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class PlayerMovementSystem : ISystem
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
            _playerFilter = World.Filter.With<PlayerTag>().With<Movement>().With<Position>().Build();
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
                ref var position = ref playerEntity.GetComponent<Position>();

                // Lane-based movement (horizontal)
                float targetX = movement.targetLane * 2f; // 2 units between lanes
                float currentX = position.value.x;
                movement.currentLane = movement.targetLane;
                if (math.abs(targetX - currentX) > 0.1f)
                {
                    // Move towards target lane
                    float step = movement.laneChangeSpeed * deltaTime;
                    position.value.x = math.lerp(currentX, targetX, step);
                }
                else
                {
                    // Reached target lane
                    position.value.x = targetX;

                }
            }
        }

        public void Dispose()
        {
        }
    }
}