using UnityEngine;
using System.Collections;

public class FlowerEntity : GridEntity 
{	
    [SerializeField]
	private Sprite[] sprites;
    [SerializeField]
	private int minGrowthTime = 3;
    [SerializeField]
	private int maxGrowthTime = 7;


	private int growthLevel;

	private float growthTime;
    private SpriteRenderer spRenderer;


	void Awake () 
	{
		spRenderer = GetComponent<SpriteRenderer>();
	}


    void OnEnable()
    {
        growthTime = Random.Range(minGrowthTime, maxGrowthTime);
        growthLevel = 0;

        StartCoroutine(BeginGrowing());
    }


    private IEnumerator BeginGrowing()
    {
        for(float timer = 0; ; timer += Time.deltaTime)
        {
            yield return null;
            if (timer >= growthTime)
            {
                // If growthLevel is not max, go up a level
                if (growthLevel < sprites.Length - 1)
                {
                    growthLevel++;
                    spRenderer.sprite = sprites[growthLevel];
                    timer -= growthTime;
                    growthTime = Random.Range(minGrowthTime, maxGrowthTime);

                    // If maximum level reached, break loop
                    if(growthLevel >= sprites.Length - 1)
                    {
                        break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Pick flower and remove flower object from game and game grid.
    /// </summary>
    public override void Interact()
    {
        if (growthLevel == 2)
        {
            GameData.Instance.AddFlowers(GameData.Instance.FlowerValueLevel);
            ObjectReferences.spawner.RemoveObject(this);
        }
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.FLOWER;
    }


    // Generates extra save data for FlowerEntity.
    public override int[] GenerateExtraSaveData()
    {
        return new int[] { growthLevel };
    }


    // Reads in save data from FileSerializer's GameData instance.
    public override void LoadExtraSaveData(int[] extraData)
    {
        growthLevel = extraData[0];
        spRenderer.sprite = sprites[growthLevel];
    }
}
