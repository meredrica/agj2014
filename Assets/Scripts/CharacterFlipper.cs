using UnityEngine;
using System.Collections;

public class CharacterFlipper : MonoBehaviour {
	private Vector3 lastPos;
	private bool posSet;

	// Use this for initialization
	void Start () {
		posSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if(posSet) {
			float xDiff = pos.x - lastPos.x;
			float mod = 1;
			
			if(xDiff < 0 && transform.localScale.x < 0 || xDiff > 0 && transform.localScale.x > 0) {
				mod = -1;
			}
			
			transform.localScale = new Vector3(transform.localScale.x * mod, transform.localScale.y, transform.localScale.z);
		}
		
		posSet = true;
		lastPos = pos;
	}
}
