using UnityEngine;
using System;
using System.Collections;

public class KittenEntity : GridEntity 
{
    private float flowerGenerationRate = 5;
    private int maxFlowersStorable = 5;
	
    private ParticleSystem flowerParticles;
    private int flowersStored;


    void Awake () 
    {
        flowerParticles = GetComponentInChildren<ParticleSystem>();
        EventManager.Values.OnKittenGenerateLevelChanged += OnKittenGenerateLevelChanged;
        EventManager.Values.OnKittenStorageLevelChanged += UpdateKittenStorageLevel;
    }


    void OnEnable()
    {
        flowersStored = 0;
        UpdateKittenStorageLevel(0, 0);
        StartCoroutine(GenerateFlowers());
    }


    void OnDisable()
    {
        StopAllCoroutines();
    }


    private IEnumerator GenerateFlowers()
    {
        while(true)
        {
            for (float timer = 0f; timer < flowerGenerationRate; timer += Time.deltaTime)
            {
                yield return null;
            }
            flowersStored += GameData.Instance.kittenGenerateLevel;

            if (flowersStored >= maxFlowersStorable)
            {
                flowersStored = maxFlowersStorable;
                // Effect to indicate kitten has maximum flowers stored
                flowerParticles.Play();
            }
        }        
    }


    private void UpdateKittenStorageLevel(int unused1, int unused2)
    {
        maxFlowersStorable = 5 + (3 * (GameData.Instance.KittenStorageLevel - 1));
    }


    private void OnKittenGenerateLevelChanged(int unused1, int unused2)
    {
        flowerGenerationRate = 5 - GameData.Instance.kittenGenerateLevel;
        if(flowerGenerationRate <= 0)
        {
            Debug.LogError("It's about time to change this algorithm. And its location...");
        }
    }


    public override void Interact()
    {
        GameData.Instance.AddFlowers(flowersStored);
        flowersStored = 0;
    }


    // Returns this object's GridEntityType.
    public override GridEntityType GetGridEntityType()
    {
        return GridEntityType.KITTEN;
    }


    // Generates extra save data for KittenEntity.
    public override int[] GenerateExtraSaveData()
    {
        return new int[] { flowersStored };
    }


    // Reads in save data from FileSerializer's GameData instance.
    public override void LoadExtraSaveData(int[] extraData)
    {
        flowersStored = extraData[0];
    }
}
