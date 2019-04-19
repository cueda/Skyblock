using UnityEngine;
using System;
using System.Collections;

public class GridEntity : MonoBehaviour 
{
    [HideInInspector]
    public GameGridCoords gridPosition { get; protected set; }

    // Spaces this entity occupies beyond the root square.
    // If no child spaces occupied, keep this null.
    private GameGridCoords[] _relativeChildPositions;
    [HideInInspector]
    public GameGridCoords[] relativeChildPositions
    {
        get { return _relativeChildPositions; }
        protected set
        {
            _relativeChildPositions = value;
            RecalculateAbsoluteChildPositions(); 
        }
    }

    // World space occupied by relativeChildPositions, given a gridPosition.
    // These values are cached and recalculated when relativeChildPositions is changed.
    [HideInInspector]
    public GameGridCoords[] absoluteChildPositions { get; private set; }
           

    public virtual void SetGridPosition(GameGridCoords coords)
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


    // Define this for all entities to return its GridEntityType.
    public virtual GridEntityType GetGridEntityType()
    {
        Debug.LogError("This GridEntity has not defined GetGridEntityType()!");
        return GridEntityType.NONE;
    }


    // Override this for GridEntities which have additional data needed to save beyond position and entity type.
    public virtual int[] GenerateExtraSaveData()
    {
        return new int[0];
    }


    // Override this for GridEntities which have additional data needed to save beyond position and entity type.
    public virtual void LoadExtraSaveData(int[] extraData)
    {
    }


    // Updates absoluteChildPositions based on relativeChildPositions' values.
    private void RecalculateAbsoluteChildPositions()
    {
        if(relativeChildPositions.Length != 0)
        {
            absoluteChildPositions = new GameGridCoords[relativeChildPositions.Length];
            for (int index = 0; index < relativeChildPositions.Length; index++)
            {
                absoluteChildPositions[index] = gridPosition + relativeChildPositions[index];
            }
        }
        else
        {
            absoluteChildPositions = null;
        }
    }
}
