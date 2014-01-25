using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource[] AudioSources;
	public GameObject[] Players;

	bool mPlayMusic;
	int mClosePlayers;

	void PlayMusic ()
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
	}
}
