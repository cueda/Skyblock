using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Serves as a screen-wide button behind all other elements.
/// Functions as a cancel button in most cases.
/// </summary>
public class FullscreenCancelButton : MonoBehaviour 
{
    [SerializeField]
    private InventoryMenu inventoryMenu;
    [SerializeField]
    private BalloonMenu balloonMenu;
    [SerializeField]
    private UpgradeMenu upgradeMenu;


	void Awake() 
	{
        EventManager.Game.OnStateSet += OnStateSet;
        gameObject.SetActive(false);
	}

	
    // Tentatively, sets this button as active in all cases except Move
	void OnStateSet(GameState.State state) 
	{
        switch (state)
        {
            case GameState.State.MOVE:
                gameObject.SetActive(false);
                break;
            default:
                gameObject.SetActive(true);
                break;

        }
	}

    public void OnClick()
    {
        if(GameState.IsCurrentState(GameState.State.BALLOON))
        {
            balloonMenu.CloseBalloonMenu();
        }
        else if(GameState.IsCurrentState(GameState.State.UPGRADE))
        {
            upgradeMenu.CloseUpgradeMenu();
        }
        else if(GameState.IsCurrentState(GameState.State.INVENTORY))
        {
            inventoryMenu.OnButtonConfirmPressed();
        }
    }
}
