using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class KittenHouseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject kittenHouseUIPanel;
    [SerializeField]
    private GameObject firstButton;
    [SerializeField]
    private Text kittenCountText;
    [SerializeField]
    private Text flowersStoredText;

    public static KittenHouseEntity currentHouseEntity;

    private ParticleSystem flowerParticles;


    void Awake()
    {
        flowerParticles = GetComponentInChildren<ParticleSystem>();
        EventManager.Game.OnStateSet += OnStateSet;
    }


    // Assume currentHouseEntity has already been set before OnStateSet is called to transition to GameState.KITTENHOUSE
    void OnStateSet(GameState.State state)
    {
        if (state == GameState.State.KITTENHOUSE)
        {
            kittenHouseUIPanel.SetActive(true);
            if (!TouchScreenInputHandler.IS_TOUCH_SCREEN)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }

            // Update text in menu buttons
            OnKittensStoredInCurrentEntityChanged();
            OnFlowersStoredInCurrentEntityChanged();
            currentHouseEntity.OnKittensStoredChanged += OnKittensStoredInCurrentEntityChanged;
            currentHouseEntity.OnFlowersStoredChanged += OnFlowersStoredInCurrentEntityChanged;
        }
        else
        {
            kittenHouseUIPanel.SetActive(false);

            if(currentHouseEntity != null)
            {
                currentHouseEntity.OnKittensStoredChanged -= OnKittensStoredInCurrentEntityChanged;
                currentHouseEntity.OnFlowersStoredChanged -= OnFlowersStoredInCurrentEntityChanged;
            }
        }
    }


    void OnKittensStoredInCurrentEntityChanged()
    {
        kittenCountText.text = "Add Kitten (currently " + currentHouseEntity.kittensStored.ToString() + ")";
    }


    void OnFlowersStoredInCurrentEntityChanged()
    {
        flowersStoredText.text = "Harvest " + currentHouseEntity.flowersStored.ToString() + " flowers";
    }


    // For use with KittenHouseMenu button
	public void OnAddKitten()
    {
        if(GameData.Instance.KittensCollected > 0)
        {
            currentHouseEntity.kittensStored++;
            GameData.Instance.KittensCollected--;
            currentHouseEntity.OnKittensStoredChanged();
        }
    }


    // For use with KittenHouseMenu button
    public void OnCollectFlowers()
    {
        GameData.Instance.AddFlowers(currentHouseEntity.flowersStored);
        currentHouseEntity.flowersStored = 0;
        currentHouseEntity.OnFlowersStoredChanged();
    }


    // For use with KittenHouseMenu button
    public void CloseKittenHouseMenu()
    {
        currentHouseEntity = null;
        EventManager.Game.OnStateSet(GameState.State.MOVE);
    }
}
