using UnityEngine;
using System;
using System.Collections;

public class DataInitialization : MonoBehaviour 
{
	void Awake() 
	{
        Debug.Log("Searching for save data under " + Application.persistentDataPath + "/saveGame.data");
        FileSerializer.Load();
	}
}
