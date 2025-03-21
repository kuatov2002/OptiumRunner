using Scellecs.Morpeh;
using Unity.Mathematics;

// Position component
[System.Serializable]
public struct Position : IComponent
{
    public float3 Value;
}

// Movement component
[System.Serializable]
public struct Movement : IComponent
{
    public float ForwardSpeed;
    public float LaneChangeSpeed;
    public int CurrentLane;
    public int TargetLane;
    public float InitialSpeed;
}

// Player tag component
[System.Serializable]
public struct PlayerTag : IComponent
{
}

// Obstacle tag component
[System.Serializable]
public struct ObstacleTag : IComponent
{
}

// Collision component
[System.Serializable]
public struct Collision : IComponent
{
    public bool HasCollided;
}

// Score component
[System.Serializable]
public struct Score : IComponent
{
    public float Value;
}

// Game state component
[System.Serializable]
public struct GameState : IComponent
{
    public bool IsGameOver;
    public float GameTime;
    public float LastSpeedIncreaseTime;
    public float SpeedIncreaseInterval;
    public float SpeedIncreasePercentage;
}