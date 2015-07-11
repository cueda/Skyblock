using UnityEngine;
using System;
using System.Collections;

public class GridEntity : MonoBehaviour 
{
    [HideInInspector]
    public GameGridCoords gridPosition { get; private set; }

    public void SetGridPosition(GameGridCoords coords)
    {
        gridPosition = new GameGridCoords(coords.x, coords.y);
    }

    /// <summary>
    /// Override this for every GridEntity child object.
    /// </summary>
    public virtual void Interact()
    {
        Debug.LogError("This entity's Interact functionality is not overriden!");
    }
}
