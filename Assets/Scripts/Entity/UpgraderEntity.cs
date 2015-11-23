using UnityEngine;
using System;
using System.Collections;

public class UpgraderEntity : GridEntity 
{
	public override void Interact() 
	{
        EventManager.Game.OnStateSet(GameState.State.UPGRADE);
	}


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.UPGRADER;
    }
}
