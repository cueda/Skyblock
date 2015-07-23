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
        EventManager.Values.OnKittenGenerateLevelChanged += OnKittenGenerateLevelChanged;
        EventManager.Values.OnKittenStorageLevelChanged += OnKittenStorageLevelChanged;
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


    private void OnKittenStorageLevelChanged(int unused1, int unused2)
    {
        maxFlowersStorable = 5 + (2 * (GameData.Instance.KittenStorageLevel - 1));
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
}
