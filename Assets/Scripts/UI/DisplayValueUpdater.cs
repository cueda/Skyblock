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
        //COUNT_KITTENS,
        BALLOON_DIRT_COST,
        BALLOON_KITTEN_COST,
        UPGRADE_FLOWERVALUE_COST,
        UPGRADE_KITTENSTORE_COST,
        UPGRADE_FLOWERVALUE_LABEL,
        UPGRADE_KITTENSTORE_LABEL
    }


    void Awake()
    {
        textDisplay = GetComponent<Text>();

        switch(typeOfText)
        {
            case TextType.BALLOON_DIRT_COST:
                EventManager.Values.OnDirtCostChanged += UpdateText;
                break;
            case TextType.BALLOON_KITTEN_COST:
                EventManager.Values.OnKittenCostChanged += UpdateText;
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
            default:
                break;
        }

        UpdateText(0, 0);
    }


	void UpdateText(int unused, int unused2)
    {
        switch(typeOfText)
        {
            case TextType.BALLOON_DIRT_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.FlowersRequiredForDirt;
                break;
            case TextType.BALLOON_KITTEN_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.FlowersRequiredForKitten;
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
            default:
                break;
        }
	}
}
