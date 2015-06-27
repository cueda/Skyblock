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

    public void UseCurrentItem()
    {
        switch (GameData.Instance.currentItem)
        {
            case EntityType.DIRT:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;
            case EntityType.SHOVEL:
                EventManager.Game.OnStateSet(GameState.State.TARGET);
                break;
            case EntityType.FLOWER1:
                if (!GameGrid.Instance.IsCoordinatesOccupied(playerGridPosition.GetPosition()))
                {
                    Debug.Log("Plant a flower here!");
                }
                break;
            case EntityType.KITTEN:
                if (!GameGrid.Instance.IsCoordinatesOccupied(playerGridPosition.GetPosition()))
                {
                    Debug.Log("Plant a kitten here!");
                }
                break;
            case EntityType.VASE:
                if (!GameGrid.Instance.IsCoordinatesOccupied(playerGridPosition.GetPosition()))
                {
                    Debug.Log("Plant a vase here!");
                }
                break;
            case EntityType.COMPUTER:
                if (!GameGrid.Instance.IsCoordinatesOccupied(playerGridPosition.GetPosition()))
                {
                    Debug.Log("Plant a computer here!");
                }
                break;
            default:
                break;
        }

        // TODO: Adapt this to non-targeting system, taken from Targeter
        // ========================================================================================================================
        // For all blocks requiring a floor, check for object in space below.
        //else if (GameGrid.Instance.IsCoordinatesOccupied(currentGridPosition + new GameGridCoords(-Vector2.up*2)))
        //{
        //    // Check that this space is a dirt/grass block.
        //    string selectedObjTag = GameGrid.Instance.GetObject(currentGridPosition + new GameGridCoords(-Vector2.up * 2)).tag;
        //    if(selectedObjTag.Equals("GroundDirt") || selectedObjTag.Equals("GroundGrass"))
        //    {
        //        // If selected item is flower
        //        if(GameData.Instance.currentItem.Equals ("flower"))
        //        {
        //            // Spawn a flower at targeted location.
        //            spawner.SpawnFlower(currentGridPosition);
        //        }

        //        // If selected item is vase
        //        if(GameData.Instance.currentItem.Equals ("vase") && GameData.Instance.vasesCollected > 0)
        //        {
        //            // Spawn a vase at targeted location and consume one vase resource.
        //            spawner.SpawnVase(currentGridPosition);
        //            GameData.Instance.vasesCollected--;
        //        }

        //        // If selected item is kitten
        //        if(GameData.Instance.currentItem.Equals ("kitten") && GameData.Instance.kittensCollected > 0)
        //        {
        //            // Spawn a kitten at targeted location and consume one kitten resource.
        //            spawner.SpawnKitten(currentGridPosition);
        //            GameData.Instance.kittensCollected--;
        //        }

        //        // If selected item is upgrader
        //        if(GameData.Instance.currentItem.Equals ("upgrader") && GameData.Instance.upgraderCollected > 0)
        //        {
        //            // Spawn an upgrader at targeted location and consume one upgrader resource.
        //            spawner.SpawnUpgrader(currentGridPosition);
        //            GameData.Instance.upgraderCollected--;
        //        }
        //    }
        //}
    }
}
