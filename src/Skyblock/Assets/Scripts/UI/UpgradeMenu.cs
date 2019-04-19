using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradeUIPanel;
    [SerializeField]
    private GameObject firstButton;
    

    void Awake()
    {
        EventManager.Game.OnStateSet += OnStateSet;
    }
    

    void OnStateSet(GameState.State state)
    {
        if (state == GameState.State.UPGRADE)
        {
            upgradeUIPanel.SetActive(true);
            if (!TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
        else
        {
            upgradeUIPanel.SetActive(false);
        }
    }


    // For use with UpgradeMenu button
    public void UpgradeFlowerValue()
    {
        if (GameData.Instance.FlowersCollected >= GameData.Instance.GetFlowerValueUpgradeCost())
        {
            GameData.Instance.UpgradeFlowerValueLevel();
            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    // For use with UpgradeMenu button
    public void UpgradeKittenStorage()
    {
        if (GameData.Instance.FlowersCollected >= GameData.Instance.GetKittenStorageUpgradeCost())
        {
            GameData.Instance.UpgradeKittenStorageLevel();
            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }
    

    // For use with UpgradeMenu button
    public void CloseUpgradeMenu()
    {
        EventManager.Game.OnStateSet(GameState.State.MOVE);
    }
}
