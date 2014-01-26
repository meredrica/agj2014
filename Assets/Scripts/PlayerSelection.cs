using UnityEngine;
using System.Collections;
using XInputDotNetPure;

using System.Collections.Generic;


public class PlayerSelection : MonoBehaviour {
	public GameObject[] GamepadIcons;
	public GameObject[] KeyboardIcons;
	public GameObject[] PlayerOns;
	public GameObject[] StartGameIcons;

	private List<PlayerIndex> unassigned = new List<PlayerIndex>();
	
	public List<InputWrapper> inputs = new List<InputWrapper>();
	private int players=0;
	private bool keyleftassigned = false;
	private bool keyrightassigned = false;

	bool mShowStartMessage;
	int mStartMessageIndex;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		unassigned.Add(PlayerIndex.One);
		unassigned.Add(PlayerIndex.Two);
		unassigned.Add(PlayerIndex.Three);
		unassigned.Add(PlayerIndex.Four);
	}
	
	// Update is called once per frame
	void Update () {
		
		// check if somebody presses start
		for(int i = 0; i<4;i++){
			var state = GamePad.GetState((PlayerIndex)i);
			if(state.Buttons.Start.Equals(ButtonState.Pressed)){
				startGame();
				return;
			}
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
			startGame();
			return;
		}
		 
	
		if(players ==4) {
			return;
		}

		if (players == 2 && !mShowStartMessage)
		{
			StartGameIcons[mStartMessageIndex].gameObject.SetActive(true);
			mShowStartMessage = true;
		}

		foreach (PlayerIndex index in unassigned) {
			var state = GamePad.GetState(index);
			if(state.Buttons.X.Equals(ButtonState.Pressed)){
				inputs.Add(new GamepadControls(index));

				var intCast = (int)index;
				PlayerOns[players].gameObject.SetActive(true);
				GamepadIcons[intCast].transform.parent = PlayerOns[players].transform;
				GamepadIcons[intCast].transform.localPosition = Vector3.zero;
				GamepadIcons[intCast].gameObject.SetActive(true);

				if (players == 0)
					mStartMessageIndex = 0;

				unassigned.Remove(index);
				players++;
				//Debug.Log("assigned player " + players);
				return;
			}
			
		}
		if (Input.GetKeyDown (KeyCode.LeftControl) && keyleftassigned == false) {
			inputs.Add(new KeyboardLeftControls());
			keyleftassigned = true;

			PlayerOns[players].gameObject.SetActive(true);
			KeyboardIcons[0].transform.parent = PlayerOns[players].transform;
			KeyboardIcons[0].transform.localPosition = Vector3.zero;
			KeyboardIcons[0].gameObject.SetActive(true);

			if (players == 0)
				mStartMessageIndex = 1;

			players++;
			//Debug.Log("assigned player " + players);
			return;
		}
		if (Input.GetKeyDown(KeyCode.RightControl) && keyrightassigned == false) {
			inputs.Add(new KeyboardRightControls());
			keyrightassigned = true;

			PlayerOns[players].gameObject.SetActive(true);
			KeyboardIcons[1].transform.parent = PlayerOns[players].transform;
			KeyboardIcons[1].transform.localPosition = Vector3.zero;
			KeyboardIcons[1].gameObject.SetActive(true);
			
			if (players == 0)
				mStartMessageIndex = 1;

			players++;
			//Debug.Log("assigned player " + players);
			return;
		}
	}
	private void startGame() {
		if(players <2) {
			return;
		}
		// TODO: implement
		//Debug.Log("start game");
		Application.LoadLevel(1);
	}
	
	
}