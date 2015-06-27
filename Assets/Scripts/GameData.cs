using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{
	public int lifetimeFlowersCollected = 0;		    // Flowers collected over lifetime
	public int flowersCollected = 0;			    	// Flowers in inventory
	public int dirtCollected = 0;			    		// Dirt in inventory
	public int vasesCollected = 0;			    		// Vases in inventory
	public int kittensCollected = 0;		    		// Kittens in inventory
	public int upgraderCollected = 0;		    		// Upgraders in inventory
	public int flowerValueLevel = 1;		    		// Upgrade: value of a single flower when collected
	public int kittenGenerateLevel = 1;		    		// Upgrade: amount of flowers generated per 5 seconds by kitten
	public int kittenStorageLevel = 1;				    // Upgrade: maximum storage of kittens
	public EntityType currentItem = EntityType.FLOWER1; // Currently selected item
	public bool vaseHasSpawned;						    // Check if vase has spawned, for Spawner
	public bool upgraderHasSpawned;		    			// Check if upgrader has spawned, for Spawner
	public bool upgraderIsActive;		    			// Check if interacting with upgrader

	public GameObject infoText;						    // InfoText prefab for displaying text
	
	private int flowersRequiredForDirt = 5;			    // Flowers required to perform a Wish
	private int flowersRequiredForKitten = 20;		    // Flowers required to buy a Kitten

    [SerializeField]
	private GameObject playerRef;
    [SerializeField]
	private SoundManager soundManager;

	public static GameData Instance {get; private set;}


	void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
	}

    
	public void PayDirtCost()
	{
		flowersCollected -= flowersRequiredForDirt;

		// Wish cost goes up by an amount defined by algorithm set here
		// Cost increases 1.5 times, rounded down.
		flowersRequiredForDirt += flowersRequiredForDirt/2;
	}


	public void PayKittenCost()
	{
		flowersCollected -= flowersRequiredForKitten;
		
		// Kitten cost goes up by an amount defined by algorithm set here
		// Cost increases 1.2 times, rounded down.
		flowersRequiredForKitten += flowersRequiredForKitten/5;
	}


	public void AddFlowers(int count)
	{
		flowersCollected += count;
		lifetimeFlowersCollected += count;

		// Instantiate an InfoText GameObject, indicating flower gain.
		// Display above player position.
		//Vector3 playerPos = playerRef.transform.position;
		//GameObject iText = (GameObject)Instantiate(infoText, new Vector3(playerPos.x, playerPos.y + 2, -5), Quaternion.identity);
		//iText.GetComponent<TextMeshDisplay>().SetText("+ " + count + " Flower");
		
		// Play a sound when gaining flowers.
		soundManager.PlayRandomNote();
	}


	public void RemoveFlowers(int count)
	{
		flowersCollected -= count;
	}


	public int GetDirtCost()
	{
		return flowersRequiredForDirt; 
	}


	public int GetKittenCost()
	{
		return flowersRequiredForKitten;
	}


	/*************************Costs*************************/

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

    void OnItemSelected(EntityType itemType)
    {
        currentItem = itemType;
    }
}
