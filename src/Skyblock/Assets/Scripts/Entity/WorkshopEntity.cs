using UnityEngine;
using System;
using System.Collections;

public class WorkshopEntity : GridEntity
{
    public override void Interact()
    {
        EventManager.Game.OnStateSet(GameState.State.CRAFTING);
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.WORKSHOP;
    }
}
