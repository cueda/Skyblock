using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Handles player's interaction with items.
/// Serves as a compartment of PlayerAction, to prevent bloating the code.
/// </summary>
[RequireComponent(typeof(PlayerAction))]
public class PlayerActionItem : MonoBehaviour 
{
    private PlayerGridPosition playerGridPosition;


    void Awake()
    {
        playerGridPosition = GetComponent<PlayerGridPosition>();
    }

    /// <summary>
    /// Uses the current item.
    /// Dirt and Shovel are handled in Targeter.
    /// All other items are currently placed onto player location, provided there is a suitable block beneath.
    /// </summary>
    public void UseCurrentItem()
    {
        GameGridCoords currentPosition = playerGridPosition.GetPosition();

        switch (GameData.Instance.currentItem)
        {
            case EntityType.DIRT:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;
            case EntityType.SHOVEL:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;
            case EntityType.FLOWER1:
                if (IsObjectPlaceableAt(currentPosition))
                {
                    ObjectReferences.spawner.SpawnFlower(currentPosition);
                }
                break;
            case EntityType.KITTEN:
                if (IsObjectPlaceableAt(currentPosition) && GameData.Instance.kittensCollected > 0)
                {
                    ObjectReferences.spawner.SpawnKitten(currentPosition);
                    GameData.Instance.kittensCollected--;
                }
                break;
            case EntityType.VASE:
                if (IsObjectPlaceableAt(currentPosition) && GameData.Instance.vasesCollected > 0)
                {
                    ObjectReferences.spawner.SpawnVase(currentPosition);
                    GameData.Instance.vasesCollected--;
                }
                break;
            case EntityType.UPGRADER:
                if (IsObjectPlaceableAt(currentPosition) && GameData.Instance.upgraderCollected > 0)
                {
                    ObjectReferences.spawner.SpawnUpgrader(currentPosition);
                    GameData.Instance.upgraderCollected--;
                }
                break;
            default:
                break;
        }
    }

    private bool IsObjectPlaceableAt(GameGridCoords coords)
    {
        GameGridCoords downOne = new GameGridCoords(0,-1);
        if (!GameGrid.Instance.IsCoordinatesOccupied(coords))
        {
            if (GameGrid.Instance.GetObjectAt(coords + downOne).tag.Equals("GroundDirt") || GameGrid.Instance.GetObjectAt(coords + downOne).tag.Equals("GroundGrass"))
            {
                return true;
            }
        }
        return false;
    }
}
