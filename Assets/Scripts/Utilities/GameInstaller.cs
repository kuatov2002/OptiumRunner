using Scellecs.Morpeh;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private World world;

    private void Awake()
    {
        // Initialize Morpeh world
        world = World.Default;
        var systemsGroup = world.CreateSystemsGroup();
        
        print("Installed");
        
        // Register systems in execution order
        systemsGroup.AddSystem(new InitializeGameSystem());
        systemsGroup.AddSystem(new InitializePlayerSystem());
        systemsGroup.AddSystem(new VisualCreationSystem());
        systemsGroup.AddSystem(new PlayerInputSystem());
        systemsGroup.AddSystem(new ObstacleSpawnerSystem());
        systemsGroup.AddSystem(new PlayerMovementSystem());
        systemsGroup.AddSystem(new ObstacleMovementSystem());
        systemsGroup.AddSystem(new CollisionDetectionSystem());
        systemsGroup.AddSystem(new ScoreSystem());
        systemsGroup.AddSystem(new SpeedIncreaseSystem());
        systemsGroup.AddSystem(new UIUpdateSystem());
        
        world.AddSystemsGroup(order: 0, systemsGroup);
    }

    private void Update()
    {
        // Update all systems
        world.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        // Clean up
        if (world != null)
        {
            world.Dispose();
        }
    }
}