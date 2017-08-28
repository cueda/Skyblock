using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class MusicPitchChanger : MonoBehaviour
{
    [SerializeField]
    private int[] flowerThresholds;             // Flower count required for a certain pitch. Must be in ascending order!
    [SerializeField]
    private float[] pitchAdjustments;           // Music speed multiplier associated with flowerThreshold indices

    private AudioSource musicSource;


	void Awake ()
    {
        if(flowerThresholds.Length != pitchAdjustments.Length)
        {
            Debug.LogError("Error in MusicPitchChanger - flowerThreshold array not same size as pitchAdjustments array");
        }

        musicSource = GetComponent<AudioSource>();                
	}
	
    	
	void OnEnable()
    {
        EventManager.Values.OnLifetimeFlowersCollectedChanged += OnLifetimeFlowersCollectedChanged;

        // Initialize music pitch
        StartCoroutine(WaitOneFrameSetMusicPitch());
    }



    private IEnumerator WaitOneFrameSetMusicPitch()
    {
        yield return null;
        SetMusicPitch(GameData.Instance.LifetimeFlowersCollected);
    }


    /// <summary>
    /// Finds current flower range, then sets music pitch associated with range.
    /// </summary>
    private void SetMusicPitch(int lifetimeFlowers)
    {
        // Starting with largest threshold, if current lifetime flower value is lower,
        // check next index to see if it is lower as well.
        // Find current flower threshold and apply its pitch.
        int index = flowerThresholds.Length - 1;
        while (lifetimeFlowers < flowerThresholds[index])
        {
            index--;

            if (index < 0)
            {
                break;
            }
        }
        musicSource.pitch = pitchAdjustments[index];
    }


    void OnLifetimeFlowersCollectedChanged(int oldVal, int newVal)
    {
        SetMusicPitch(newVal);
    }
}
