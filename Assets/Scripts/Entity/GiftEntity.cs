using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A falling gift box.
/// Set contents on instantiation (or at least before player can collect it)
/// </summary>
public class GiftEntity : MonoBehaviour 
{
    [HideInInspector]
    public Contents contents;


    public enum Contents
    {
        DIRT,
        KITTEN,
        SOMETHINGELSE
    }
    

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch(contents)
            {
                case Contents.DIRT:
                    GameData.Instance.DirtCollected++;
                    break;
                case Contents.KITTEN:
                    GameData.Instance.KittensCollected++;
                    break;
                case Contents.SOMETHINGELSE:
                    break;
                default:
                    Debug.LogError("Contents of gift not properly set.");
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
