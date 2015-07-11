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
        COUNT_BALLOON_DIRT_COST,
        //COUNT_KITTENS,
        COUNT_BALLOON_KITTEN_COST
    }


    void Awake()
    {
        textDisplay = GetComponent<Text>();

        switch(typeOfText)
        {
            case TextType.COUNT_BALLOON_DIRT_COST:
                EventManager.Values.OnDirtCostChanged += UpdateText;
                break;
            case TextType.COUNT_BALLOON_KITTEN_COST:
                EventManager.Values.OnKittenCostChanged += UpdateText;
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
            case TextType.COUNT_BALLOON_DIRT_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.GetDirtCost();
                break;
            case TextType.COUNT_BALLOON_KITTEN_COST:
                textDisplay.text = "Flowers: \n" + GameData.Instance.GetKittenCost();
                break;
            default:
                break;
        }
	}
}
