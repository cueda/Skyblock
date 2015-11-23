using UnityEngine;
using System;
using System.Collections;

public class WoodPlatformEntity : GridEntity 
{
    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.WOOD;
    }
}
