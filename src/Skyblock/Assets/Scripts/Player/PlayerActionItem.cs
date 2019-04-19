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
            case ItemEntityType.DIRT:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;

            case ItemEntityType.SHOVEL:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;

            case ItemEntityType.WOOD:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;

            case ItemEntityType.FLOWER:
                if (IsObjectPlaceableAt(currentPosition, true))
                {
                    ObjectReferences.spawner.SpawnFlower(currentPosition);
                }
                break;

            case ItemEntityType.KITTEN:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.KittensCollected > 0)
                {
                    ObjectReferences.spawner.SpawnKitten(currentPosition);
                    GameData.Instance.KittensCollected--;
                }
                break;

            case ItemEntityType.VASE:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.VasesCollected > 0)
                {
                    ObjectReferences.spawner.SpawnVase(currentPosition);
                    GameData.Instance.VasesCollected--;
                }
                break;

            case ItemEntityType.UPGRADER:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.UpgradersCollected > 0)
                {
                    ObjectReferences.spawner.SpawnUpgrader(currentPosition);
                    GameData.Instance.UpgradersCollected--;
                }
                break;

            case ItemEntityType.SAPLING:
                if (IsObjectPlaceableAt(currentPosition, true) && GameData.Instance.SaplingsCollected > 0)
                {
                    ObjectReferences.spawner.SpawnSapling(currentPosition);
                    GameData.Instance.SaplingsCollected--;
                }
                break;

            case ItemEntityType.WORKSHOP:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.WorkshopsCollected > 0)
                {
                    ObjectReferences.spawner.SpawnWorkshop(currentPosition);
                    GameData.Instance.WorkshopsCollected--;
                }
                break;

            case ItemEntityType.KITTENHOUSE:
                if (IsObjectPlaceableAt(currentPosition, false) && GameData.Instance.KittenHousesCollected > 0)
                {
                    ObjectReferences.spawner.SpawnKittenHouse(currentPosition);
                    GameData.Instance.KittenHousesCollected--;
                }
                break;

            case ItemEntityType.BALLOON:
                EventManager.Game.OnStateSet(GameState.State.BALLOON);
                break;

            default:
                Debug.LogError("Behavior not implemented for EntityType.");
                break;
        }
    }


    /// <summary>
    /// Checks to see if ground is at location.
    /// Parameter matchGrassOnly checks if ground is grass, otherwise returns false.
    /// </summary>
    private bool IsObjectPlaceableAt(GameGridCoords coords, bool matchGrassOnly)
    {
        GameGridCoords downOne = new GameGridCoords(0, -1);
        if (!GameGrid.Instance.IsCoordinatesOccupied(coords))
        {
            GridEntity entity = GameGrid.Instance.GetObjectAt(coords + downOne);
            if (entity != null)
            {
                if (matchGrassOnly)
                {
                    if(entity is DirtEntity)
                    {
                        if (((DirtEntity)entity).hasGrownGrass)
                        {
                            return true;
                        }
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
