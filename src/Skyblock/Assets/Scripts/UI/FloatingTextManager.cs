using UnityEngine;
using System;
using System.Collections;

public class FloatingTextManager : MonoBehaviour 
{
    [SerializeField]
    private Vector3 playerOffset = new Vector3(0, 2f, -5f);
    [SerializeField]
    private GameObject textPrefab;
    

	public static FloatingTextManager Instance {get; private set;}


	void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
	}


    /// <summary>
    /// Creates a gameobject with floating text relative to the player.
    /// Sets text to default color.
    /// </summary>
	public void SpawnTextPrefab(string text)
    {
        /// TODO: Implement pooling system to include created text prefab.
        GameObject textInstance = (GameObject)Instantiate(textPrefab, ObjectReferences.player.position + playerOffset, Quaternion.identity);
        TextMeshDisplay displayText = textInstance.GetComponent<TextMeshDisplay>();
        displayText.SetText(text);
    }


    /// <summary>
    /// Creates a gameobject with floating text relative to the player.
    /// Sets text to user-specified color.
    /// </summary>
    public void SpawnTextPrefab(string text, Color color)
    {
        /// TODO: Implement pooling system to include created text prefab.
        GameObject textInstance = (GameObject)Instantiate(textPrefab, ObjectReferences.player.position + playerOffset, Quaternion.identity);
        TextMeshDisplay displayText = textInstance.GetComponent<TextMeshDisplay>();
        displayText.SetText(text);
        displayText.SetColor(color);
    }
}
