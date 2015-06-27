using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private Image spriteCurrent;

    
    void Awake()
    {
        EventManager.Game.OnStateSet += OnStateSet;
    }


    void OnEnable()
    {
        // Ignore the EntityType.DIRT - it's unused and serves as a placeholder
        OnItemSelected(EntityType.DIRT);
        EventManager.Game.OnItemSelected += OnItemSelected;
    }


    void OnDisable()
    {
        EventManager.Game.OnItemSelected -= OnItemSelected;
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
            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    void OnItemSelected(EntityType unused)
    {
        StartCoroutine(WaitFrameAndUpdateSprite());
    }


    void OnStateSet(GameState.State state)
    {
        if(state == GameState.State.MOVE || state == GameState.State.INVENTORY)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    private IEnumerator WaitFrameAndUpdateSprite()
    {
        yield return null;
        spriteCurrent.sprite = inventoryManager.GetImageForCurrentItem();
    }
}
