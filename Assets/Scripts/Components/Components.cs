using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine.Serialization;

// Position component
[System.Serializable]
public struct Position : IComponent
{
    [FormerlySerializedAs("Value")] public float3 value;
}

// Movement component
[System.Serializable]
public struct Movement : IComponent
{
    [FormerlySerializedAs("ForwardSpeed")] public float forwardSpeed;
    [FormerlySerializedAs("LaneChangeSpeed")] public float laneChangeSpeed;
    [FormerlySerializedAs("CurrentLane")] public int currentLane;
    [FormerlySerializedAs("TargetLane")] public int targetLane;
    [FormerlySerializedAs("InitialSpeed")] public float initialSpeed;
}

[System.Serializable]
public struct Map : IComponent
{
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float distanceBetweenLines;
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
    [FormerlySerializedAs("HasCollided")] public bool hasCollided;
}

// Score component
[System.Serializable]
public struct Score : IComponent
{
    [FormerlySerializedAs("Value")] public float value;
}

// Game state component
[System.Serializable]
public struct GameState : IComponent
{
    [FormerlySerializedAs("IsGameOver")] public bool isGameOver;
    [FormerlySerializedAs("GameTime")] public float gameTime;
    [FormerlySerializedAs("LastSpeedIncreaseTime")] public float lastSpeedIncreaseTime;
    [FormerlySerializedAs("SpeedIncreaseInterval")] public float speedIncreaseInterval;
    [FormerlySerializedAs("SpeedIncreasePercentage")] public float speedIncreasePercentage;
}