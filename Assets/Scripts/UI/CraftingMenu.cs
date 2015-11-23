using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject craftingUIPanel;
    [SerializeField]
    private GameObject firstButton;

    void Awake()
    {
        EventManager.Game.OnStateSet += OnStateSet;
    }

    void OnStateSet(GameState.State state)
    {
        if (state == GameState.State.CRAFTING)
        {
            craftingUIPanel.SetActive(true);
            if (!TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
        else
        {
            craftingUIPanel.SetActive(false);
        }
    }

    // For use with CraftingMenu button
    public void CraftKittenHouse()
    {
        if (GameData.Instance.WoodCollected >= GameData.Instance.WoodRequiredForKittenHouse)
        {
            GameData.Instance.PayKittenHouseCost();
            GameData.Instance.KittenHousesCollected++;
            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    // For use with CraftingMenu button
    public void CloseCraftingMenu()
    {
        EventManager.Game.OnStateSet(GameState.State.MOVE);
    }
}

