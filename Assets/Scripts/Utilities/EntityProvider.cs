using System;
using Scellecs.Morpeh;
using UnityEngine;

public class EntityProvider : MonoBehaviour, IDisposable
{
    public Entity Entity { get; private set; }
    private World _world;
    private bool _isEntityValid = true;
    private bool _isBeingDestroyed = false;
    
    // Static dictionary to keep track of all entity providers
    private static System.Collections.Generic.Dictionary<Entity, EntityProvider> _allProviders = 
        new System.Collections.Generic.Dictionary<Entity, EntityProvider>();
    
    public void Initialize(World world, Entity entity)
    {
        this._world = world;
        Entity = entity;
        
        // Register this provider
        if (Entity != null)
        {
            _allProviders[Entity] = this;
        }
    }
    
    private void Update()
    {
        // First check if the entity is still valid before trying to access components
        if (Entity != null && _isEntityValid && !_isBeingDestroyed)
        {
            try
            {
                // Try to access a component to confirm the entity is still valid
                if (Entity.Has<Position>())
                {
                    ref var position = ref Entity.GetComponent<Position>();
                    transform.position = new Vector3(position.value.x, position.value.y, position.value.z);
                }
            }
            catch (InvalidHasOperationException)
            {
                // Entity has been disposed, mark it as invalid
                _isEntityValid = false;
                Debug.Log("Entity was disposed, marked as invalid in EntityProvider");
            }
        }
    }
    
    public void Dispose()
    {
        // Only try to remove the entity if we think it's still valid and the world exists
        if (!_isBeingDestroyed)
        {
            _isBeingDestroyed = true;
            
            // Remove from static dictionary
            if (Entity != null)
            {
                _allProviders.Remove(Entity);
            }
            
            // Remove entity from world if it exists
            if (_world != null && Entity != null && _isEntityValid)
            {
                try
                {
                    _world.RemoveEntity(Entity);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to remove entity: {e.Message}");
                }
                finally
                {
                    _isEntityValid = false;
                }
            }
        }
    }
    
    private void OnDestroy()
    {
        Dispose();
    }
    
    public static void RemoveEntityAndGameObject(Entity entity)
    {
        if (entity != null && _allProviders.TryGetValue(entity, out EntityProvider provider))
        {
            if (provider != null)
            {
                // First mark the entity as being destroyed to prevent Update from accessing it
                provider._isBeingDestroyed = true;
                
                // Remove from dictionary first to prevent future lookups
                _allProviders.Remove(entity);
                
                // Store reference to gameObject for destruction
                GameObject gameObjectToDestroy = provider.gameObject;
                
                // Then remove entity from world
                if (provider._world != null && provider._isEntityValid)
                {
                    try
                    {
                        provider._world.RemoveEntity(entity);
                        provider._isEntityValid = false;
                        Debug.Log($"Entity successfully removed from world");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Failed to remove entity: {e.Message}");
                    }
                }
                
                // Finally destroy the GameObject
                if (gameObjectToDestroy != null)
                {
                    Debug.Log($"Destroying GameObject for entity");
                    GameObject.Destroy(gameObjectToDestroy);
                }
            }
        }
        else
        {
            Debug.LogWarning($"Attempted to remove entity that has no registered provider");
        }
    }
}