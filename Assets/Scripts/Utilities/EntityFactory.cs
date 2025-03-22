using OptimumRunner.Components;
using Scellecs.Morpeh;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject obstaclePrefab;
    private World _world;

    public void Initialize(World world)
    {
        this._world = world;
    }
    
    public void CreatePlayerVisual()
    {
        var filter = _world.Filter.With<PlayerTag>().With<Position>().Build();
        
        foreach (var entity in filter)
        {
            ref var position = ref entity.GetComponent<Position>();
            
            GameObject playerObj = Instantiate(playerPrefab, new Vector3(position.value.x, position.value.y, position.value.z), Quaternion.identity);
            if (!playerObj.TryGetComponent<EntityProvider>(out EntityProvider provider))
            {
                provider = playerObj.AddComponent<EntityProvider>();
            }
            provider.Initialize(_world, entity);
        }
    }
    
    public void CreateObstacleVisual(Entity entity)
    {
        if (entity.Has<Position>())
        {
            ref var position = ref entity.GetComponent<Position>();
            
            GameObject obstacleObj = Instantiate(obstaclePrefab, new Vector3(position.value.x, position.value.y, position.value.z), Quaternion.identity);
            if (!obstacleObj.TryGetComponent<EntityProvider>(out EntityProvider provider))
            {
                provider = obstacleObj.AddComponent<EntityProvider>();
            }
            provider.Initialize(_world, entity);
        }
    }
}