using UnityEngine;
using System.Collections;

public class DirtEntity : GridEntity
{

    [SerializeField]
    private Sprite grassSprite;
    [SerializeField]
    private bool isStarterGrass;
    [SerializeField]
	private float timeChangeMin = 5;
    [SerializeField]
	private float timeChangeMax = 15;

    [HideInInspector]
    public bool hasGrownGrass { get; private set; }

    private SpriteRenderer spRenderer;
	private float timeChange;


	void Start () 
	{
        spRenderer = GetComponent<SpriteRenderer>();

        if (isStarterGrass)
        {
            GrowGrass();
        }
        else
        {
            StartCoroutine(BeginGrassTimer());
        }
	}
	

    private IEnumerator BeginGrassTimer()
    {
        timeChange = Random.Range(timeChangeMin, timeChangeMax);
        for (float timer = 0; timer < timeChange; timer += Time.deltaTime )
        {
            yield return null;
        }
        GrowGrass();
    }


    private void GrowGrass()
    {
        spRenderer.sprite = grassSprite;
        hasGrownGrass = true;
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.DIRT;
    }


    // Generates extra save data for FlowerEntity.
    public override int[] GenerateExtraSaveData()
    {
        return new int[] { hasGrownGrass ? 1 : 0 };
    }


    // Reads in save data from FileSerializer's GameData instance.
    public override void LoadExtraSaveData(int[] extraData)
    {
        if(extraData[0] == 1)
        {
            isStarterGrass = true;
        }
        else
        {
            isStarterGrass = false;
        }
    }
}
