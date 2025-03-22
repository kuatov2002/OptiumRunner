using Scellecs.Morpeh;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class ScoreSystem : ISystem
    {
        private Filter _scoreFilter;
        private Filter _gameStateFilter;
        private World _world;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _scoreFilter = World.Filter.With<Score>().Build();
            _gameStateFilter = World.Filter.With<GameState>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var gameStateEntity in _gameStateFilter)
            {
                ref var gameState = ref gameStateEntity.GetComponent<GameState>();

                if (gameState.isGameOver)
                    return;

                gameState.gameTime += deltaTime;

                foreach (var scoreEntity in _scoreFilter)
                {
                    ref var score = ref scoreEntity.GetComponent<Score>();
                    score.value += deltaTime;
                }
            }
        }

        public void Dispose()
        {
        }
    }
}