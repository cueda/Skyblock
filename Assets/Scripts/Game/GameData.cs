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
    
    [HideInInspector]
    public int kittenGenerateLevel = 1;		    		    // Upgrade: amount of flowers generated per 5 seconds by kitten
    
    public bool vaseHasSpawned;						        // Check if vase has spawned, for Spawner
    public bool upgraderHasSpawned;		    			    // Check if upgrader has spawned, for Spawner

    public EntityType currentItem = EntityType.FLOWER1;     // Currently selected item

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
	}


    void Start()
    {
        // Deliberately update the private values, to avoid triggering OnFlowersCollectedValueChanged
        lifetimeFlowersCollected = 0;
        flowersCollected = 0;

        DirtCollected = 0;
        VasesCollected = 0;
        KittensCollected = 0;
        UpgradersCollected = 0;

        FlowersRequiredForDirt = baseFlowersRequiredForDirt;
        FlowersRequiredForKitten = baseFlowersRequiredForKitten;

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


    public void PayDirtCost()
    {
        RemoveFlowers(FlowersRequiredForDirt);

        // Wish cost goes up by an amount defined by algorithm set here
        // Cost increases 1.5 times, rounded down.
        FlowersRequiredForDirt += FlowersRequiredForDirt / 2;
    }


    public void PayKittenCost()
    {
        RemoveFlowers(FlowersRequiredForKitten);

        // Kitten cost goes up by an amount defined by algorithm set here
        // Cost increases 1.2 times, rounded down.
        FlowersRequiredForKitten += FlowersRequiredForKitten / 5;
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


    void OnItemSelected(EntityType itemType)
    {
        currentItem = itemType;
    }
}
