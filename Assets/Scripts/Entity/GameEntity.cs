using UnityEngine;
using System;
using System.Collections;

public class GameEntity : MonoBehaviour 
{
    [HideInInspector]
    public GameGridCoords gridPosition { get; private set; }

    public void SetGridPosition(GameGridCoords coords)
    {
        gridPosition = new GameGridCoords(coords.x, coords.y);
    }
}
