using Scellecs.Morpeh;
using UnityEngine;

public sealed class UIUpdateSystem : ISystem
{
    private Filter scoreFilter;
    private Filter gameStateFilter;
    private World world;
    private GameUI gameUI;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        scoreFilter = World.Filter.With<Score>().Build();
        gameStateFilter = World.Filter.With<GameState>().Build();
        gameUI = GameObject.FindObjectOfType<GameUI>();
    }

    public void OnUpdate(float deltaTime)
    {
        // Update score UI
        foreach (var scoreEntity in scoreFilter)
        {
            ref var score = ref scoreEntity.GetComponent<Score>();
            
            if (gameUI != null)
            {
                gameUI.UpdateScoreText(score.Value);
            }
        }
        
        // Check for game over
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver && gameUI != null)
            {
                gameUI.ShowGameOver();
            }
        }
    }

    public void Dispose()
    {
    }
}