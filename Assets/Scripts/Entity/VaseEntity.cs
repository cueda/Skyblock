using UnityEngine;
using System;
using System.Collections;

public class VaseEntity : GridEntity 
{
    [SerializeField]
    private bool potato;


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.VASE;
    }


    // Generates extra save data for FlowerEntity.
    public override int[] GenerateExtraSaveData()
    {
        if(potato)
        {
            return new int[] { 1 };
        }
        else
        {
            return new int[] { 0 };
        }
    }


    // Reads in save data from FileSerializer's GameData instance.
    public override void LoadExtraSaveData(int[] extraData)
    {
        potato = extraData[0] == 1 ? true : false;
    }
}
