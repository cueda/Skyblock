using UnityEngine;
using System;
using System.Collections;

public class TestTargeterButton : MonoBehaviour 
{
    private bool isTargeterOn;

	void Awake() 
	{
        isTargeterOn = false;
	}
	
	public void OnButtonPressed() 
	{
        if(isTargeterOn)
        {
            EventManager.Game.OnStateSet(GameState.State.MOVE);
            isTargeterOn = false;
        }
        else
        {
            EventManager.Game.OnStateSet(GameState.State.TARGET);
            isTargeterOn = true;
        }
	}
}
