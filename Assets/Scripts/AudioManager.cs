using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource[] AudioSources = new AudioSource[4];
	public SceneSetup setup;
	public float closeDistance = 6;

	bool mPlayMusic;
	int mClosePlayers;

	public void PlayMusic ()
	{
		for (int i = 0; i < AudioSources.Length; i++)
		{
			AudioSources[i].volume = 0;
			AudioSources[i].Play();
		}
		AudioSources[0].volume = 1;

		mPlayMusic = true;
	}

	// Update is called once per frame
	void Update ()
	{
	
		if (!mPlayMusic) return;
		CheckPlayerDistances();
		// loop through the "close" audio sources
		for(int i= 1; i< AudioSources.Length; i++) {			
			if(mClosePlayers > i-1) {
				// we have enough close players to fade in the current sound
				float vol = AudioSources[i].volume + (Time.deltaTime /3);
				AudioSources[i].volume = vol > 1? 1 : vol;
			} else {
				float vol = AudioSources[i].volume - (Time.deltaTime /3);
				AudioSources[i].volume = vol >= 0? vol : 0;
			}
			Debug.Log("vol["+i+"]: "+AudioSources[i].volume);
		}
		
	}

	void CheckPlayerDistances()
	{
		
		mClosePlayers = 0;
		GameObject[] players = setup.players;
		for(int i=0; i<players.Length;i++) {
			var current = players[i];
			for (int j=i+1; j< players.Length; j++) {
				var other = players[j];
				var currentPosition = current.transform.position;
				var dt = other.GetComponent<DamageTaker>() as DamageTaker;
				if(!dt.alive || !other.GetComponent<Killer>().isActive) {
					continue;
				}
				var otherPosition = other.transform.position;
				var distance = Vector3.Distance(currentPosition,otherPosition);
				if(distance <= closeDistance) {
					mClosePlayers++;
				}
			}
		}
	}
}
