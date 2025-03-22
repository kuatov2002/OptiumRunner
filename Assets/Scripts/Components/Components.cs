using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace OptimumRunner.Components
{
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

        [FormerlySerializedAs("LaneChangeSpeed")]
        public float laneChangeSpeed;

        [FormerlySerializedAs("CurrentLane")] public int currentLane;
        [FormerlySerializedAs("TargetLane")] public int targetLane;
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

        [FormerlySerializedAs("LastSpeedIncreaseTime")]
        public float lastSpeedIncreaseTime;

        [FormerlySerializedAs("SpeedIncreaseInterval")]
        public float speedIncreaseInterval;

        [FormerlySerializedAs("SpeedIncreasePercentage")]
        public float speedIncreasePercentage;
    }
}