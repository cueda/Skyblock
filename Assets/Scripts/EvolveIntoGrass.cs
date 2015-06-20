using UnityEngine;
using System.Collections;

public class EvolveIntoGrass : MonoBehaviour {
    
	public GameObject grassBlock;
	public float timeChangeMin = 5;
	public float timeChangeMax = 15;

	float timeAlive;
	float timeChange;

	// Use this for initialization
	void Start () 
	{
		timeAlive = 0;
		timeChange = Random.Range(timeChangeMin, timeChangeMax);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeAlive += Time.deltaTime;
		if(timeAlive > timeChange)
		{
			// Destroy object instance, and remove current object from GameGrid
			Destroy(this.gameObject);
            GameGridCoords currentGridPosition = new GameGridCoords(transform.position);
			GameGrid.Instance.RemoveAtCoordinates(currentGridPosition);

			GameObject newGrass = (GameObject)Instantiate(grassBlock, transform.position, Quaternion.identity);
            GameGrid.Instance.AddObject(newGrass, currentGridPosition);
			newGrass.transform.parent = GameObject.Find("Platforms Parent").transform;
		}
	}
}
