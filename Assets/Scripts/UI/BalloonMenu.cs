﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class BalloonMenu : MonoBehaviour 
{
    [SerializeField]
    private GameObject balloonUIPanel;
    [SerializeField]
    private GameObject firstButton;

	void Awake() 
	{
        EventManager.Game.OnStateSet += OnStateSet;
        
        balloonUIPanel.SetActive(false);
	}
	
	void OnStateSet(GameState.State state)
	{
	    if(state == GameState.State.BALLOON)
        {
            balloonUIPanel.SetActive(true);
            if(TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
                Debug.Log("Remember to change it back!");
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
        else
        {
            balloonUIPanel.SetActive(false);
        }
	}	


    // For use with BalloonMenu button
    public void SpawnDirtBalloon()
    {
        if (GameData.Instance.FlowersCollected >= GameData.Instance.FlowersRequiredForDirt)
        {
            ObjectReferences.spawner.SpawnBalloon(BalloonEntity.RequestType.DIRT);
            GameData.Instance.PayDirtCost();

            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    // For use with BalloonMenu button
    public void SpawnKittenBalloon()
    {
        if (GameData.Instance.FlowersCollected >= GameData.Instance.FlowersRequiredForKitten)
        {
            ObjectReferences.spawner.SpawnBalloon(BalloonEntity.RequestType.KITTEN);
            GameData.Instance.PayKittenCost();

            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    // For use with BalloonMenu button
    public void SpawnUnimplemented()
    {
        Debug.LogError("That hasn't been implemented yet!");
    }


    // For use with BalloonMenu button
    public void CloseBalloonMenu()
    {
        EventManager.Game.OnStateSet(GameState.State.MOVE);
    }
}