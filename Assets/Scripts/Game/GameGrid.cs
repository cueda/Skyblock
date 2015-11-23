using UnityEngine;
using System;
using System.Collections.Generic;

// Representation of the in-game game board state using a Dictionary.
public class GameGrid : MonoBehaviour 
{
    // Game world grid coordinates occupied with GameGridTiles
	private Dictionary<GameGridCoords, GridEntity> tiles = new Dictionary<GameGridCoords, GridEntity>();
	
	public static GameGrid Instance {get; private set;}


	void Awake()
    {
        // Singleton initialization.
		if(Instance != null && Instance != this)
			Destroy(gameObject);
		
		Instance = this;		
	}
        
    #region Adding and removing entities
    
    // Adds a GridEntity to the target coordinates.
    // If an entity already exists at target location, return an error.
    // Handles adding any child spaces by adding filler entities to respective location.
    // Condition checking should be done outside of this class, including collisions for children.
	public void AddEntity(GridEntity entity, GameGridCoords coords)
	{
        // If child positions are not null, fill child positions with FillerEntities
        if(entity.absoluteChildPositions != null)
        {
            AddFillerEntities(entity.absoluteChildPositions);
        }

        // Add parent entity to respective coordinates.
        try
        {
            tiles.Add(coords, entity); 
        }
        catch (ArgumentException)
        {
            Debug.LogError("Element already exists at " + coords + " .");
        }
	}


    // Attempts to remove entity using coordinates.
    // Handles removing any child spaces by removing any filler entities to respective location.
	// If not found, returns false.
	public bool RemoveEntity(GridEntity entity)
	{
        if(entity.absoluteChildPositions != null)
        {
            RemoveFillerEntities(entity.absoluteChildPositions);
        }

        return tiles.Remove(entity.gridPosition);
    }


    // Add filler object to multiple coordinates at once.
    // Convenience function utilizing AddFillerEntity.
    public void AddFillerEntities(GameGridCoords[] objCoords)
    {
        if(objCoords != null)
        {
            foreach (GameGridCoords coords in objCoords)
            {
                AddFillerEntity(coords);
            }
        }
    }


    // Adds a FillerEntity manually to the given coordinates.
    public void AddFillerEntity(GameGridCoords coords)
    {
        try
        {
            //tiles.Add(coords, new FillerEntity());
            ObjectReferences.spawner.SpawnFiller(coords);
        }
        catch (ArgumentException)
        {
            Debug.LogError("Element already exists at " + coords + " .");
        }
    }


    // Remove filler object from multiple coordinates at once.
    // Convenience function utilizing RemoveFillerEntity.
    // Will remove anything at coordinates - use properly by using GridEntity's absoluteChildPositions array
    public void RemoveFillerEntities(GameGridCoords[] objCoords)
    {
        if(objCoords != null)
        {
            foreach (GameGridCoords coords in objCoords)
            {
                RemoveFillerEntity(coords);
            }
        }
    }


    // Removes a FillerEntity manually from the given coordinates.
    public void RemoveFillerEntity(GameGridCoords coords)
    {
        try
        {
            //tiles.Remove(coords);
            GameObject fillerToDestroy = GetObjectAt(coords).gameObject;
            tiles.Remove(coords);
            Destroy(fillerToDestroy);
            Debug.Log("Removed filler entity at " + coords);
        }
        catch (ArgumentException)
        {
            Debug.LogError("Element already exists at " + coords + " .");
        }
    }

    #endregion

    #region Utility functions

    // Attempts to return object's GridEntity at coordinates.
    // If not found, returns null.
    public GridEntity GetObjectAt(GameGridCoords coords)
    {
        GridEntity value;
        if (tiles.TryGetValue(coords, out value))
            return value;

        return null;
    }


    // Checks coordinates for an object.
    // If found, true; if not, false.
    public bool IsCoordinatesOccupied(GameGridCoords coords)
    {
        GridEntity value;
        if (tiles.TryGetValue(coords, out value))
        {
            return true;
        }

        return false;
    }


    // Get tile grid for saving and loading purposes.
    // Do not directly modify the contents of the GameGrid dictionary otherwise!
    public Dictionary<GameGridCoords, GridEntity> GetGameGridTiles()
    {
        return tiles;
    }

    #endregion
}
