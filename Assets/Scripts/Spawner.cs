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

	public GameObject spawnFlower;
	public GameObject spawnPaperPlane;
	public GameObject spawnGift;
	public GameObject spawnGiftSpecial;
	public GameObject spawnDirt;
	public GameObject spawnGrass;
	public GameObject spawnVase;
	public GameObject spawnUpgrader;
	public GameObject spawnKitten;
	
	public Vector2 flowerDrawOffset = new Vector2(0, -.6F);		// Location to draw flower sprite relative to game coordinates
	public Vector2 vaseDrawOffset = new Vector2(0, -1);			// Location to draw vase sprite relative to game coordinates
	public Vector2 kittenDrawOffset = new Vector2(0, -.8F);		// Location to draw kitten sprite relative to game coordinates
	public Vector2 upgraderDrawOffset = new Vector2(0, -.5F);	// Location to draw ugprader sprite relative to game coordinates

	public Vector2 defGiftSpawnLocation = new Vector2(20, 8);	// Location to spawn gifts. Beyond the edge of the map, preferably

	void Start()
	{
		// Add initial blocks, hardcoded, to the GameGrid
		// Assign parent object as "platforms" GameObject
		Vector2[] init = new Vector2[]{
			new Vector2(-4,0),
			new Vector2(-2,0),
			new Vector2(0,0),
			new Vector2(2,0),
			new Vector2(4,0)};
		GameObject grass0 = (GameObject)Instantiate(spawnGrass, init[0], Quaternion.identity);
		GameObject grass1 = (GameObject)Instantiate(spawnGrass, init[1], Quaternion.identity);
		GameObject grass2 = (GameObject)Instantiate(spawnGrass, init[2], Quaternion.identity);
		GameObject grass3 = (GameObject)Instantiate(spawnGrass, init[3], Quaternion.identity);
		GameObject grass4 = (GameObject)Instantiate(spawnGrass, init[4], Quaternion.identity);
		grass0.transform.parent = platformsParent.transform;
		grass1.transform.parent = platformsParent.transform;
        grass2.transform.parent = platformsParent.transform;
		grass3.transform.parent = platformsParent.transform;
        grass4.transform.parent = platformsParent.transform;
		GameGrid.Instance.AddObject(grass0, new GameGridCoords(init[0]));
        GameGrid.Instance.AddObject(grass1, new GameGridCoords(init[1]));
        GameGrid.Instance.AddObject(grass2, new GameGridCoords(init[2]));
        GameGrid.Instance.AddObject(grass3, new GameGridCoords(init[3]));
        GameGrid.Instance.AddObject(grass4, new GameGridCoords(init[4]));
	}


	void Update()
	{
		if (GameData.Instance.lifetimeFlowersCollected >= 100 && !GameData.Instance.vaseHasSpawned)
		{
			//SpawnGiftSpecial("vase");
			GameData.Instance.vaseHasSpawned = true;
		}

		if (GameData.Instance.lifetimeFlowersCollected >= 250 && !GameData.Instance.upgraderHasSpawned)
		{
			//SpawnGiftSpecial("upgrader");
			GameData.Instance.upgraderHasSpawned = true;
		}
	}


	public void SpawnFlower(GameGridCoords coords)
	{
		Vector2 spawnPoint = coords.ToWorldSpace() + flowerDrawOffset;
		GameObject newFlower = (GameObject)Instantiate (spawnFlower, spawnPoint, Quaternion.identity);
		GameGrid.Instance.AddObject (newFlower, coords);
		newFlower.transform.parent = entitiesParent.transform;
	}


    public void SpawnDirtBlock(GameGridCoords coords)
	{
		GameObject newDirt = (GameObject)Instantiate(spawnDirt, coords.ToWorldSpace(), Quaternion.identity);
		GameGrid.Instance.AddObject(newDirt, coords);
		newDirt.transform.parent = platformsParent.transform;
	}


    public void SpawnVase(GameGridCoords coords)
	{
		Vector2 spawnPoint = coords.ToWorldSpace() + vaseDrawOffset;
		GameObject newVase = (GameObject)Instantiate (spawnVase, spawnPoint, Quaternion.identity);
		GameGrid.Instance.AddObject (newVase, coords);
        newVase.transform.parent = entitiesParent.transform;
	}


    public void SpawnKitten(GameGridCoords coords)
	{
		Vector2 spawnPoint = coords.ToWorldSpace() + kittenDrawOffset;
		GameObject newKitten = (GameObject)Instantiate (spawnKitten, spawnPoint, Quaternion.identity);
		GameGrid.Instance.AddObject (newKitten, coords);
        newKitten.transform.parent = entitiesParent.transform;
	}


    public void SpawnUpgrader(GameGridCoords coords)
	{
		Vector2 spawnPoint = coords.ToWorldSpace() + upgraderDrawOffset;
		GameGrid.Instance.AddObject((GameObject)Instantiate(spawnUpgrader, spawnPoint, Quaternion.identity), coords);
	}


	public void RemoveObject(GameObject obj)
	{
		// Find position of object, and subtract its offset to find coordinates to remove
		Vector2 offset = Vector2.zero;

		switch(obj.tag)
		{
		case "flower":
			offset = -1 * flowerDrawOffset;
			break;
		default:
			break;
		}

		// Remove coordinates from GameGrid
		GameGrid.Instance.RemoveAtCoordinates(new GameGridCoords((Vector2)obj.transform.position + offset));

		// Destroy the object at the end
		Destroy (obj);
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
}
