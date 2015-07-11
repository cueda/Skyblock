using UnityEngine;
using System;
using System.Collections;

public class KittenEntity : GridEntity 
{
    [SerializeField]
    private float flowerGenerationRate = 5;
    [SerializeField]
    private int maxFlowersStorable = 5;
	
    private ParticleSystem particleSystem;
    private int flowersStored;


    void Awake () 
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        EventManager.Upgrades.OnKittenGenerateLevelChanged += OnKittenGenerateLevelChanged;
        EventManager.Upgrades.OnKittenStorageLevelChanged += OnKittenStorageLevelChanged;
    }


    void OnEnable()
    {
        flowersStored = 0;
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
                particleSystem.Play();
            }
        }        
    }


    private void OnKittenStorageLevelChanged()
    {
        maxFlowersStorable = 5 * GameData.Instance.kittenStorageLevel;
    }


    private void OnKittenGenerateLevelChanged()
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
}
