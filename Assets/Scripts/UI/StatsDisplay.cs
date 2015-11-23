using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StatsDisplay : MonoBehaviour 
{
    private Text textDisplay;

	void Awake()
    {
        textDisplay = GetComponent<Text>();

        EventManager.Values.OnLifetimeFlowersCollectedChanged += UpdateDisplay;
        EventManager.Values.OnFlowersCollectedChanged += UpdateDisplay;
        //EventManager.Values.OnDirtCollectedChanged += UpdateDisplay;
        //EventManager.Values.OnKittensCollectedChanged += UpdateDisplay;
        //EventManager.Values.OnVasesCollectedChanged += UpdateDisplay;
        //EventManager.Values.OnUpgradersCollectedChanged += UpdateDisplay;
	}

    
    void Start()
    {
        // Ignore the placeholder values
        UpdateDisplay(0, 0);
    }
	
	void UpdateDisplay(int unused1, int unused2)
    {
        textDisplay.text =
            "Lifetime Flowers: " + GameData.Instance.LifetimeFlowersCollected.ToString() + "\n" +
            "Flowers: " + GameData.Instance.FlowersCollected.ToString() + "\n"; // +
            //"Dirt Blocks: " + GameData.Instance.DirtCollected.ToString() + "\n" +
            //"Kittens: " + GameData.Instance.KittensCollected.ToString() + "\n" +
            //"Vases: " + GameData.Instance.VasesCollected.ToString() + "\n" +
            //"Upgraders: " + GameData.Instance.UpgradersCollected.ToString();
    }
}
