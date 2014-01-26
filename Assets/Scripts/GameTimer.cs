using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public struct Score {
	public int playerIndex;
	public int points;
	public Color playerColor;
	
	public Score(int playerIndex, int points, Color playerColor) {
		this.points = points;
		this.playerIndex = playerIndex;
		this.playerColor = playerColor;
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

	public TimerMessages TimerMessage;
	public AudioSource InitialCountdown;
	public AudioSource VictorySound;

	public GameObject QuitGame;
	public GameObject[] StartGameIcons;

	public Transform[] RankAnchors;

	int state;
	bool start;
	float timer;
	string winnerMessage;
	List<Score> ranking;
	int mColorIndex;

	// Use this for initialization
	void Start () {
		state = 3;
		timer = gameStartDelay;
		start = true;
		//winnerMessage = "";
		InitialCountdown.Play();
		InitialCountdown.audio.pitch = 1.15f;
		mColorIndex = 0;
	}
	
	private void displayCountdown(string message) {
		SysMessage.Message = message;
	}

	private void displayCountdownSmall(string message) {
		TimerMessage.Message = message;
	}
	
	private void gameStarted() {
		//Debug.Log ("Game started!");
		sceneSetup.StartGame();
	}
	
	private void gameEnded() {

		//Debug.Log ("Game ended!");
		int playerIndex = 0;
		ranking = new List<Score>();

		//Debug.Log ("Game ended!");
		if(VictorySound != null)
			VictorySound.Play();

		foreach(var player in sceneSetup.players) {
			playerIndex++;

			Killer killer = player.GetComponent<Killer>();
			killer.isActive = false;
			Debug.Log(playerIndex + " " + mColorIndex);
			ranking.Add(new Score(playerIndex, killer.KillScore, sceneSetup.PlayerColors[playerIndex-1]));
			mColorIndex++;

		}
		
		var npcs = GameObject.FindObjectsOfType<AIController>();
		foreach (var npc in npcs)
		{
			npc.gameObject.SetActive(false);
		}

		//ranking.Sort(Score.compareByPoints);
		//ranking.Reverse();
		StartCoroutine("ShowWinner");
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer < state && state > 0) {
			displayCountdownSmall("" + state);
			state--;
		} else if(timer < state && state == 0 && start) {
			displayCountdownSmall("");
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
			displayCountdownSmall("");
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
		Vector3 IconOffset = new Vector3(0,0.75f,0);
		Vector3 CharacterOffset = new Vector3(-3, 0, 0);

		yield return new WaitForSeconds(3);

		var gm = FindObjectOfType<PlayerSelection>();
		StartGameIcons[gm.mStartMessageIndex].SetActive(true);
		QuitGame.SetActive(true);

		var cam = GameObject.Find("Main Camera");
		var counter = 0;

		for(int i = 0; i < ranking.Count; i++) {
			//if(i > 0) winnerMessage += "\n";
			
			//winnerMessage += (i+1) + ".Player " + ranking[i].playerIndex + ": " + ranking[i].points;
			
			InputWrapper wrapper = sceneSetup.players[i].GetComponent<PlayerMover>().wrapper;
			//Debug.Log(wrapper);
			GameObject playerId = null;
			TextMesh ScoreMesh = null;
			
			if(wrapper is KeyboardLeftControls) {
				playerId = sceneSetup.Keyboards[0];
				ScoreMesh = sceneSetup.KeyboardsTextMesh[0];
				//Debug.Log("LeftPlayer");
			} else if(wrapper is KeyboardRightControls) {
				playerId = sceneSetup.Keyboards[1];
				ScoreMesh = sceneSetup.KeyboardsTextMesh[1];
				//Debug.Log("RightPlayer");
			} else if(wrapper is GamepadControls) {
				GamepadControls gc = (GamepadControls)wrapper;
				
				int index = (int)gc.index;
				playerId = sceneSetup.Gamepads[index];
				ScoreMesh = sceneSetup.GamepadTextMesh[index];
				//Debug.Log("gamepad_0" + (index + 1));
			}
			
			if(playerId != null) {

				Vector3 pos = new Vector3(playerId.transform.localPosition.x, yCharacterStart[ranking.Count-1] - yCharacterOffset*i, playerId.transform.localPosition.z);
				//Debug.Log("Position: " + pos);
				//playerId.transform.position = pos;
				playerId.transform.parent = RankAnchors[counter];
				playerId.transform.localPosition = IconOffset;

				ScoreMesh.text = "" + ranking[i].points;
				ScoreMesh.color = ranking[counter].playerColor;
				playerId.SetActive(true);
				//sceneSetup.players[i].transform.parent = ScoreMesh.transform;
				//sceneSetup.players[i].transform.localPosition = Vector3.zero;
			}
			
			//sceneSetup.players[i].transform.parent = cam.transform;
			sceneSetup.players[i].transform.parent = RankAnchors[counter];
			sceneSetup.players[i].transform.localPosition = CharacterOffset;

			//Player_Controller cc = sceneSetup.players[i].GetComponent<Player_Controller>();
			//cc.TargetPosition = new Vector3(-7.5f, yCharacterStart[ranking.Count-1] - yCharacterOffset*i, 51f);
			//cc.UpdatePositionOffset = true;
			
			PlayerMover mover = sceneSetup.players[i].GetComponent<PlayerMover>();
			mover.wrapper = null;
			counter++;
		}
		
		//ScoreBoard.Message = winnerMessage;
	}
}
