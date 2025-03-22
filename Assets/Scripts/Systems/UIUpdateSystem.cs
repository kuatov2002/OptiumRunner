using Scellecs.Morpeh;
using UnityEngine;
using OptimumRunner.Components;

namespace OptimumRunner.Systems
{
    public sealed class UIUpdateSystem : ISystem
    {
        private Filter _scoreFilter;
        private Filter _gameStateFilter;
        private World _world;
        private GameUI _gameUI;

        public World World
        {
            get => _world;
            set => _world = value;
        }

        public void OnAwake()
        {
            _scoreFilter = World.Filter.With<Score>().Build();
            _gameStateFilter = World.Filter.With<GameState>().Build();
            _gameUI = GameObject.FindObjectOfType<GameUI>();
        }

        public void OnUpdate(float deltaTime)
        {
            // Update score UI
            foreach (var scoreEntity in _scoreFilter)
            {
                ref var score = ref scoreEntity.GetComponent<Score>();

                if (_gameUI != null)
                {
                    _gameUI.UpdateScoreText(score.value);
                }
            }

            // Check for game over
            foreach (var gameStateEntity in _gameStateFilter)
            {
                ref var gameState = ref gameStateEntity.GetComponent<GameState>();

                if (gameState.isGameOver && _gameUI != null)
                {
                    _gameUI.ShowGameOver();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}