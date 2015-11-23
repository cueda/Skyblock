using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private InventoryMenu inventoryManager;
    [SerializeField]
    private Image spriteCurrent;

    private Button button;

    
    void Awake()
    {
        button = GetComponent<Button>();

        EventManager.Game.OnStateSet += OnStateSet;
    }


    void OnEnable()
    {
        // Ignore the EntityType.DIRT - it's unused and serves as a placeholder
        OnInventoryItemSelected(ItemEntityType.DIRT);
        EventManager.Game.OnInventoryItemSelected += OnInventoryItemSelected;
    }


    void OnDisable()
    {
        EventManager.Game.OnInventoryItemSelected -= OnInventoryItemSelected;
    }


    // Serves as a substitute for controller and keyboard input for accessing Inventory menu.
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            OnButtonPressed();
        }
    }


    public void OnButtonPressed()
    {
        if (GameState.IsCurrentState(GameState.State.MOVE))
        {
            EventManager.Game.OnStateSet(GameState.State.INVENTORY);
        }
        else if (GameState.IsCurrentState(GameState.State.INVENTORY))
        {
            inventoryManager.OnButtonConfirmPressed();
            //EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    void OnInventoryItemSelected(ItemEntityType unused)
    {
        StartCoroutine(WaitFrameAndUpdateSprite());
    }


    void OnStateSet(GameState.State state)
    {
        if(state == GameState.State.MOVE || state == GameState.State.INVENTORY)
        {
            button.interactable = true;
            spriteCurrent.color = button.colors.normalColor;
        }
        else
        {
            button.interactable = false;
            spriteCurrent.color = button.colors.disabledColor;
        }
    }


    private IEnumerator WaitFrameAndUpdateSprite()
    {
        yield return null;
        spriteCurrent.sprite = inventoryManager.GetImageForCurrentItem();
    }
}
