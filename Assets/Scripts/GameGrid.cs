using UnityEngine;
using System;
using System.Collections.Generic;

// Representation of the in-game game board state using a Dictionary.
public class GameGrid : MonoBehaviour 
{
    // Game world grid coordinates occupied with GameGridTiles
	private Dictionary<GameGridCoords, GameObject> tiles = new Dictionary<GameGridCoords, GameObject>();
	
	public static GameGrid Instance {get; private set;}


	void Awake()
    {
        // Singleton initialization.
		if(Instance != null && Instance != this)
			Destroy(gameObject);
		
		Instance = this;		
	}


	public void AddObject(GameObject obj, GameGridCoords coords)
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


	// Attempts to return object at coordinates.
	// If not found, returns null.
	public GameObject GetObject(GameGridCoords coords)
	{
        GameObject value;
		if(tiles.TryGetValue(coords, out value))
			return value;

		return null;
	}


	// Checks coordinates for an object.
	// If found, true; if not, false.
	public bool IsCoordinatesOccupied(GameGridCoords coords)
    {
        GameObject value;
        if (tiles.TryGetValue(coords, out value))
        {
            return true;
        }

		return false;
	}
	

	// Attempts to remove object at coordinates.
	// If not found, returns false.
	public bool RemoveAtCoordinates(GameGridCoords coords)
	{
        return tiles.Remove(coords);
	}
}
