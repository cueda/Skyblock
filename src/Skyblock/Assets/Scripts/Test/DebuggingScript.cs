using UnityEngine;
using System;
using System.Collections;

public class DebuggingScript : MonoBehaviour 
{
    
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameData.Instance.AddFlowers(500);
            Debug.Log("500 flowers added to inventory.");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameData.Instance.WoodCollected += 100;
            Debug.Log("100 wood added to inventory.");
        }
    }
}
