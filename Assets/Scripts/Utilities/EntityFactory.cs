using Scellecs.Morpeh;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject obstaclePrefab;
    private World world;

    public void Initialize(World world)
    {
        this.world = world;
    }
    
    public void CreatePlayerVisual()
    {
        var filter = world.Filter.With<PlayerTag>().With<Position>().Build();
        
        foreach (var entity in filter)
        {
            ref var position = ref entity.GetComponent<Position>();
            
            GameObject playerObj = Instantiate(playerPrefab, new Vector3(position.Value.x, position.Value.y, position.Value.z), Quaternion.identity);
            if (!playerObj.TryGetComponent<EntityProvider>(out EntityProvider provider))
            {
                provider = playerObj.AddComponent<EntityProvider>();
            }
            provider.Initialize(world, entity);
        }
    }
    
    public void CreateObstacleVisual(Entity entity)
    {
        if (entity.Has<Position>())
        {
            ref var position = ref entity.GetComponent<Position>();
            
            GameObject obstacleObj = Instantiate(obstaclePrefab, new Vector3(position.Value.x, position.Value.y, position.Value.z), Quaternion.identity);
            if (!obstacleObj.TryGetComponent<EntityProvider>(out EntityProvider provider))
            {
                provider = obstacleObj.AddComponent<EntityProvider>();
            }
            provider.Initialize(world, entity);
        }
    }
}