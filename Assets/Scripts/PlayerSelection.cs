using UnityEngine;
using System.Collections;
using XInputDotNetPure;

using System.Collections.Generic;


public class PlayerSelection : MonoBehaviour {

	private List<PlayerIndex> unassigned = new List<PlayerIndex>();
	
	public List<InputWrapper> inputs = new List<InputWrapper>();
	private int players=0;
	private bool keyleftassigned = false;
	private bool keyrightassigned = false;
	// Use this for initialization
	void Start () {
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
		foreach (PlayerIndex index in unassigned) {
			var state = GamePad.GetState(index);
			if(state.Buttons.X.Equals(ButtonState.Pressed)){
				inputs.Add(new GamepadControls(index));
				unassigned.Remove(index);
				players++;
				Debug.Log("assigned player " + players);
				break;
			}
			if (Input.GetKeyDown (KeyCode.LeftControl) && keyleftassigned == false) {
				inputs.Add(new KeyboardLeftControls());
				keyleftassigned = true;
				players++;
				Debug.Log("assigned player " + players);
				break;
			}
			if (Input.GetKeyDown(KeyCode.RightControl) && keyrightassigned == false) {
				inputs.Add(new KeyboardRightControls());
				keyrightassigned = true;
				players++;
				Debug.Log("assigned player " + players);
				break;
			}
		}
	}
	private void startGame() {
		// TODO: implement
		Debug.Log("start game");
	}
	
	
}