using UnityEngine;
using System.Collections;

public class FlowerEntity : GridEntity 
{	
    [SerializeField]
	private Sprite[] sprites;
    [SerializeField]
	public int minGrowthTime = 3;
    [SerializeField]
	public int maxGrowthTime = 7;


	public int growthLevel;

	private float growthTime;
    private float growTick;
    private SpriteRenderer spRenderer;


	void Start () 
	{
		spRenderer = GetComponent<SpriteRenderer>();

		growthTime = Random.Range(minGrowthTime, maxGrowthTime);
		growthLevel = 0;
		growTick = 0F;
	}


	void Update () 
	{
		growTick += Time.deltaTime;

		if(growTick >= growthTime && growthLevel < sprites.Length-1)
		{
			growthLevel++;
			spRenderer.sprite = sprites[growthLevel];
			growTick -= growthTime;
			growthTime = Random.Range(minGrowthTime, maxGrowthTime);
		}
	}

    /// <summary>
    /// Pick flower and remove flower object from game and game grid.
    /// </summary>
    public override void Interact()
    {
        if (growthLevel == 2)
        {
            GameData.Instance.AddFlowers(GameData.Instance.flowerValueLevel);
            ObjectReferences.spawner.RemoveObject(this);
        }
    }
}
