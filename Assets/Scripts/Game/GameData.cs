using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameData : MonoBehaviour
{
    #region Game data values

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

    private int flowersRequiredForDirt;
    public int FlowersRequiredForDirt
    {
        get { return flowersRequiredForDirt; }
        set { int old = flowersRequiredForDirt; flowersRequiredForDirt = value; EventManager.Values.OnDirtCostChanged(old, value); }
    }

    private int flowersRequiredForKitten;
    private int FlowersRequiredForKitten
    {
        get { return flowersRequiredForKitten; }
        set { int old = flowersRequiredForKitten; flowersRequiredForKitten = value; EventManager.Values.OnKittenCostChanged(old, value); }
    }

    #endregion

    [SerializeField]
    private int baseFlowersRequiredForDirt = 5;			    // Flowers required to perform a Wish at base, only used during initialization
    [SerializeField]
    private int baseFlowersRequiredForKitten = 20;		    // Flowers required to buy a Kitten at base, only used during initialization

    public int flowerValueLevel = 1;		    		    // Upgrade: value of a single flower when collected
    public int kittenGenerateLevel = 1;		    		    // Upgrade: amount of flowers generated per 5 seconds by kitten
    public int kittenStorageLevel = 1;				        // Upgrade: maximum storage of kittens
    public bool vaseHasSpawned;						        // Check if vase has spawned, for Spawner
    public bool upgraderHasSpawned;		    			    // Check if upgrader has spawned, for Spawner
    public bool upgraderIsActive;		    			    // Check if interacting with upgrader

    public EntityType currentItem = EntityType.FLOWER1;     // Currently selected item

    public SoundManager soundManager;
    

	public static GameData Instance {get; private set;}


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
    }

    
	public void PayDirtCost()
	{
		FlowersCollected -= FlowersRequiredForDirt;

		// Wish cost goes up by an amount defined by algorithm set here
		// Cost increases 1.5 times, rounded down.
        FlowersRequiredForDirt += FlowersRequiredForDirt / 2;
	}


	public void PayKittenCost()
	{
		FlowersCollected -= FlowersRequiredForKitten;
		
		// Kitten cost goes up by an amount defined by algorithm set here
		// Cost increases 1.2 times, rounded down.
		FlowersRequiredForKitten += FlowersRequiredForKitten/5;
	}


	public void AddFlowers(int count)
	{
		FlowersCollected += count;
		LifetimeFlowersCollected += count;

        FloatingTextManager.Instance.SpawnTextPrefab("+ " + count + " Flower");
		// Instantiate an InfoText GameObject, indicating flower gain.
		// Display above player position.
		//Vector3 playerPos = playerRef.transform.position;
		//GameObject iText = (GameObject)Instantiate(infoText, new Vector3(playerPos.x, playerPos.y + 2, -5), Quaternion.identity);
		//iText.GetComponent<TextMeshDisplay>().SetText("+ " + count + " Flower");
		
		// Play a sound when gaining flowers.
		//soundManager.PlayRandomNote();
	}


	public void RemoveFlowers(int count)
	{
        FlowersCollected -= count;
	}


	public int GetDirtCost()
	{
		return FlowersRequiredForDirt; 
	}


	public int GetKittenCost()
	{
		return FlowersRequiredForKitten;
	}


    #region Costs

    public int GetFlowerValueUpgradeCost()
	{
		// 100 * ((CURLVL-1)*CURLVL + 1)
		return 100 * (((flowerValueLevel-1)*flowerValueLevel) + 1);
	}
	
	
	public int GetKittenGenerateUpgradeCost()
	{
		// (100 * CURLVL^3) - ((CURLVL-1 * 200))
		return (100 * kittenGenerateLevel * kittenGenerateLevel * kittenGenerateLevel) -
			((kittenGenerateLevel - 1) * 200);
	}


	public int GetKittenStorageUpgradeCost()
	{
		// 100 * ((CURLVL-1)*CURLVL + 1)
		return 100 * (((kittenStorageLevel-1)*kittenStorageLevel) + 1);
	}

    #endregion


    void OnItemSelected(EntityType itemType)
    {
        currentItem = itemType;
    }
}
