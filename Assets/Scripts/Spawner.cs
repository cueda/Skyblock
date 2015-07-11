using UnityEngine;
using System.Collections;

// Handles all spawning and despawning in the world.
// Responsible for all interaction with the GameGrid.
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform platformsParentTransform;
    [SerializeField]
    private Transform entitiesParentTransform;

    [Space(10)]

    [SerializeField]
    private GameObject spawnDirt;
    [SerializeField]
    private GameObject spawnStarterGrass;
    [SerializeField]
    private GameObject spawnFlower;
    [SerializeField]
    private GameObject spawnKitten;
    [SerializeField]
    private GameObject spawnVase;
    [SerializeField]
    private GameObject spawnUpgrader;

    [Space(10)]

    [SerializeField]
    private GameObject spawnBalloon;
    [SerializeField]
    private GameObject spawnGiftDirt;
    [SerializeField]
    private GameObject spawnGiftSpecial;

    [Space(20)]
	
    [SerializeField]
	private Vector2 flowerDrawOffset = new Vector2(0, -.6F);		// Location to draw flower sprite relative to game coordinates
    [SerializeField]
    private Vector2 vaseDrawOffset = new Vector2(0, -1);			// Location to draw vase sprite relative to game coordinates
    [SerializeField]
    private Vector2 kittenDrawOffset = new Vector2(0, -.8F);		// Location to draw kitten sprite relative to game coordinates
    [SerializeField]
    private Vector2 upgraderDrawOffset = new Vector2(0, -.5F);	    // Location to draw ugprader sprite relative to game coordinates

    [SerializeField]
	public Vector2 giftSpawnLocation = new Vector2(0, 50);	    // Location to spawn gifts. Beyond the edge of the map, preferably

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
            GameGridCoords newCoords = new GameGridCoords(init[index]);
            newGrassEntity.SetGridPosition(newCoords);
            GameGrid.Instance.AddObject(newGrassEntity, newCoords);
            newGrass.transform.parent = platformsParentTransform;
        }
	}


    //void Update()
    //{
    //    if (GameData.Instance.LifetimeFlowersCollected >= 100 && !GameData.Instance.vaseHasSpawned)
    //    {
    //        Debug.Log("Spawn a vase at 100.");
    //        //SpawnGiftSpecial("vase");
    //        GameData.Instance.vaseHasSpawned = true;
    //    }

    //    if (GameData.Instance.LifetimeFlowersCollected >= 250 && !GameData.Instance.upgraderHasSpawned)
    //    {
    //        Debug.Log("Spawn an upgrader at 250.");
    //        //SpawnGiftSpecial("upgrader");
    //        GameData.Instance.upgraderHasSpawned = true;
    //    }
    //}


	public void SpawnFlower(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + flowerDrawOffset;
		GameObject newFlower = (GameObject)Instantiate (spawnFlower, worldSpaceSpawn, Quaternion.identity);
        FlowerEntity newFlowerEntity = newFlower.GetComponent<FlowerEntity>();
        newFlowerEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newFlowerEntity, coords);
		newFlower.transform.parent = entitiesParentTransform;
	}


    public void SpawnDirtBlock(GameGridCoords coords)
	{
		GameObject newDirt = (GameObject)Instantiate(spawnDirt, coords.ToWorldSpace(), Quaternion.identity);
        DirtEntity newDirtEntity = newDirt.GetComponent<DirtEntity>();
        newDirtEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject(newDirtEntity, coords);
		newDirt.transform.parent = platformsParentTransform;
	}


    public void SpawnVase(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + vaseDrawOffset;
		GameObject newVase = (GameObject)Instantiate (spawnVase, worldSpaceSpawn, Quaternion.identity);
        VaseEntity newVaseEntity = newVase.GetComponent<VaseEntity>();
        newVaseEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newVaseEntity, coords);
        newVase.transform.parent = entitiesParentTransform;
	}


    public void SpawnKitten(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + kittenDrawOffset;
		GameObject newKitten = (GameObject)Instantiate (spawnKitten, worldSpaceSpawn, Quaternion.identity);
        KittenEntity newKittenEntity = newKitten.GetComponent<KittenEntity>();
        newKittenEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newKittenEntity, coords);
        newKitten.transform.parent = entitiesParentTransform;
	}


    public void SpawnUpgrader(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + upgraderDrawOffset;
        GameObject newUpgrader = (GameObject)Instantiate(spawnUpgrader, worldSpaceSpawn, Quaternion.identity);
        UpgraderEntity newUpgraderEntity = newUpgrader.GetComponent<UpgraderEntity>();
        newUpgraderEntity.SetGridPosition(coords);
		GameGrid.Instance.AddObject (newUpgraderEntity, coords);
        newUpgrader.transform.parent = entitiesParentTransform;
	}


    public void SpawnBalloon(BalloonEntity.RequestType request)
    {
        GameObject newBalloon = (GameObject)Instantiate (spawnBalloon, ObjectReferences.player.position, Quaternion.identity);
        BalloonEntity balloonEntity = newBalloon.GetComponent<BalloonEntity>();
        balloonEntity.SetRequest(request);
        newBalloon.transform.parent = entitiesParentTransform;
    }


    public void SpawnGiftWithContents(GiftEntity.Contents contents)
    {
        GameObject gift = (GameObject)Instantiate(spawnGiftDirt, giftSpawnLocation, Quaternion.identity);
        GiftEntity giftEntity = gift.GetComponent<GiftEntity>();
        giftEntity.contents = contents;
        gift.transform.parent = entitiesParentTransform;
    }


    /*

    private void SpawnGiftSpecial(string request)
    {
        GameObject gift = (GameObject)Instantiate(spawnGiftSpecial, defGiftSpawnLocation, Quaternion.identity);
        GiftMove giftState = gift.GetComponent<GiftMove>();
        giftState.request = request;
    }
     * */


    public void RemoveObject(GridEntity entity)
    {
        // Remove coordinates from GameGrid
        GameGrid.Instance.RemoveAtCoordinates(entity.gridPosition);

        // Destroy the object at the end
        Destroy(entity.gameObject);
    }
}
