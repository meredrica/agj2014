using UnityEngine;
using XInputDotNetPure; // Required in C#
using System.Collections;

public interface InputWrapper {
	float getXMod();
	float getYMod();
	bool murder();
}

public class Keyboard1Controls : InputWrapper {
	public float getXMod() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			return -1;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			return 1;
		}
		
		return 0;
	}
	
	public float getYMod() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			return -1;
		} else if (Input.GetKey(KeyCode.PageDown)) {
			return 1;
		}
		
		return 0;
	}
	
	public bool murder() {
		return Input.GetKeyDown(KeyCode.RightControl);
	}
}

public class GamepadControls : InputWrapper {
	PlayerIndex index;
	
	public GamepadControls(int player) {
		index = (PlayerIndex)player;
	}
	
	public float getXMod() {
		var state = GamePad.GetState(index);
		
		if(Mathf.Abs (state.ThumbSticks.Left.X) > 0) {
			return state.ThumbSticks.Left.X;
		} else if((bool)state.DPad.Left) {
			return -1;
		} else if((bool)state.DPad.Right) {
			return 1;
		}
		
		return 0;
	}
	
	public float getYMod() {
		var state = GamePad.GetState(index);
		
		if(Mathf.Abs (state.ThumbSticks.Left.Y) > 0) {
			return state.ThumbSticks.Left.X;
		} else if((bool)state.DPad.Down) {
			return -1;
		} else if((bool)state.DPad.Up) {
			return 1;
		}
		
		return 0;
	}
	
	public bool murder() {
		var state = GamePad.GetState(index);
		
		return state.Buttons.X;
	}
}

public class Keyboard2Controls : InputWrapper {
	public float getXMod() {
		if (Input.GetKey (KeyCode.A)) {
			return -1;
		} else if (Input.GetKey(KeyCode.D)) {
			return 1;
		}
		
		return 0;
	}
	
	public float getYMod() {
		if (Input.GetKey (KeyCode.S)) {
			return -1;
		} else if (Input.GetKey(KeyCode.W)) {
			return 1;
		}
		
		return 0;
	}
	
	public bool murder() {
		return Input.GetKeyDown(KeyCode.Space);
	}
}

public class PlayerMover : MonoBehaviour {

	public float speed = 7;
	public int controllerPlayer = -1;
	public int keyboardPlayer = -1;
	
	InputWrapper wrapper = null;
	
	// Use this for initialization
	void Start () {
		checkWrapper();
	}
	
	private void checkWrapper() {
		if(wrapper == null) {
			if(controllerPlayer > -1) {
				wrapper = new GamepadControls(controllerPlayer);
			} else if(keyboardPlayer == 0) {
				wrapper = new Keyboard1Controls();
			} else if(keyboardPlayer == 1) {
				wrapper = new Keyboard2Controls();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		checkWrapper();
		
		float delta = Time.deltaTime;
		
		if(wrapper != null) {
			//TODO use move script
			transform.Translate (wrapper.getXMod() * speed * delta,0 , wrapper.getYMod() * speed * delta);
			
			if(wrapper.murder()) {
				//TODO Send murder message
			}
		}
	}
}
