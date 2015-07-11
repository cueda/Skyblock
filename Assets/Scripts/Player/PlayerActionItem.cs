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
                if (IsObjectPlaceableAt(currentPosition, true))
                {
                    ObjectReferences.spawner.SpawnFlower(currentPosition);
                }
                break;

            case EntityType.KITTEN:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.KittensCollected > 0)
                {
                    ObjectReferences.spawner.SpawnKitten(currentPosition);
                    GameData.Instance.KittensCollected--;
                }
                break;

            case EntityType.VASE:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.VasesCollected > 0)
                {
                    ObjectReferences.spawner.SpawnVase(currentPosition);
                    GameData.Instance.VasesCollected--;
                }
                break;

            case EntityType.UPGRADER:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.UpgradersCollected > 0)
                {
                    ObjectReferences.spawner.SpawnUpgrader(currentPosition);
                    GameData.Instance.UpgradersCollected--;
                }
                break;

            case EntityType.BALLOON:
                EventManager.Game.OnStateSet(GameState.State.BALLOON);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Checks to see if ground is at location.
    /// Parameter matchGrassOnly checks if ground is grass, otherwise returns false.
    /// </summary>
    private bool IsObjectPlaceableAt(GameGridCoords coords, bool matchGrassOnly)
    {
        GameGridCoords downOne = new GameGridCoords(0,-1);
        if (!GameGrid.Instance.IsCoordinatesOccupied(coords))
        {
            GridEntity entity = GameGrid.Instance.GetObjectAt(coords + downOne);
            if(entity != null)
            {
                if(matchGrassOnly)
                {
                    if (((DirtEntity)entity).hasGrownGrass)
                    {
                        return true;
                    }
                }
                else if (entity.tag.Equals("Ground"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
