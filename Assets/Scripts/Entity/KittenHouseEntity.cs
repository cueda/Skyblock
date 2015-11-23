using UnityEngine;
using System;
using System.Collections;

public class KittenHouseEntity : GridEntity
{
    private static readonly GameGridCoords[] CHILDREN_SPACES = { new GameGridCoords(-1, 1), new GameGridCoords(0, 1), new GameGridCoords(1, 1),
                                                                 new GameGridCoords(-1, 0), new GameGridCoords(1, 0) };

    [HideInInspector]
    public int kittensStored;
    [HideInInspector]
    public int flowersStored;

    private ParticleSystem flowerParticles;
    private float flowerGenerationRate = 5;
    private int maxFlowersStorable = 100;       // temporary default value

    public Action OnKittensStoredChanged = delegate { };
    public Action OnFlowersStoredChanged = delegate { };


    void Awake()
    {
        flowerParticles = GetComponentInChildren<ParticleSystem>();
        EventManager.Values.OnKittenGenerateLevelChanged += OnKittenGenerateLevelChanged;        
    }


	void OnEnable()
    {
        kittensStored = 0;
        flowersStored = 0;

        StartCoroutine(GenerateFlowers());
    }


    void OnDisable()
    {
        StopAllCoroutines();
    }


    public void SetGridPosition(GameGridCoords coords)
    {
        gridPosition = new GameGridCoords(coords.x, coords.y);
        relativeChildPositions = CHILDREN_SPACES;
    }


    private IEnumerator GenerateFlowers()
    {
        while (true)
        {
            for (float timer = 0f; timer < flowerGenerationRate; timer += Time.deltaTime)
            {
                yield return null;
            }
            flowersStored += GameData.Instance.kittenGenerateLevel * kittensStored;

            if (flowersStored >= maxFlowersStorable)
            {
                flowersStored = maxFlowersStorable;
                // Effect to indicate kitten has maximum flowers stored
                flowerParticles.Play();
            }

            OnFlowersStoredChanged();
        }
    }


    private void OnKittenGenerateLevelChanged(int unused1, int unused2)
    {
        flowerGenerationRate = 5 - GameData.Instance.kittenGenerateLevel;
        if (flowerGenerationRate <= 0)
        {
            Debug.LogError("It's about time to change this algorithm. And its location...");
        }
    }


    public override void Interact()
    {
        KittenHouseMenu.currentHouseEntity = this;
        EventManager.Game.OnStateSet(GameState.State.KITTENHOUSE);
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.KITTENHOUSE;
    }


    // Generates extra save data for FlowerEntity.
    public override int[] GenerateExtraSaveData()
    {
        return new int[] { kittensStored, flowersStored };
    }


    // Reads in save data from FileSerializer's GameData instance.
    public override void LoadExtraSaveData(int[] extraData)
    {
        kittensStored = extraData[0];
        flowersStored = extraData[1];
    }
}
