using UnityEngine;
using XInputDotNetPure; // Required in C#
using System.Collections;

public interface InputWrapper {
	float getXMod();
	float getZMod();
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
	
	public GamepadControls(int player) {
		index = (PlayerIndex)player;
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

public class Keyboard2Controls : InputWrapper {
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
	public Vector3 MinBounds = new Vector3(-15, 0, -10);
	public Vector3 MaxBounds = new Vector3(15, 0, 10);
	
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
			Vector3 result = new Vector3(wrapper.getXMod() * speed * delta,0 , wrapper.getZMod() * speed * delta) + transform.position;
			
			if(result.x < MinBounds.x) {
				Debug.Log("x < minX");
				result.x = MinBounds.x;
			} else if(result.x > MaxBounds.x) {
				Debug.Log("x > maxX");
				result.x = MaxBounds.x;
			}
			
			if(result.z < MinBounds.z) {
				Debug.Log("z < minZ");
				result.z = MinBounds.z;
			} else if(result.z > MaxBounds.z) {
				Debug.Log("z > maxZ");
				result.z = MaxBounds.z;
			}
			
			transform.position = result;
			
			if(wrapper.murder()) {
				//TODO Send murder message
			}
		}
	}
}
