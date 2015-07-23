using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A falling gift box.
/// Set contents on instantiation (or at least before player can collect it)
/// </summary>
public class GiftEntity : MonoBehaviour
{
    [SerializeField]
    private Sprite normalGiftSprite;
    [SerializeField]
    private Sprite specialGiftSprite;
    [HideInInspector]
    public Contents contents;

    private SpriteRenderer spRenderer;


    public enum Contents
    {
        DIRT,
        KITTEN,
        UPGRADER,
        SOMETHINGELSE
    }

    
    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }


    // Set color to special gift if marked as special.
    public void SetSpecialState(bool isSpecial)
    {
        if(isSpecial)
        {
            spRenderer.sprite = specialGiftSprite;
        }
        else
        {
            spRenderer.sprite = normalGiftSprite;
        }
    }
    

	void OnTriggerEnter2D(Collider2D other)
    {
        // If collided with player, add appropriate resource or activate respective effect based on Contents.
        // TODO: pooling system
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
                case Contents.UPGRADER:
                    GameData.Instance.UpgradersCollected++;
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
