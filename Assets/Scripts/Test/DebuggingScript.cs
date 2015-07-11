using UnityEngine;
using System;
using System.Collections;

public class DebuggingScript : MonoBehaviour 
{
    
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameData.Instance.AddFlowers(10);
            Debug.Log("10 flowers added to GameData.");
        }
    }
}
