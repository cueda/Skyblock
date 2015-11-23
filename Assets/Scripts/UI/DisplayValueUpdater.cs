using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// Place this script on objects with text that should be updated when GameData values change.
/// </summary>
public class DisplayValueUpdater : MonoBehaviour 
{    
    [SerializeField]
    private TextType typeOfText;

    private Text textDisplay;


    public enum TextType
    {
        //COUNT_FLOWERS,
        //COUNT_DIRT,
        COUNT_KITTENS,
        BALLOON_DIRT_COST,
        BALLOON_KITTEN_COST,
        BALLOON_SAPLING_COST,
        UPGRADE_FLOWERVALUE_COST,
        UPGRADE_KITTENSTORE_COST,
        UPGRADE_FLOWERVALUE_LABEL,
        UPGRADE_KITTENSTORE_LABEL,
        CRAFT_KITTENHOUSE_COST,
        CRAFT_KITTENHOUSE_LABEL,
        KITTENHOUSE_KITTENCOUNT_STOCK
    }


    void Awake()
    {
        textDisplay = GetComponent<Text>();

        switch(typeOfText)
        {
            case TextType.COUNT_KITTENS:
                EventManager.Values.OnKittensCollectedChanged += UpdateText;
                break;
            case TextType.BALLOON_DIRT_COST:
                EventManager.Values.OnDirtCostChanged += UpdateText;
                break;
            case TextType.BALLOON_KITTEN_COST:
                EventManager.Values.OnKittenCostChanged += UpdateText;
                break;
            case TextType.BALLOON_SAPLING_COST:
                EventManager.Values.OnSaplingCostChanged += UpdateText;
                break;
            case TextType.UPGRADE_FLOWERVALUE_COST:
                EventManager.Values.OnFlowerValueLevelChanged += UpdateText;
                break;
            case TextType.UPGRADE_FLOWERVALUE_LABEL:
                EventManager.Values.OnFlowerValueLevelChanged += UpdateText;
                break;
            case TextType.UPGRADE_KITTENSTORE_COST:
                EventManager.Values.OnKittenStorageLevelChanged += UpdateText;
                break;
            case TextType.UPGRADE_KITTENSTORE_LABEL:
                EventManager.Values.OnKittenStorageLevelChanged += UpdateText;
                break;
            case TextType.KITTENHOUSE_KITTENCOUNT_STOCK:
                EventManager.Values.OnKittensCollectedChanged += UpdateText;
                break;
            default:
                break;
        }

        UpdateText(0, 0);
    }


	void UpdateText(int unused, int unused2)
    {
        switch(typeOfText)
        {
            case TextType.COUNT_KITTENS:
                textDisplay.text = "Kittens: \n" + GameData.Instance.KittensCollected;
                break;
            case TextType.BALLOON_DIRT_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.FlowersRequiredForDirt;
                break;
            case TextType.BALLOON_KITTEN_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.FlowersRequiredForKitten;
                break;
            case TextType.BALLOON_SAPLING_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.FlowersRequiredForSapling;
                break;
            case TextType.UPGRADE_FLOWERVALUE_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.GetFlowerValueUpgradeCost();
                break;
            case TextType.UPGRADE_FLOWERVALUE_LABEL:
                textDisplay.text = "Improve Flower Growth Lv [" + GameData.Instance.FlowerValueLevel + "]";
                break;
            case TextType.UPGRADE_KITTENSTORE_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.GetKittenStorageUpgradeCost();
                break;
            case TextType.UPGRADE_KITTENSTORE_LABEL:
                textDisplay.text = "Expand Kitten Storehouses Lv [" + GameData.Instance.KittenStorageLevel + "]";
                break;
            case TextType.CRAFT_KITTENHOUSE_COST:
                textDisplay.text = "Wood Panels:\n" + GameData.Instance.WoodRequiredForKittenHouse;
                break;
            case TextType.KITTENHOUSE_KITTENCOUNT_STOCK:
                textDisplay.text = GameData.Instance.KittensCollected + " kittens in stock";
                break;
            default:
                break;
        }
	}
}
