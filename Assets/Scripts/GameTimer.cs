using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public struct Score {
	public int playerIndex;
	public int points;
	
	public Score(int playerIndex, int points) {
		this.points = points;
		this.playerIndex = playerIndex;
	}
	
	public static int compareByPoints(Score x, Score y) {
		return x.points.CompareTo(y.points)*-1;

	}
}

public class GameTimer : MonoBehaviour {
	public float gameDuration = 8;
	public float gameStartDelay = 3;
	public float gameStartEnd = -1;
	public SceneSetup sceneSetup;
	public SystemMessages SysMessage;
	public SystemMessages ScoreBoard;

	int state;
	bool start;
	float timer;
	string winnerMessage;
	List<Score> ranking;

	// Use this for initialization
	void Start () {
		state = 3;
		timer = gameStartDelay;
		start = true;
		winnerMessage = "";
	}
	
	private void displayCountdown(string message) {
		SysMessage.Message = message;
	}
	
	private void gameStarted() {
		Debug.Log ("Game started!");
		sceneSetup.StartGame();
	}
	
	private void gameEnded() {
		Debug.Log ("Game ended!");
		int playerIndex = 0;
		ranking = new List<Score>();
		
		foreach(var player in sceneSetup.players) {
			playerIndex++;
			
			PlayerMover mover = player.GetComponent<PlayerMover>();
			mover.wrapper = null;
			
			Killer killer = player.GetComponent<Killer>();
			killer.isActive = false;
			
			ranking.Add(new Score(playerIndex, killer.KillScore));
		}
		
		ranking.Sort(Score.compareByPoints);
		
		StartCoroutine("ShowWinner");
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if(timer < state && state > 0) {
			displayCountdown("" + state);
			state--;
		} else if(timer < state && state == 0 && start) {
			displayCountdown("GO!");
			state = -1;
			StartCoroutine("HideGo");
		} else if(timer < gameStartEnd && start) {
			timer = gameDuration;
			gameStarted();
			state = 5;
			start = false;
		} else if(timer < state && state == 0 && !start) {
			gameEnded ();
			displayCountdown("THE END");
			StartCoroutine("HideTheEnd");
			state = -1;
		}
	}

	IEnumerator HideGo()
	{
		yield return new WaitForSeconds(1);
		displayCountdown("");
	}

	IEnumerator HideTheEnd()
	{
		yield return new WaitForSeconds(3);
		displayCountdown("");
	}
	
	IEnumerator ShowWinner() {
		float yCharacterOffset = 2;
		float[] yCharacterStart = {-0.6f, 0.4f, 1.4f, 2.4f};
		
		yield return new WaitForSeconds(3);
		
		var cam = GameObject.Find("Main Camera");
		
		for(int i = 0; i < ranking.Count; i++) {
			if(i > 0) winnerMessage += "\n";
			
			winnerMessage += (i+1) + ".Player " + ranking[i].playerIndex + ": " + ranking[i].points;
			
			sceneSetup.players[i].transform.parent = cam.transform;
			Player_Controller cc = sceneSetup.players[i].GetComponent<Player_Controller>();
			cc.TargetPosition = new Vector3(-7.5f, yCharacterStart[ranking.Count-1] - yCharacterOffset*i, 51f);
			cc.UpdatePositionOffset = true;
		}
		
		ScoreBoard.Message = winnerMessage;
	}
}
