using System;
using Scellecs.Morpeh;
using UnityEngine;

public class EntityProvider : MonoBehaviour, IDisposable
{
    public Entity Entity { get; private set; }
    private World world;
    private bool isEntityValid = true;
    
    public void Initialize(World world, Entity entity)
    {
        this.world = world;
        Entity = entity;
    }
    
    private void Update()
    {
        // First check if the entity is still valid before trying to access components
        if (Entity != null && isEntityValid)
        {
            try
            {
                // Try to access a component to confirm the entity is still valid
                if (Entity.Has<Position>())
                {
                    ref var position = ref Entity.GetComponent<Position>();
                    transform.position = new Vector3(position.Value.x, position.Value.y, position.Value.z);
                }
            }
            catch (InvalidHasOperationException)
            {
                // Entity has been disposed, mark it as invalid
                isEntityValid = false;
                Debug.Log("Entity was disposed, marked as invalid in EntityProvider");
            }
        }
    }
    
    public void Dispose()
    {
        // Only try to remove the entity if we think it's still valid and the world exists
        if (world != null && Entity != null && isEntityValid)
        {
            try
            {
                world.RemoveEntity(Entity);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to remove entity: {e.Message}");
            }
            finally
            {
                isEntityValid = false;
            }
        }
    }
    
    private void OnDestroy()
    {
        Dispose();
    }
}