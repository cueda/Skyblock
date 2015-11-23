using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryMenu : MonoBehaviour 
{    
    [SerializeField]
    private ItemEntityType[] selectableEntities;
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

    private bool[] isNonzeroQuantity;
    private int currentIndex;
    

	void Awake() 
	{
        if (selectableEntities.Length != entityImages.Length)
        {
            Debug.LogError("SelectableEntities array and EntityImages array are not the same size!");
        }

        isNonzeroQuantity = new bool[selectableEntities.Length];

        EventManager.Game.OnStateSet += OnStateSet;
        EventManager.Game.OnInventoryItemSelected += OnInventoryItemSelected;
        EventManager.Game.OnItemUsed += OnItemUsed;
        EventManager.Values.OnFlowersCollectedChanged += OnItemCollected;
        EventManager.Values.OnDirtCollectedChanged += OnItemCollected;
        EventManager.Values.OnKittensCollectedChanged += OnItemCollected;
        EventManager.Values.OnVasesCollectedChanged += OnItemCollected;
        EventManager.Values.OnUpgradersCollectedChanged += OnItemCollected;
        EventManager.Values.OnSaplingsCollectedChanged += OnItemCollected;
        EventManager.Values.OnWoodCollectedChanged += OnItemCollected;
        EventManager.Values.OnWorkshopsCollectedChanged += OnItemCollected;
        EventManager.Values.OnKittenHousesCollectedChanged += OnItemCollected;
	}
	

    void Start()
    {
        UpdateNonzeroItemList();
        EventManager.Game.OnInventoryItemHighlightChanged(selectableEntities[currentIndex]);
    }


    // Takes in controller input 
    // Calls MoveSelectionNext, MoveSelectionPrevious and ConfirmSelection
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
        currentIndex = FindValidPrevIndex();
        UpdateSelectorImages();
        EventManager.Game.OnInventoryItemHighlightChanged(selectableEntities[currentIndex]);
    }


	void MoveSelectionNext()
    {
        currentIndex = FindValidNextIndex();
        UpdateSelectorImages();
        EventManager.Game.OnInventoryItemHighlightChanged(selectableEntities[currentIndex]);
    }


    void ConfirmSelection()
    {
        EventManager.Game.OnInventoryItemSelected(selectableEntities[currentIndex]);
    }


    // Updates the current, previous, and next selection item displays.
    void UpdateSelectorImages()
    {
        // Set updated images to current, previous, next entity icons
        currentImage.sprite = entityImages[currentIndex];        
        previousImage.sprite = entityImages[FindValidPrevIndex()];
        nextImage.sprite = entityImages[FindValidNextIndex()];
    }

    
    /// <summary>
    /// Updates the valid selection bool array to the newest inventory information. 
    /// Also performs reassignment of selected item to next valid item, if current selection becomes invalid.
    /// </summary>
    void UpdateNonzeroItemList()
    {
        for(int index = 0; index < selectableEntities.Length; index++)
        {            
            switch(selectableEntities[index])
            {
                case ItemEntityType.BALLOON:
                    isNonzeroQuantity[index] = true;
                    break;

                case ItemEntityType.FLOWER:
                    isNonzeroQuantity[index] = true;
                    break;

                case ItemEntityType.DIRT:
                    if(GameData.Instance.DirtCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.SHOVEL:
                    if (GameData.Instance.IsShovelUnlocked)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                    }
                    break;

                case ItemEntityType.KITTEN:
                    if (GameData.Instance.KittensCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.UPGRADER:
                    if (GameData.Instance.UpgradersCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.SAPLING:
                    if (GameData.Instance.SaplingsCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.WOOD:
                    if (GameData.Instance.WoodCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.WORKSHOP:
                    if (GameData.Instance.WorkshopsCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.KITTENHOUSE:
                    if (GameData.Instance.KittenHousesCollected > 0)
                    {
                        isNonzeroQuantity[index] = true;
                    }
                    else
                    {
                        isNonzeroQuantity[index] = false;
                        if (currentIndex == index)
                        {
                            SelectNextValidItem();
                        }
                    }
                    break;

                case ItemEntityType.VASE:
                    break;

                default:
                    Debug.LogError("EntityType not implemented in InventoryMenu.UpdateNonzeroItemList().");
                    break;
            }
        }

        UpdateSelectorImages();
    }


    /// <summary>
    /// Selects the next valid item in inventory.
    /// For use when current inventory item is depleted.
    /// </summary>
    void SelectNextValidItem()
    {
        currentIndex = FindValidNextIndex();
        EventManager.Game.OnInventoryItemHighlightChanged(selectableEntities[currentIndex]);
        ConfirmSelection();
    }

    
    /// <summary>
    /// Finds and returns the index of the next valid nonzero item in inventory.
    /// Will return current index as the next valid if it loops around once.
    /// </summary>
    int FindValidNextIndex()
    {
        int iteratorIndex = currentIndex + 1;
        while(iteratorIndex != currentIndex)
        {
            if(iteratorIndex >= selectableEntities.Length)
            {
                iteratorIndex = 0;
            }

            if(isNonzeroQuantity[iteratorIndex])
            {
                break;
            }

            iteratorIndex++;
        }
        return iteratorIndex;
    }


    /// <summary>
    /// Finds and returns the index of the previous valid nonzero item in inventory.
    /// Will return current index as the previous valid if it loops around once.
    /// </summary>
    int FindValidPrevIndex()
    {
        int iteratorIndex = currentIndex - 1;
        while (iteratorIndex != currentIndex)
        {
            if (iteratorIndex < 0)
            {
                iteratorIndex = selectableEntities.Length - 1;
            }

            if (isNonzeroQuantity[iteratorIndex])
            {
                break;
            }

            iteratorIndex--;
        }
        return iteratorIndex;
    }


    void OnStateSet(GameState.State state)
    {
        if (state == GameState.State.INVENTORY)
        {
            selectionPanel.SetActive(true);
            currentIndex = Array.IndexOf<ItemEntityType>(selectableEntities, GameData.Instance.currentItem);
            UpdateSelectorImages();
        }
        else if (state == GameState.State.MOVE)
        {
            selectionPanel.SetActive(false);
        }
    }


    // Selects and saves an item selection, and returns to Move mode.
	void OnInventoryItemSelected(ItemEntityType itemType) 
	{
        EventManager.Game.OnStateSet(GameState.State.MOVE);
	}


    void OnItemUsed(ItemEntityType itemType)
    {
        // For efficiency's sake, remove items which are not placeables
        if(itemType != ItemEntityType.FLOWER || itemType != ItemEntityType.SHOVEL)
        {
            UpdateNonzeroItemList();
        }
    }


    void OnItemCollected(int unused1, int unused2)
    {
        UpdateNonzeroItemList();
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
        int index = Array.IndexOf<ItemEntityType>(selectableEntities, GameData.Instance.currentItem);
        return entityImages[index];
    }


    public ItemEntityType GetCurrentHighlightedItem()
    {
        return selectableEntities[currentIndex];
    }
}
