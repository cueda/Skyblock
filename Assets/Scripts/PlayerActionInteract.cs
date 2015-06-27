using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Handles player's interaction with entities on the GameGrid.
/// Serves as a compartment of PlayerAction, to prevent bloating the code.
/// </summary>
[RequireComponent(typeof(PlayerAction))]
public class PlayerActionInteract : MonoBehaviour
{
    private PlayerGridPosition playerGridPosition;      


	void Awake()
    {
        playerGridPosition = GetComponent<PlayerGridPosition>();
	}
	
    /// <summary>
    /// Interacts with the entity at player's position.
    /// </summary>
	public void InteractWithEntity()
    {
        GameObject entity = GameGrid.Instance.GetObjectAt(playerGridPosition.GetPosition());

        if(entity != null)
        {
            switch(entity.tag)
            {
                case "EntityFlower":
                    InteractWithFlower(entity);
                    break;
                case "EntityKitten":
                    InteractWithKitten(entity);
                    break;
                case "EntityVase":
                    InteractWithVase(entity);
                    break;
                case "EntityUpgrader":
                    InteractWithUpgrader(entity);
                    break;
                default:
                    Debug.LogError("Tag '" + entity.tag + "' not found in list.");
                    break;
            }
        }
    }

    #region Interactions

    /// <summary>
    /// Pick flower and remove flower object from game and game grid.
    /// </summary>
    private void InteractWithFlower(GameObject flowerObject)
    {
        FlowerEntity flower = flowerObject.GetComponent<FlowerEntity>();
        if (flower.growthLevel == 2)
        {
            GameData.Instance.AddFlowers(GameData.Instance.flowerValueLevel);
            // TODO: create a pool instead of destroying objects
            ObjectReferences.spawner.RemoveObject(flowerObject);
        }
    }

    // TODO: complete kitten interaction
    private void InteractWithKitten(GameObject kittenObject)
    {

    }


    // TODO: complete vase interaction
    private void InteractWithVase(GameObject vaseObject)
    {

    }


    // TODO: complete upgrader interaction
    private void InteractWithUpgrader(GameObject upgraderObject)
    {

    }

    #endregion
}
