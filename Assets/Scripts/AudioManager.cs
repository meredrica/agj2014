using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource[] AudioSources;
	public SceneSetup setup;
	public float closeDistance = 7;

	bool mPlayMusic;
	int mClosePlayers;

	public void PlayMusic ()
	{
		for (int i = 0; i < 4; i++)
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

		AudioSources[1].volume = mClosePlayers > 0 ? 1 : 0;
		AudioSources[2].volume = mClosePlayers > 1 ? 1 : 0;
		AudioSources[3].volume = mClosePlayers > 2 ? 1 : 0;
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
				var otherPosition = other.transform.position;
				var distance = Vector3.Distance(currentPosition,otherPosition);
				if(distance <= closeDistance) {
					mClosePlayers++;
				}
			}
		}
		Debug.Log("close players: "+ mClosePlayers);
	}
}
