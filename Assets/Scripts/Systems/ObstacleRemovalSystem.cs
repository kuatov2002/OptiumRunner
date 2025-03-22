using Scellecs.Morpeh;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class ObstacleRemovalSystem : ISystem
    {
        private Filter _obstacleFilter;
        private World _world;
        private readonly float _playerPosZ = 0;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _obstacleFilter = World.Filter.With<ObstacleTag>().With<Position>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            // Check obstacles that are behind the player
            foreach (var obstacleEntity in _obstacleFilter)
            {
                ref var obstaclePosition = ref obstacleEntity.GetComponent<Position>();

                // Check if obstacle is far behind the player (adjust th    is threshold as needed)
                if (obstaclePosition.value.z < _playerPosZ - 10f)
                {
                    EntityProvider.RemoveEntityAndGameObject(obstacleEntity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}