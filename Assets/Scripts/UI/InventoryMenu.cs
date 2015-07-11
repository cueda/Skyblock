using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryMenu : MonoBehaviour 
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

    private int currentIndex;


	void Awake() 
	{
        if (selectableEntities.Length != entityImages.Length)
        {
            Debug.LogError("SelectableEntities array and EntityImages array are not the same size!");
        }
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


    // Takes in controller input 
    // Calls MoveSelectionNext, MoveSelectionPrevious and 
    void Update()
    {
        if (GameState.IsCurrentState(GameState.State.INVENTORY))
        {
            if (AgnosticInputHandler.IsLeftPressed)
            {
                MoveSelectionPrev();
            }
            else if (AgnosticInputHandler.IsRightPressed)
            {
                MoveSelectionNext();
            }
            else if (AgnosticInputHandler.IsActionPressed)
            {
                ConfirmSelection();
            }
        }
    }


    void MoveSelectionPrev()
    {
        // Decrement currentIndex and wrap around if out of bounds
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = selectableEntities.Length - 1;
        }
        UpdateSelectorImages();
    }


	void MoveSelectionNext()
    {
        // Increment currentIndex and wrap around if out of bounds
        currentIndex++;
        if (currentIndex >= selectableEntities.Length)
        {
            currentIndex = 0;
        }
        UpdateSelectorImages();
    }


    void ConfirmSelection()
    {
        EventManager.Game.OnItemSelected(selectableEntities[currentIndex]);
    }


    void OnStateSet(GameState.State state)
    {
        if(state == GameState.State.INVENTORY)
        {
            selectionPanel.SetActive(true);
            currentIndex = Array.IndexOf<EntityType>(selectableEntities, GameData.Instance.currentItem);
            UpdateSelectorImages();
        }
        else if(state == GameState.State.MOVE)
        {
            selectionPanel.SetActive(false);
        }
    }


    // Updates the current, previous, and next selection item displays.
    void UpdateSelectorImages()
    {
        // Set updated images to current, previous, next entity icons
        currentImage.sprite = entityImages[currentIndex];
        if(currentIndex <= 0)
        {
            previousImage.sprite = entityImages[selectableEntities.Length - 1];
        }
        else
        {
            previousImage.sprite = entityImages[currentIndex - 1];
        }

        if(currentIndex + 1 >= selectableEntities.Length)
        {
            nextImage.sprite = entityImages[0];
        }
        else
        {
            nextImage.sprite = entityImages[currentIndex + 1];
        }
    }


    // Selects and saves an item selection, and returns to Move mode.
	void OnItemSelected(EntityType itemType) 
	{
        GameData.Instance.currentItem = itemType;
        EventManager.Game.OnStateSet(GameState.State.MOVE);
	}


    public void OnButtonLeftPressed()
    {
        MoveSelectionPrev();
    }


    public void OnButtonRightPressed()
    {
        MoveSelectionNext();
    }


    public void OnButtonConfirmPressed()
    {
        ConfirmSelection();
    }

    
    public Sprite GetImageForCurrentItem()
    {
        int index = Array.IndexOf<EntityType>(selectableEntities, GameData.Instance.currentItem);
        return entityImages[index];
    }
}
