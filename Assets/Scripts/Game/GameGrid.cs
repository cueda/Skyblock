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


    // Adds a GridEntity to the given coordinates.
    // If an entity already exists, return an error.
    // Condition checking should be done outside of this class.
	public void AddObject(GridEntity obj, GameGridCoords coords)
	{
        try
        {
            tiles.Add(coords, obj); 
        }
        catch (ArgumentException)
        {
            Debug.LogError("Element already exists at " + coords + " .");
        }
	}


	// Attempts to return object's GridEntity at coordinates.
	// If not found, returns null.
	public GridEntity GetObjectAt(GameGridCoords coords)
	{
        GridEntity value;
		if(tiles.TryGetValue(coords, out value))
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
	

	// Attempts to remove entry at coordinates.
	// If not found, returns false.
	public bool RemoveAtCoordinates(GameGridCoords coords)
	{
        return tiles.Remove(coords);
	}
}
