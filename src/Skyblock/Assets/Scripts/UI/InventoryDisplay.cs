using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryDisplay : MonoBehaviour 
{
    [SerializeField]
    private InventoryMenu inventoryMenu;

    private Text textBox;

	void Awake() 
	{
        textBox = GetComponent<Text>();

        EventManager.Game.OnInventoryItemHighlightChanged += UpdateDisplay;
        EventManager.Game.OnItemUsed += UpdateDisplayOnItemUsed;
        EventManager.Values.OnFlowersCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnDirtCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnKittensCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnVasesCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnUpgradersCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnWoodCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnWorkshopsCollectedChanged += UpdateDisplayOnItemCollected;
        EventManager.Values.OnKittenHousesCollectedChanged += UpdateDisplayOnItemCollected;
	}
	

	void UpdateDisplay(ItemEntityType newType) 
	{
	    switch(newType)
        {
            case ItemEntityType.FLOWER:
                textBox.text = "Flower Seeds:\nUnlimited";
                break;

            case ItemEntityType.BALLOON:
                textBox.text = "Balloons:\nUsable";
                break;

            case ItemEntityType.DIRT:
                textBox.text = "Dirt Blocks:\n" + GameData.Instance.DirtCollected.ToString() + " in stock";
                break;

            case ItemEntityType.SHOVEL:
                textBox.text = "Shovel:\nUsable";
                break;

            case ItemEntityType.KITTEN:
                textBox.text = "Kittens:\n" + GameData.Instance.KittensCollected.ToString() + " in stock";
                break;

            case ItemEntityType.UPGRADER:
                textBox.text = "Upgrader:\nPlaceable";
                break;

            case ItemEntityType.SAPLING:
                textBox.text = "Saplings:\n" + GameData.Instance.SaplingsCollected.ToString() + " in stock";
                break;

            case ItemEntityType.WOOD:
                textBox.text = "Wood panels:\n" + GameData.Instance.WoodCollected.ToString() + " in stock";
                break;

            case ItemEntityType.WORKSHOP:
                textBox.text = "Workshop:\nPlaceable";
                break;

            case ItemEntityType.KITTENHOUSE:
                textBox.text = "Kitten House:\n" + GameData.Instance.KittenHousesCollected.ToString() + " in stock";
                break;
                
            default:
                Debug.Log("Entity type not accounted for in InventoryDisplay.");
                break;
        }
	}


    void UpdateDisplayOnItemUsed(ItemEntityType unused)
    {
        UpdateDisplay(GameData.Instance.currentItem);
    }


    /// <summary>
    /// Update display when an item is collected.
    /// If not in Inventory menu, update the currently equipped item (from GameData.)
    /// If in Inventory menu, update the currently highlighted item (from InventoryMenu.)
    /// </summary>
    void UpdateDisplayOnItemCollected(int unused1, int unused2)
    {
        if(!GameState.IsCurrentState(GameState.State.INVENTORY))
        {
            UpdateDisplay(GameData.Instance.currentItem);
        }
        else
        {
            UpdateDisplay(inventoryMenu.GetCurrentHighlightedItem());
        }
    }
}
