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
    [SerializeField]
    private GameObject spawnTree;
    [SerializeField]
    private GameObject spawnWoodPlatform;
    [SerializeField]
    private GameObject spawnWorkshop;
    [SerializeField]
    private GameObject spawnKittenHouse;

    [SerializeField]
    private GameObject spawnFiller;
    
    [Space(10)]

    [SerializeField]
    private GameObject spawnBalloon;
    [SerializeField]
    private GameObject spawnGiftDirt;

    [Space(20)]
	
    [SerializeField]
	private Vector2 flowerDrawOffset = new Vector2(0, -.6F);		// Location to draw flower sprite relative to game coordinates
    [SerializeField]
    private Vector2 vaseDrawOffset = new Vector2(0, -1);			// Location to draw vase sprite relative to game coordinates
    [SerializeField]
    private Vector2 kittenDrawOffset = new Vector2(0, -.8F);		// Location to draw kitten sprite relative to game coordinates
    [SerializeField]
    private Vector2 upgraderDrawOffset = new Vector2(0, -.5F);	    // Location to draw upgrader sprite relative to game coordinates
    [SerializeField]
    private Vector2 treeDrawOffset = new Vector2(0, -.6F);		    // Location to draw flower sprite relative to game coordinates
    [SerializeField]
    private Vector2 workshopDrawOffset = new Vector2(0, -.5F);	    // Location to draw workshop sprite relative to game coordinates
    [SerializeField]
    private Vector2 kittenHouseDrawOffset = new Vector2(0, -.5F);	    // Location to draw workshop sprite relative to game coordinates

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
        if(FileSerializer.fileData != null)
        {
            Debug.Log("Beginning loading game world from file data.");
            SpawnFromSaveData();
        }
        else
        {
            SpawnFromDefault();
        }

        EventManager.Values.OnLifetimeFlowersCollectedChanged += OnLifetimeFlowersCollectedChanged;
	}


    private void SpawnFromSaveData()
    {
        foreach (FileData.FileGridEntityData data in FileSerializer.fileData.gameEntities)
        {
            GridEntity newEntity = null;

            switch (data.entityType)
            {
                case GridEntityType.DIRT:
                    newEntity = SpawnDirtBlock(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.FLOWER:
                    newEntity = SpawnFlower(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.KITTEN:
                    newEntity = SpawnKitten(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.UPGRADER:
                    newEntity = SpawnUpgrader(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.VASE:
                    newEntity = SpawnVase(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.TREE:
                    newEntity = SpawnSapling(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.WOOD:
                    newEntity = SpawnWoodPlatform(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.WORKSHOP:
                    newEntity = SpawnWorkshop(new GameGridCoords(data.posX, data.posY));
                    break;
                case GridEntityType.KITTENHOUSE:
                    newEntity = SpawnKittenHouse(new GameGridCoords(data.posX, data.posY));
                    break;
                default:
                    Debug.LogError("GridEntityType not found in SpawnFromSaveData function.");
                    break;
            }

            if(newEntity != null)
            {
                newEntity.LoadExtraSaveData(data.extraData);
            }
            else
            {
                Debug.LogError("Entity reference not found in SpawnFromSaveData function.");
            }
        }
    }


    private void SpawnFromDefault()
    {
        Debug.Log("No file data found. Spawning default game objects from Spawner.Start().");
        // Add initial blocks, hardcoded, to the GameGrid
        Vector2[] init = new Vector2[]{
			new Vector2(-4,0),
			new Vector2(-2,0),
			new Vector2(0,0),
			new Vector2(2,0),
			new Vector2(4,0)};

        for (int index = 0; index < init.Length; index++)
        {
            GameObject newGrass = (GameObject)Instantiate(spawnStarterGrass, init[index], Quaternion.identity);
            DirtEntity newGrassEntity = newGrass.GetComponent<DirtEntity>();
            GameGridCoords newCoords = new GameGridCoords(init[index]);
            newGrassEntity.SetGridPosition(newCoords);
            GameGrid.Instance.AddEntity(newGrassEntity, newCoords);
            newGrass.transform.parent = platformsParentTransform;
        }
    }


    private void OnLifetimeFlowersCollectedChanged(int oldVal, int newVal)
    {
        if (GameData.Instance.LifetimeFlowersCollected >= 100 && !GameData.Instance.upgraderHasSpawned)
        {
            Debug.Log("Spawn an upgrader at 100 lifetime flowers.");
            SpawnGiftWithContents(GiftEntity.Contents.UPGRADER, true);
            GameData.Instance.upgraderHasSpawned = true;
        }

        if (GameData.Instance.LifetimeFlowersCollected >= 1000 && !GameData.Instance.workshopHasSpawned)
        {
            Debug.Log("Spawn a workshop at 1000 lifetime flowers.");
            SpawnGiftWithContents(GiftEntity.Contents.WORKSHOP, true);
            GameData.Instance.workshopHasSpawned = true;
        }
    }

    #region Entity spawning

    /// <summary>
    /// Each of the following functions creates a new Gameobject of its Inspector-set type.
    /// Then, it will give a reference to the entity GameComponent attached for additional initialization.
    /// </summary>

    public GridEntity SpawnFlower(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + flowerDrawOffset;
		GameObject newFlower = (GameObject)Instantiate (spawnFlower, worldSpaceSpawn, Quaternion.identity);
        FlowerEntity newFlowerEntity = newFlower.GetComponent<FlowerEntity>();
        newFlowerEntity.SetGridPosition(coords);
		GameGrid.Instance.AddEntity (newFlowerEntity, coords);
		newFlower.transform.parent = entitiesParentTransform;
        return newFlowerEntity;
	}


    public GridEntity SpawnDirtBlock(GameGridCoords coords)
	{
		GameObject newDirt = (GameObject)Instantiate(spawnDirt, coords.ToWorldSpace(), Quaternion.identity);
        DirtEntity newDirtEntity = newDirt.GetComponent<DirtEntity>();
        newDirtEntity.SetGridPosition(coords);
		GameGrid.Instance.AddEntity(newDirtEntity, coords);
		newDirt.transform.parent = platformsParentTransform;
        return newDirtEntity;
	}


    public GridEntity SpawnVase(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + vaseDrawOffset;
		GameObject newVase = (GameObject)Instantiate (spawnVase, worldSpaceSpawn, Quaternion.identity);
        VaseEntity newVaseEntity = newVase.GetComponent<VaseEntity>();
        newVaseEntity.SetGridPosition(coords);
		GameGrid.Instance.AddEntity (newVaseEntity, coords);
        newVase.transform.parent = entitiesParentTransform;
        return newVaseEntity;
	}


    public GridEntity SpawnKitten(GameGridCoords coords)
	{
		Vector2 worldSpaceSpawn = coords.ToWorldSpace() + kittenDrawOffset;
		GameObject newKitten = (GameObject)Instantiate (spawnKitten, worldSpaceSpawn, Quaternion.identity);
        KittenEntity newKittenEntity = newKitten.GetComponent<KittenEntity>();
        newKittenEntity.SetGridPosition(coords);
		GameGrid.Instance.AddEntity (newKittenEntity, coords);
        newKitten.transform.parent = entitiesParentTransform;
        return newKittenEntity;
	}


    public GridEntity SpawnUpgrader(GameGridCoords coords)
    {
        Vector2 worldSpaceSpawn = coords.ToWorldSpace() + upgraderDrawOffset;
        GameObject newUpgrader = (GameObject)Instantiate(spawnUpgrader, worldSpaceSpawn, Quaternion.identity);
        UpgraderEntity newUpgraderEntity = newUpgrader.GetComponent<UpgraderEntity>();
        newUpgraderEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newUpgraderEntity, coords);
        newUpgrader.transform.parent = entitiesParentTransform;
        return newUpgraderEntity;
    }


    public GridEntity SpawnSapling(GameGridCoords coords)
    {
        Vector2 worldSpaceSpawn = coords.ToWorldSpace() + treeDrawOffset;
        GameObject newTree = (GameObject)Instantiate(spawnTree, worldSpaceSpawn, Quaternion.identity);
        TreeEntity newTreeEntity= newTree.GetComponent<TreeEntity>();
        newTreeEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newTreeEntity, coords);
        newTree.transform.parent = entitiesParentTransform;
        return newTreeEntity;
    }


    public WoodPlatformEntity SpawnWoodPlatform(GameGridCoords coords)
    {
        GameObject newWoodPlatform = (GameObject)Instantiate(spawnWoodPlatform, coords.ToWorldSpace(), Quaternion.identity);
        WoodPlatformEntity newWoodPlatformEntity = newWoodPlatform.GetComponent<WoodPlatformEntity>();
        newWoodPlatformEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newWoodPlatformEntity, coords);
        newWoodPlatform.transform.parent = platformsParentTransform;
        return newWoodPlatformEntity;
    }


    public WorkshopEntity SpawnWorkshop(GameGridCoords coords)
    {
        Vector2 worldSpaceSpawn = coords.ToWorldSpace() + workshopDrawOffset;
        GameObject newWorkshop = (GameObject)Instantiate(spawnWorkshop, worldSpaceSpawn, Quaternion.identity);
        WorkshopEntity newWorkshopEntity = newWorkshop.GetComponent<WorkshopEntity>();
        newWorkshopEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newWorkshopEntity, coords);
        newWorkshop.transform.parent = entitiesParentTransform;
        return newWorkshopEntity;
    }


    public KittenHouseEntity SpawnKittenHouse(GameGridCoords coords)
    {
        Vector2 worldSpaceSpawn = coords.ToWorldSpace() + kittenHouseDrawOffset;
        GameObject newKittenHouse = (GameObject)Instantiate(spawnKittenHouse, worldSpaceSpawn, Quaternion.identity);
        KittenHouseEntity newKittenHouseEntity = newKittenHouse.GetComponent<KittenHouseEntity>();
        newKittenHouseEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newKittenHouseEntity, coords);
        newKittenHouse.transform.parent = entitiesParentTransform;
        return newKittenHouseEntity;
    }


    public FillerEntity SpawnFiller(GameGridCoords coords)
    {
        GameObject newFiller = (GameObject)Instantiate(spawnFiller, coords.ToWorldSpace(), Quaternion.identity);
        FillerEntity newFillerEntity = newFiller.GetComponent<FillerEntity>();
        newFillerEntity.SetGridPosition(coords);
        GameGrid.Instance.AddEntity(newFillerEntity, coords);
        newFiller.transform.parent = entitiesParentTransform;
        return newFillerEntity;
    }


    #endregion

    #region Gift spawning

    public void SpawnBalloon(BalloonEntity.RequestType request)
    {
        GameObject newBalloon = (GameObject)Instantiate (spawnBalloon, ObjectReferences.player.position, Quaternion.identity);
        BalloonEntity balloonEntity = newBalloon.GetComponent<BalloonEntity>();
        balloonEntity.SetRequest(request);
        newBalloon.transform.parent = entitiesParentTransform;
    }


    public void SpawnGiftWithContents(GiftEntity.Contents contents, bool isSpecial)
    {
        GameObject gift = (GameObject)Instantiate(spawnGiftDirt, giftSpawnLocation, Quaternion.identity);
        GiftEntity giftEntity = gift.GetComponent<GiftEntity>();
        giftEntity.contents = contents;
        giftEntity.SetSpecialState(isSpecial);
        gift.transform.parent = entitiesParentTransform;
    }

    #endregion


    public void RemoveObject(GridEntity entity)
    {
        // Remove entity from GameGrid
        GameGrid.Instance.RemoveEntity(entity);

        // Destroy the object at the end
        Destroy(entity.gameObject);
    }
}
