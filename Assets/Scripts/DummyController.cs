using UnityEngine;
using System.Collections;

public class DummyController : MonoBehaviour {
	public float speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		InBoundMover mover = GetComponent<InBoundMover>();
		Vector3 direction = Vector3.zero;
		
		if(Input.GetKey(KeyCode.A)) {
			direction.x = -1;
		}
		else if(Input.GetKey(KeyCode.D)) {
			direction.x = 1;
		}
		
		if(Input.GetKey(KeyCode.W)) {
			direction.z = 1;
		}
		else if(Input.GetKey(KeyCode.S)) {
			direction.z = -1;
		}
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<Killer>().kill();
		}
		
		mover.moveToPosition(transform.position + direction * speed * Time.deltaTime);
	}
}
