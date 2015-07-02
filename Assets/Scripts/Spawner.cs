using UnityEngine;
using System.Collections;

// Handles all spawning and despawning in the world.
// Responsible for all interaction with the GameGrid.
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject platformsParent;
    [SerializeField]
    private GameObject entitiesParent;

    [Space(10)]

    [SerializeField]
    private GameObject spawnDirt;
    [SerializeField]
    private GameObject spawnStarterGrass;
    [SerializeField]
    private GameObject spawnFlower;
    [SerializeField]
    private GameObject spawnPaperPlane;
    [SerializeField]
    private GameObject spawnVase;
    [SerializeField]
    private GameObject spawnUpgrader;
    [SerializeField]
    private GameObject spawnKitten;

    [Space(10)]

    [SerializeField]
    private GameObject spawnGift;
    [SerializeField]
    private GameObject spawnGiftSpecial;

    [Space(20)]
	
	public Vector2 flowerDrawOffset = new Vector2(0, -.6F);		// Location to draw flower sprite relative to game coordinates
	public Vector2 vaseDrawOffset = new Vector2(0, -1);			// Location to draw vase sprite relative to game coordinates
	public Vector2 kittenDrawOffset = new Vector2(0, -.8F);		// Location to draw kitten sprite relative to game coordinates
	public Vector2 upgraderDrawOffset = new Vector2(0, -.5F);	// Location to draw ugprader sprite relative to game coordinates

	public Vector2 defGiftSpawnLocation = new Vector2(20, 8);	// Location to spawn gifts. Beyond the edge of the map, preferably

    public static Spawner Instance { get; private set; }


    void Awake()
    {
        // Singleton initialization.
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }


	void Start()
	{
		// Add initial blocks, hardcoded, to the GameGrid
		Vector2[] init = new Vector2[]{
			new Vector2(-4,0),
			new Vector2(-2,0),
			new Vector2(0,0),
			new Vector2(2,0),
			new Vector2(4,0)};

        for(int index = 0; index < init.Length; index++)
        {
            GameObject newGrass = (GameObject)Instantiate(spawnStarterGrass, init[index], Quaternion.identity);
            DirtEntity newGrassEntity = newGrass.GetComponent<DirtEntity>();
            GameGrid.Instance.AddObject(newGrassEntity, new GameGridCoords(init[index]));
            newGrass.transform.parent = platformsParent.transform;
        }
	}


	void Update()
	{
		if (GameData.Instance.lifetimeFlowersCollected >= 100 && !GameData.Instance.vaseHasSpawned)
		{
            Debug.Log("Spawn a vase at 100.");
			//SpawnGiftSpecial("vase");
			GameData.Instance.vaseHasSpawned = true;
		}

		if (GameData.Instance.lifetimeFlowersCollected >= 250 && !GameData.Instance.upgraderHasSpawned)
        {
            Debug.Log("Spawn an upgrader at 250.");
			//SpawnGiftSpecial("upgrader");
			GameData.Instance.upgraderHasSpawned = true;
		}
	}


	public void SpawnFlower(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + flowerDrawOffset;
		GameObject newFlower = (GameObject)Instantiate (spawnFlower, worldSpaceSpawn, Quaternion.identity);
        FlowerEntity newFlowerEntity = newFlower.GetComponent<FlowerEntity>();
        newFlowerEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newFlowerEntity, coords);
		newFlower.transform.parent = entitiesParent.transform;
	}


    public void SpawnDirtBlock(GameGridCoords coords)
	{
		GameObject newDirt = (GameObject)Instantiate(spawnDirt, coords.ToWorldSpace(), Quaternion.identity);
        DirtEntity newDirtEntity = newDirt.GetComponent<DirtEntity>();
        newDirtEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject(newDirtEntity, coords);
		newDirt.transform.parent = platformsParent.transform;
	}


    public void SpawnVase(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + vaseDrawOffset;
		GameObject newVase = (GameObject)Instantiate (spawnVase, worldSpaceSpawn, Quaternion.identity);
        VaseEntity newVaseEntity = newVase.GetComponent<VaseEntity>();
        newVaseEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newVaseEntity, coords);
        newVase.transform.parent = entitiesParent.transform;
	}


    public void SpawnKitten(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + kittenDrawOffset;
		GameObject newKitten = (GameObject)Instantiate (spawnKitten, worldSpaceSpawn, Quaternion.identity);
        KittenEntity newKittenEntity = GetComponent<KittenEntity>();
        newKittenEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newKittenEntity, coords);
        newKitten.transform.parent = entitiesParent.transform;
	}


    public void SpawnUpgrader(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + upgraderDrawOffset;
        GameObject newUpgrader = (GameObject)Instantiate(spawnUpgrader, worldSpaceSpawn, Quaternion.identity);
        UpgraderEntity newUpgraderEntity = GetComponent<UpgraderEntity>();
        newUpgraderEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newUpgraderEntity, coords);
        newUpgrader.transform.parent = entitiesParent.transform;
	}
	

	/*
	public void SpawnPaperPlane(string request)
	{
		GameObject plane = (GameObject)Instantiate (spawnPaperPlane, player.transform.position, Quaternion.identity);
		PlaneState state = plane.GetComponent<PlaneState>();
		state.request = request;
	}


	public void SpawnGift(Vector3 newPosition, string request)
	{
		GameObject gift = (GameObject)Instantiate(spawnGift, newPosition, Quaternion.identity);
		GiftMove giftState = gift.GetComponent<GiftMove>();
		giftState.centerHeight = newPosition.y;
		giftState.request = request;
	}


	private void SpawnGiftSpecial(string request)
	{
		GameObject gift = (GameObject)Instantiate(spawnGiftSpecial, defGiftSpawnLocation, Quaternion.identity);
		GiftMove giftState = gift.GetComponent<GiftMove>();
		giftState.request = request;
	}
     * */


    public void RemoveObject(GameEntity entity)
    {
        // Remove coordinates from GameGrid
        GameGrid.Instance.RemoveAtCoordinates(entity.gridPosition);

        // Destroy the object at the end
        Destroy(entity.gameObject);
    }
}
