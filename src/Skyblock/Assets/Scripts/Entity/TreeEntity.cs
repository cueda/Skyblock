using UnityEngine;
using System.Collections;

public class TreeEntity : GridEntity
{
    private readonly GameGridCoords[] CHILDREN_SPACES_LEVEL_ONE = { };
    private readonly GameGridCoords[] CHILDREN_SPACES_LEVEL_TWO = { new GameGridCoords(0, 1) };
    private readonly GameGridCoords[] CHILDREN_SPACES_LEVEL_THREE = { new GameGridCoords(0, 1), new GameGridCoords(0, 2) };

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private int minGrowthTime = 60;
    [SerializeField]
    private int maxGrowthTime = 90;
    [SerializeField]
    private int levelOneSeedOutput = 2;
    [SerializeField]
    private int levelOneWoodOutput = 1;
    [SerializeField]
    private int levelTwoSeedOutput = 2;
    [SerializeField]
    private int levelTwoWoodOutput = 5;

    private int growthLevel;

    private float growthTime;
    private SpriteRenderer spRenderer;


    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

	
	void OnEnable()
    {
        growthTime = Random.Range(minGrowthTime, maxGrowthTime);
        growthLevel = 0;

        StartCoroutine(BeginGrowing());
	}


    public void SetGridPosition(GameGridCoords coords)
    {
        gridPosition = new GameGridCoords(coords.x, coords.y);
        relativeChildPositions = CHILDREN_SPACES_LEVEL_ONE;
    }


    private IEnumerator BeginGrowing()
    {
        for (float timer = 0; ; timer += Time.deltaTime)
        {
            yield return null;
            // Once every growthTime...
            if (timer >= growthTime)
            {
                // Check if the space above is empty
                // Check two spaces above if currently growthLevel 1
                if (!GameGrid.Instance.IsCoordinatesOccupied(gridPosition + new GameGridCoords(0, (short)(growthLevel + 1))))
                {
                    // If growth level is less than 2 (in case of loaded data)
                    if(growthLevel < 2)
                    {

                        // If all conditions fulfilled, go up a level
                        GrowOneLevel();

                        // Reset timer and set new growth time
                        timer -= growthTime;
                        growthTime = Random.Range(minGrowthTime, maxGrowthTime);
                    }

                    // If maximum level reached, break loop
                    if (growthLevel >= sprites.Length - 1)
                    {
                        break;
                    }
                }
                else
                {
                    timer -= growthTime;
                    growthTime = Random.Range(minGrowthTime, maxGrowthTime);
                }
            }
        }
    }


    private void GrowOneLevel()
    {
        // Add level and update sprite
        growthLevel++;
        spRenderer.sprite = sprites[growthLevel];

        // Update child grid positions in three steps:
        // remove existing filler entities, updating relative child positions, and adding new filler entities
        GameGrid.Instance.RemoveFillerEntities(absoluteChildPositions);
        if (growthLevel == 1)
        {
            relativeChildPositions = CHILDREN_SPACES_LEVEL_TWO;
        }
        else if (growthLevel == 2)
        {
            relativeChildPositions = CHILDREN_SPACES_LEVEL_THREE;
        }
        GameGrid.Instance.AddFillerEntities(absoluteChildPositions);
    }


    /// <summary>
    /// Cut tree and remove tree object from game and game grid.
    /// </summary>
    public override void Interact()
    {
        if (growthLevel == 1)
        {
            GameData.Instance.SaplingsCollected += levelOneSeedOutput;
            GameData.Instance.AddWood(levelOneWoodOutput);
            ObjectReferences.spawner.RemoveObject(this);
        }
        else if (growthLevel == 2)
        {
            GameData.Instance.SaplingsCollected += levelTwoSeedOutput;
            GameData.Instance.AddWood(levelTwoWoodOutput);
            ObjectReferences.spawner.RemoveObject(this);
        }
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.TREE;
    }


    // Generates extra save data for TreeEntity.
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
