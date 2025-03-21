using Scellecs.Morpeh;
using Unity.Mathematics;

public sealed class ObstacleSpawnerSystem : ISystem
{
    private Filter gameStateFilter;
    private World world;
    private float spawnTimer;
    private float spawnInterval = 2f;
    private float spawnDistance = 50f;
    private int[] lanes = { -1, 0, 1 };

    public World World { get => world; set => world = value; }

    public void OnAwake()
    {
        gameStateFilter = World.Filter.With<GameState>().Build();
        spawnTimer = 0f;
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var gameStateEntity in gameStateFilter)
        {
            ref var gameState = ref gameStateEntity.GetComponent<GameState>();
            
            if (gameState.IsGameOver)
                return;
            
            spawnTimer += deltaTime;
            
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnObstacle();
                
                // Gradually decrease spawn interval as game progresses
                spawnInterval = math.max(0.5f, spawnInterval - 0.05f);
            }
        }
    }
    
    private void SpawnObstacle()
    {
        // Choose a random lane
        int laneIndex = UnityEngine.Random.Range(0, lanes.Length);
        int lane = lanes[laneIndex];
        
        Entity obstacleEntity = World.CreateEntity();
        
        ref ObstacleTag obstacleTag = ref obstacleEntity.AddComponent<ObstacleTag>();
        
        ref Position position = ref obstacleEntity.AddComponent<Position>();
        position.Value = new float3(lane * 2f, 0.5f, spawnDistance);
    }

    public void Dispose()
    {
    }
}