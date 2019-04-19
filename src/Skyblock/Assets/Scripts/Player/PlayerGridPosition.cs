using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Calculates player grid coordinates closest to player location. 
/// Provides a GameGridCoords with x and y values for public access.
/// </summary>
public class PlayerGridPosition : MonoBehaviour 
{
    [SerializeField]
    private float playerCenter = 1f;

    private GameGridCoords gridPosition;

	void Awake () 
	{
        CalculateGridPosition();
	}

    void Update()
    {
        CalculateGridPosition();
    }

    private void CalculateGridPosition()
    {
        Vector2 groundedPosition = transform.position - (Vector3.up * playerCenter);
        gridPosition = new GameGridCoords(groundedPosition);
    }
	
	public GameGridCoords GetPosition()
    {
        return gridPosition;
    }
}
