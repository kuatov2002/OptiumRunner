using Scellecs.Morpeh;

public sealed class ScoreSystem : ISystem
{
    private Filter scoreFilter;
    private Filter gameStateFilter;
    private World world;

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        scoreFilter = World.Filter.With<Score>().Build();
        gameStateFilter = World.Filter.With<GameState>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver)
                return;
            
            gameState.GameTime += deltaTime;
            
            foreach (var scoreEntity in scoreFilter)
            {
                ref var score = ref scoreEntity.GetComponent<Score>();
                score.Value += deltaTime;
            }
        }
    }

    public void Dispose()
    {
    }
}