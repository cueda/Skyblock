using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public AudioClip phrase1, phrase2, phrase3;
	public AudioClip[] notes;

	private AudioSource audioPhrase1, audioPhrase2, audioPhrase3;
	private AudioSource[] audioNotes;
	

	void Awake () {
        //audioPhrase1 = AddAudioSource(phrase1, false, false, 1.0F);
        //audioPhrase2 = AddAudioSource(phrase2, false, false, 1.0F);
        //audioPhrase3 = AddAudioSource(phrase3, false, false, 1.0F);

		audioNotes = new AudioSource[notes.Length];
		for(int i=0; i<audioNotes.Length; i++) {
			audioNotes[i] = AddAudioSource(notes[i], false, false, 1.0F);
		}
	}


	// Create an AudioSource object from an AudioClip.
	AudioSource AddAudioSource(AudioClip clip, bool loop, bool playAwake, float vol) {
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}


	// Plays a note from audioNotes.
	public void PlayRandomNote() {
		int selection = Random.Range(0, audioNotes.Length);
		//Debug.Log("Selected " + selection + " out of " + (audioNotes.Length-1));
		audioNotes[selection].Play();
	}
}
