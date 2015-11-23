using UnityEngine;
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
	}
	
	void OnStateSet(GameState.State state)
	{
	    if(state == GameState.State.BALLOON)
        {
            balloonUIPanel.SetActive(true);
            if(!TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
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
    public void SpawnSaplingBalloon()
    {
        if (GameData.Instance.FlowersCollected >= GameData.Instance.FlowersRequiredForSapling)
        {
            ObjectReferences.spawner.SpawnBalloon(BalloonEntity.RequestType.SAPLING);
            GameData.Instance.PaySaplingCost();

            EventManager.Game.OnStateSet(GameState.State.MOVE);
        }
    }


    // For use with BalloonMenu button
    public void CloseBalloonMenu()
    {
        EventManager.Game.OnStateSet(GameState.State.MOVE);
    }
}
