using UnityEngine;
using System;
using System.Collections;

public class InventoryInfoAnim : MonoBehaviour 
{
    [SerializeField]
    private string animName;

    private Animation anim;
    private bool isInInventory;

	void Awake() 
	{
        anim = GetComponent<Animation>();

        EventManager.Game.OnStateSet += OnStateSet;
        isInInventory = false;
	}
	

	void OnStateSet(GameState.State newState)
	{
	    if(newState == GameState.State.INVENTORY && !isInInventory)
        {
            anim[animName].speed = 1.0f;
            anim.Play(animName);
            isInInventory = true;
        }
        else if(newState == GameState.State.MOVE && isInInventory)
        {
            anim[animName].speed = -1.0f;
            anim[animName].time = anim[animName].length;
            anim.Play(animName);
            isInInventory = false;
        }
	}
}
