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
		if (Input.GetKey (KeyCode.DownArrow)) {
			return -1;
		} else if (Input.GetKey(KeyCode.UpArrow)) {
			return 1;
		}
		
		return 0;
	}
	
	public bool murder() {
		return Input.GetKeyDown(KeyCode.RightControl);
	}
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
		return Input.GetKeyDown(KeyCode.LeftControl);
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
		
		return state.Buttons.X.Equals(ButtonState.Pressed);
	}
}


public class PlayerMover : MonoBehaviour {

	public float speed = 7;
	public Vector3 MinBounds = new Vector3(-15, 0, -10);
	public Vector3 MaxBounds = new Vector3(15, 0, 10);
	
	public InputWrapper wrapper = null;
	
	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {

		float delta = Time.deltaTime;
		
		if(wrapper != null) {
			Vector3 result = new Vector3(wrapper.getXMod() * speed * delta,0 , wrapper.getZMod() * speed * delta) + transform.position;
			
			if(result.x < MinBounds.x) {
				result.x = MinBounds.x;
			} else if(result.x > MaxBounds.x) {
				result.x = MaxBounds.x;
			}
			
			if(result.z < MinBounds.z) {
				result.z = MinBounds.z;
			} else if(result.z > MaxBounds.z) {
				result.z = MaxBounds.z;
			}
			
			transform.position = result;
			
			if(wrapper.murder()) {
				GetComponent<Killer>().kill();
			}
		}
	}
}
