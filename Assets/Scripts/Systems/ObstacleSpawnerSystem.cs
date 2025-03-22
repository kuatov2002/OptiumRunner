using Scellecs.Morpeh;
using Unity.Mathematics;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class ObstacleSpawnerSystem : ISystem
    {
        private Filter _gameStateFilter;
        private World _world;
        private float _spawnTimer;
        private float _maxSpawnInterval = 2f;
        private float _minSpawnInterval = 0.7f;
        private float _spawnDistance = 25f;
        private int[] _lanes = { -1, 0, 1 };

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _gameStateFilter = World.Filter.With<GameState>().Build();
            _spawnTimer = 0f;
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var gameStateEntity in _gameStateFilter)
            {
                ref var gameState = ref gameStateEntity.GetComponent<GameState>();

                if (gameState.isGameOver)
                    return;

                _spawnTimer += deltaTime;

                if (_spawnTimer >= _maxSpawnInterval)
                {
                    _spawnTimer = 0f;
                    SpawnObstacle();

                    // Gradually decrease spawn interval as game progresses
                    _maxSpawnInterval = math.max(_minSpawnInterval, _maxSpawnInterval - 0.03f);
                }
            }
        }

        private void SpawnObstacle()
        {
            // Choose a random lane
            int laneIndex = UnityEngine.Random.Range(0, _lanes.Length);
            int lane = _lanes[laneIndex];

            Entity obstacleEntity = World.CreateEntity();

            ref ObstacleTag obstacleTag = ref obstacleEntity.AddComponent<ObstacleTag>();

            ref Position position = ref obstacleEntity.AddComponent<Position>();
            position.value = new float3(lane * 2f, 0.5f, _spawnDistance);
        }

        public void Dispose()
        {
        }
    }
}