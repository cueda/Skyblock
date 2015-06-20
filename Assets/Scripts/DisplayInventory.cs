using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DisplayInventory : MonoBehaviour 
{    
    [SerializeField]
    private EntityType[] selectableEntities;
    [SerializeField]
    private Sprite[] entityImages;

    [SerializeField]
    private GameObject selectionPanel;
    [SerializeField]
    private Image previousImage;
    [SerializeField]
    private Image currentImage;
    [SerializeField]
    private Image nextImage;

    private EntityType highlightedType;             // The currently highlighted item type in the Inventory menu

	void Awake() 
	{
        if (selectableEntities.Length != entityImages.Length)
        {
            Debug.LogError("SelectableEntities array and EntityImages array are not the same size!");
        }
	}
	
    void Start()
    {
        highlightedType = GameData.Instance.currentItem;
    }

	void OnEnable()
	{
        EventManager.Game.OnStateSet += OnStateSet;
        EventManager.Game.OnItemSelected += OnItemSelected;
	}
	
	void OnDisable()
    {
        EventManager.Game.OnStateSet -= OnStateSet;
        EventManager.Game.OnItemSelected -= OnItemSelected;
	}
	
    void OnStateSet(GameState.State state)
    {
        if(state == GameState.State.INVENTORY)
        {
            selectionPanel.SetActive(true);
            highlightedType = GameData.Instance.currentItem;
            SetSelectorImages();
        }
        else if(state == GameState.State.MOVE)
        {
            selectionPanel.SetActive(false);
        }
    }

    void SetSelectorImages()
    {
        // Find currently highlighted EntityType's index in selectableEntities array
        int currentIndex = Array.IndexOf<EntityType>(selectableEntities, highlightedType);

        // Set updated images to current, previous, next entity icons
        currentImage.sprite = entityImages[currentIndex];
        if(currentIndex <= 0)
        {
            previousImage.sprite = entityImages[selectableEntities.Length];
        }
        else
        {
            previousImage.sprite = entityImages[currentIndex - 1];
        }

        if(currentIndex + 1 > selectableEntities.Length)
        {
            nextImage.sprite = entityImages[0];
        }
        else
        {
            nextImage.sprite = entityImages[currentIndex + 1];
        }
    }

	void OnItemSelected(EntityType itemType) 
	{
        //TODO: Fill with item selection logic
	}
}
