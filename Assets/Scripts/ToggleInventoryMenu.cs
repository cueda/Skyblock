using UnityEngine;
using System;
using System.Collections;

public class ToggleInventoryMenu : MonoBehaviour
{
    private bool isInventoryOn;

    void Awake()
    {
        isInventoryOn = false;
    }

    public void OnButtonPressed()
    {
        if (isInventoryOn)
        {
            EventManager.Game.OnStateSet(GameState.State.MOVE);
            isInventoryOn = false;
        }
        else
        {
            EventManager.Game.OnStateSet(GameState.State.INVENTORY);
            isInventoryOn = true;
        }
    }
}
