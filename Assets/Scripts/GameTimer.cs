using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {
	public float gameDuration = 8;
	public float gameStartDelay = 3;
	public float gameStartEnd = -1;
	
	int state;
	bool start;
	float timer;

	// Use this for initialization
	void Start () {
		state = 3;
		timer = gameStartDelay;
		start = true;
	}
	
	private void displayCountdown(string message) {
		Debug.Log (message);
	}
	
	private void gameStarted() {
		Debug.Log ("Game started!");
	}
	
	private void gameEnded() {
		Debug.Log ("Game ended!");
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if(timer < state && state > 0) {
			displayCountdown("" + state);
			state--;
		} else if(timer < state && state == 0 && start) {
			displayCountdown("GO!");
			gameStarted();
			state = -1;
		} else if(timer < gameStartEnd && start) {
			timer = gameDuration;
			state = 5;
			start = false;
		} else if(timer < state && state == 0 && !start) {
			gameEnded ();
			state = -1;
		}
	}
}
