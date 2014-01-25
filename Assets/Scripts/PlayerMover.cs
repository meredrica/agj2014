using UnityEngine;
using XInputDotNetPure; // Required in C#
using System.Collections;

public interface InputWrapper {
	float getXMod();
	float getZMod();
	bool murder();
}

public class KeyboardRightControls : InputWrapper {
	public float getXMod() {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			return -1;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			return 1;
		}
		
		return 0;
	}
	
	public float getZMod() {
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
	
	public GamepadControls(PlayerIndex player) {
		index = player;
	}
	
	public float getXMod() {
		var state = GamePad.GetState(index);
		
		if (Mathf.Abs (state.ThumbSticks.Left.X) > 0.3) {
			return Mathf.Sign(state.ThumbSticks.Left.X);
		} else {
			if(state.DPad.Left.Equals(ButtonState.Pressed)) {
				return -1;
			}
			if(state.DPad.Right.Equals(ButtonState.Pressed)) {
				return 1;
			}
		}
		return 0;
	}
	
	public float getZMod() {
		var state = GamePad.GetState(index);
		
		if (Mathf.Abs (state.ThumbSticks.Left.Y) > 0.3) {
			return Mathf.Sign(state.ThumbSticks.Left.Y);
		} else {
			if(state.DPad.Down.Equals(ButtonState.Pressed)) {
				return -1;
			}
			if(state.DPad.Up.Equals(ButtonState.Pressed)) {
				return 1;
			}
		}
		return 0;
	}
	
	public bool murder() {
		var state = GamePad.GetState(index);
		return true;
		/*
		return state.Buttons.X;
	*/}
}

public class KeyboardLeftControls : InputWrapper {
	public float getXMod() {
		if (Input.GetKey (KeyCode.A)) {
			return -1;
		} else if (Input.GetKey(KeyCode.D)) {
			return 1;
		}
		
		return 0;
	}
	
	public float getZMod() {
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
	}

	
	// Update is called once per frame
	void Update () {

		float delta = Time.deltaTime;
		
		
		if(wrapper != null) {
			Debug.Log("getX: "+wrapper.getXMod());
			Debug.Log("getZ: "+wrapper.getZMod());
					//TODO use move script
				transform.Translate (wrapper.getXMod() * speed * delta,0 , wrapper.getZMod() * speed * delta);
			
			if(wrapper.murder()) {
				//TODO Send murder message
			}
		}
	}
}
