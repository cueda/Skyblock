using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FileData
{
    #region Data classes

    [System.Serializable]
    public class FileGameData
    {
        public int lifetimeFlowersCollected;
        public int flowersCollected;
        public int dirtCollected;
        public int kittensCollected;
        public int vasesCollected;
        public int upgradersCollected;
        public int saplingsCollected;
        public int woodCollected;
        public int workshopsCollected;
        public int kittenHousesCollected;
        public int flowersRequiredForDirt;
        public int flowersRequiredForKitten;
        public int flowersRequiredForSapling;
        public int woodRequiredForKittenHouse;
        public int flowerValueLevel;
        public int kittenStorageLevel;
        public bool vaseHasSpawned;
        public bool upgraderHasSpawned;
        public bool workshopHasSpawned;
        public bool IsShovelUnlocked;
    }


    // Child class for serializing data
    // Extra data holds up to 8 floats currently - change here
    [Serializable]
    public class FileGridEntityData
    {
        public short posX;
        public short posY;

        public GridEntityType entityType;
        public int[] extraData;

    }

    #endregion

    public FileGameData gameData;
    public List<FileGridEntityData> gameEntities;

    
    public FileData()
    {
        gameData = new FileGameData();
        gameEntities = new List<FileGridEntityData>();
    }

    public void PopulateData()
    {
        PopulateGameData();
        PopulateGridData();
    }

    private void PopulateGameData()
    {
        gameData.lifetimeFlowersCollected = GameData.Instance.LifetimeFlowersCollected;
        gameData.flowersCollected = GameData.Instance.FlowersCollected;
        gameData.dirtCollected = GameData.Instance.DirtCollected;
        gameData.kittensCollected = GameData.Instance.KittensCollected;
        gameData.vasesCollected = GameData.Instance.VasesCollected;
        gameData.upgradersCollected = GameData.Instance.UpgradersCollected;
        gameData.saplingsCollected = GameData.Instance.SaplingsCollected;
        gameData.woodCollected = GameData.Instance.WoodCollected;
        gameData.workshopsCollected = GameData.Instance.WorkshopsCollected;
        gameData.kittenHousesCollected = GameData.Instance.KittenHousesCollected;
        gameData.flowersRequiredForDirt = GameData.Instance.FlowersRequiredForDirt;
        gameData.flowersRequiredForKitten = GameData.Instance.FlowersRequiredForKitten;
        gameData.flowersRequiredForSapling = GameData.Instance.FlowersRequiredForSapling;
        gameData.woodRequiredForKittenHouse = GameData.Instance.WoodRequiredForKittenHouse;
        gameData.flowerValueLevel = GameData.Instance.FlowerValueLevel;
        gameData.kittenStorageLevel = GameData.Instance.KittenStorageLevel;
        gameData.vaseHasSpawned = GameData.Instance.vaseHasSpawned;
        gameData.upgraderHasSpawned = GameData.Instance.upgraderHasSpawned;
        gameData.workshopHasSpawned = GameData.Instance.workshopHasSpawned;
        gameData.IsShovelUnlocked = GameData.Instance.IsShovelUnlocked;
    }


    /// <summary>
    /// Fills in grid data to gameEntities.
    /// </summary>
    private void PopulateGridData()
    {
        // Create a FileGridEntityData object for each GridEntity
        // Then add data to it and append to list of gameEntities
        foreach (GridEntity ge in GameGrid.Instance.GetGameGridTiles().Values)
        {
            if(!(ge is FillerEntity))
            {
                // Create new FileGridEntityData
                FileGridEntityData entityData = new FileGridEntityData();
            
                // Add position data
                entityData.posX = ge.gridPosition.x;
                entityData.posY = ge.gridPosition.y;

                // Add GridEntityType and additional data using overloaded GridEntity functions specified in each entity
                entityData.entityType = ge.GetGridEntityType();
                entityData.extraData = ge.GenerateExtraSaveData();
            
                gameEntities.Add(entityData);
            }            
        }
    }
}
