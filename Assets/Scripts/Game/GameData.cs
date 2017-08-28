using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameData : MonoBehaviour
{
    #region Game data values

    #region Primary stats and inventory count

    private int lifetimeFlowersCollected;
    public int LifetimeFlowersCollected
    {
        get { return lifetimeFlowersCollected; }
        private set { int old = lifetimeFlowersCollected; lifetimeFlowersCollected = value; EventManager.Values.OnLifetimeFlowersCollectedChanged(old, value); }
    }

    private int flowersCollected;
    public int FlowersCollected
    {
        get { return flowersCollected; }
        private set { int old = flowersCollected; flowersCollected = value; EventManager.Values.OnFlowersCollectedChanged(old, value); }
    }

    private int dirtCollected;
    public int DirtCollected
    {
        get { return dirtCollected; }
        set { int old = dirtCollected; dirtCollected = value; EventManager.Values.OnDirtCollectedChanged(old, value); }
    }

    private int kittensCollected;
    public int KittensCollected
    {
        get { return kittensCollected; }
        set { int old = kittensCollected; kittensCollected = value; EventManager.Values.OnKittensCollectedChanged(old, value); }
    }

    private int vasesCollected;
    public int VasesCollected
    {
        get { return vasesCollected; }
        set { int old = vasesCollected; vasesCollected = value; EventManager.Values.OnVasesCollectedChanged(old, value); }
    }

    private int upgradersCollected;
    public int UpgradersCollected
    {
        get { return upgradersCollected; }
        set { int old = upgradersCollected; upgradersCollected = value; EventManager.Values.OnUpgradersCollectedChanged(old, value); }
    }

    private int saplingsCollected;
    public int SaplingsCollected
    {
        get { return saplingsCollected; }
        set { int old = saplingsCollected; saplingsCollected = value; EventManager.Values.OnSaplingsCollectedChanged(old, value); }
    }

    private int woodCollected;
    public int WoodCollected
    {
        get { return woodCollected; }
        set { int old = woodCollected; woodCollected = value; EventManager.Values.OnWoodCollectedChanged(old, value); }
    }

    private int workshopsCollected;
    public int WorkshopsCollected
    {
        get { return workshopsCollected; }
        set { int old = workshopsCollected; workshopsCollected = value; EventManager.Values.OnWorkshopsCollectedChanged(old, value); }
    }

    private int kittenHousesCollected;
    public int KittenHousesCollected
    {
        get { return kittenHousesCollected; }
        set { int old = kittenHousesCollected; kittenHousesCollected = value; EventManager.Values.OnKittenHousesCollectedChanged(old, value); }
    }

    #endregion

    #region Purchase costs

    private int flowersRequiredForDirt;
    public int FlowersRequiredForDirt
    {
        get { return flowersRequiredForDirt; }
        private set { int old = flowersRequiredForDirt; flowersRequiredForDirt = value; EventManager.Values.OnDirtCostChanged(old, value); }
    }

    private int flowersRequiredForKitten;
    public int FlowersRequiredForKitten
    {
        get { return flowersRequiredForKitten; }
        private set { int old = flowersRequiredForKitten; flowersRequiredForKitten = value; EventManager.Values.OnKittenCostChanged(old, value); }
    }

    private int flowersRequiredForSapling;
    public int FlowersRequiredForSapling
    {
        get { return flowersRequiredForSapling; }
        private set { int old = flowersRequiredForSapling; flowersRequiredForSapling = value; EventManager.Values.OnSaplingCostChanged(old, value); }
    }

    public int WoodRequiredForKittenHouse
    {
        get;
        private set;
    }

    #endregion

    #region Upgrade levels

    // Upgrade: value of a single flower when collected
    private int flowerValueLevel;
    public int FlowerValueLevel
    {
        get { return flowerValueLevel; }
        private set { int old = flowerValueLevel; flowerValueLevel = value; EventManager.Values.OnFlowerValueLevelChanged(old, value); }
    }

    // Upgrade: maximum storage of kittens
    private int kittenStorageLevel;
    public int KittenStorageLevel
	{
        get { return kittenStorageLevel; }
        private set { int old = kittenStorageLevel; kittenStorageLevel = value; EventManager.Values.OnKittenStorageLevelChanged(old, value); }
    }	        

    #endregion

    #endregion

    [SerializeField]
    private int baseFlowersRequiredForDirt = 5;			    // Flowers required to perform a Wish at base, only used during initialization
    [SerializeField]
    private int baseFlowersRequiredForKitten = 20;		    // Flowers required to buy a Kitten at base, only used during initialization
    [SerializeField]
    private int baseFlowersRequiredForSapling = 200;	    // Flowers required to buy a Kitten at base, only used during initialization
    [SerializeField]
    private int baseWoodRequiredForKittenHouse = 100;	    // Flowers required to buy a Kitten at base, only used during initialization
    
    [HideInInspector]
    public int kittenGenerateLevel = 1;		    		    // Upgrade: speed at which kittens generate flowers (not used)
    
    public bool vaseHasSpawned;						        // Value for Spawner to determine new vase spawn
    public bool upgraderHasSpawned;		    			    // Value for Spawner to determine new upgrader spawn
    public bool workshopHasSpawned;                         // Value for Spawner to determine new workshop spawn

    private bool isShovelUnlocked;                          // Value for InventoryMenu to display or hide shovel, once first dirt is obtained
    public bool IsShovelUnlocked
    {
        get { return isShovelUnlocked; }
        private set { isShovelUnlocked = value; }
    }

    public ItemEntityType currentItem = ItemEntityType.FLOWER;      // Currently selected item

    public SoundManager soundManager;
    

	public static GameData Instance {get; private set;}


    #region Initialization

    void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;

        EventManager.Game.OnInventoryItemSelected += OnInventoryItemSelected;
        EventManager.Values.OnDirtCollectedChanged += OnFirstDirtCollected;
	}


    void Start()
    {
        /*
         * 
         * TODO: Load game data from deserialized fileData from save file
         * 
         */
        if (FileSerializer.fileData != null)
        {
            Debug.Log("Beginning loading game state from file data.");
            SpawnFromSaveData();
        }
        else
        {
            SpawnFromDefault();
        }
    }


    private void SpawnFromSaveData()
    {   
        // Deliberately update the private values, to avoid triggering OnFlowersCollectedValueChanged
        lifetimeFlowersCollected = FileSerializer.fileData.gameData.lifetimeFlowersCollected;
        flowersCollected = FileSerializer.fileData.gameData.flowersCollected;

        DirtCollected = FileSerializer.fileData.gameData.dirtCollected;
        VasesCollected = FileSerializer.fileData.gameData.vasesCollected;
        KittensCollected = FileSerializer.fileData.gameData.kittensCollected;
        UpgradersCollected = FileSerializer.fileData.gameData.upgradersCollected;
        SaplingsCollected = FileSerializer.fileData.gameData.saplingsCollected;
        WoodCollected = FileSerializer.fileData.gameData.woodCollected;
        WorkshopsCollected = FileSerializer.fileData.gameData.workshopsCollected;
        KittenHousesCollected = FileSerializer.fileData.gameData.kittenHousesCollected;

        FlowersRequiredForDirt = FileSerializer.fileData.gameData.flowersRequiredForDirt;
        FlowersRequiredForKitten = FileSerializer.fileData.gameData.flowersRequiredForKitten;
        FlowersRequiredForSapling = FileSerializer.fileData.gameData.flowersRequiredForSapling;
        WoodRequiredForKittenHouse = FileSerializer.fileData.gameData.woodRequiredForKittenHouse;

        FlowerValueLevel = FileSerializer.fileData.gameData.flowerValueLevel;
        KittenStorageLevel = FileSerializer.fileData.gameData.kittenStorageLevel;
        vaseHasSpawned = FileSerializer.fileData.gameData.vaseHasSpawned;
        upgraderHasSpawned = FileSerializer.fileData.gameData.upgraderHasSpawned;
        workshopHasSpawned = FileSerializer.fileData.gameData.workshopHasSpawned;
        IsShovelUnlocked = FileSerializer.fileData.gameData.IsShovelUnlocked;
    }


    private void SpawnFromDefault()
    {
        // Deliberately update the private values, to avoid triggering OnFlowersCollectedValueChanged
        lifetimeFlowersCollected = 0;
        flowersCollected = 0;

        DirtCollected = 0;
        VasesCollected = 0;
        KittensCollected = 0;
        UpgradersCollected = 0;
        WoodCollected = 0;
        WorkshopsCollected = 0;

        FlowersRequiredForDirt = baseFlowersRequiredForDirt;
        FlowersRequiredForKitten = baseFlowersRequiredForKitten;
        FlowersRequiredForSapling = baseFlowersRequiredForSapling;
        WoodRequiredForKittenHouse = baseWoodRequiredForKittenHouse;

        flowerValueLevel = 1;
        kittenStorageLevel = 1;

    }

    #endregion

    #region Public value modifiers

    public void AddFlowers(int count)
	{
		FlowersCollected += count;
		LifetimeFlowersCollected += count;

        string pluralFlower = "";
        if(count == 1)
        {
            pluralFlower = " Flower";
        }
        else
        {
            pluralFlower = " Flowers";
        }

        FloatingTextManager.Instance.SpawnTextPrefab("+ " + count + pluralFlower);

		// Play a sound when gaining flowers.
		//soundManager.PlayRandomNote();
	}


	public void RemoveFlowers(int count)
	{
        FlowersCollected -= count;
    }


    public void AddWood(int count)
    {
        WoodCollected += count;
        
        FloatingTextManager.Instance.SpawnTextPrefab("+ " + count + " Wood");

        // Play a sound when gaining flowers.
        //soundManager.PlayRandomNote();
    }


    public void RemoveWood(int count)
    {
        FlowersCollected -= count;
    }


    public void PayDirtCost()
    {
        RemoveFlowers(FlowersRequiredForDirt);

        // Wish cost goes up by an amount defined by algorithm set here
        // Cost increases 1.4 times, rounded down.
        FlowersRequiredForDirt = (int)(FlowersRequiredForDirt * 1.4);
    }


    public void PayKittenCost()
    {
        RemoveFlowers(FlowersRequiredForKitten);

        // Kitten cost goes up by an amount defined by algorithm set here
        // Cost increases 1.3 times, rounded down.
        FlowersRequiredForKitten = (int)(FlowersRequiredForKitten * 1.3);
    }


    public void PaySaplingCost()
    {
        RemoveFlowers(FlowersRequiredForSapling);

        // Sapling cost goes up by an amount defined by algorithm set here
        // Cost increases by double.
        FlowersRequiredForSapling = FlowersRequiredForSapling * 2;
    }


    public void PayKittenHouseCost()
    {
        WoodCollected -= WoodRequiredForKittenHouse;
        
        // No change to cost
    }
    

    /// <summary>
    /// Upgrades flower's value level.
    /// Handles flower payment internally.
    /// </summary>
    public void UpgradeFlowerValueLevel()
    {
        RemoveFlowers(GetFlowerValueUpgradeCost());
        FlowerValueLevel++;
        FloatingTextManager.Instance.SpawnTextPrefab("Growth Lv." + FlowerValueLevel, Color.green);
    }

    
    /// <summary>
    /// Upgrades kitten's storage level.
    /// Handles flower payment internally.
    /// </summary>
    public void UpgradeKittenStorageLevel()
    {
        RemoveFlowers(GetKittenStorageUpgradeCost());
        KittenStorageLevel++;
        FloatingTextManager.Instance.SpawnTextPrefab("Capacity Lv." + KittenStorageLevel, Color.green);
    }

    #endregion

    #region Costs

    public int GetFlowerValueUpgradeCost()
	{
		// 100 * ((CURLVL-1)*CURLVL + 1)
		return 100 * (((FlowerValueLevel-1)*FlowerValueLevel) + 1);
	}
	
	
    //public int GetKittenGenerateUpgradeCost()
    //{
    //    // (100 * CURLVL^3) - ((CURLVL-1 * 200))
    //    return (100 * kittenGenerateLevel * kittenGenerateLevel * kittenGenerateLevel) -
    //        ((kittenGenerateLevel - 1) * 200);
    //}


	public int GetKittenStorageUpgradeCost()
	{
		// 100 * ((CURLVL-1)*CURLVL + 1)
		return 100 * (((KittenStorageLevel-1)*KittenStorageLevel) + 1);
	}

    #endregion


    void OnInventoryItemSelected(ItemEntityType itemType)
    {
        currentItem = itemType;
    }


    void OnFirstDirtCollected(int unused1, int unused2)
    {
        if(DirtCollected > 0 && !IsShovelUnlocked)
        {
            IsShovelUnlocked = true;
        }
    }
}
