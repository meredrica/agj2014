using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour {
	public float radius;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void kill() {
		var gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach(GameObject go in gameObjects) {
			DamageTaker dt = go.GetComponent<DamageTaker>();
			if(dt != null) {
				float squareDistance = Mathf.Pow(go.transform.position.x - transform.position.x, 2)
					+ Mathf.Pow(go.transform.position.z - transform.position.z, 2);
				Debug.Log(squareDistance);
				if(squareDistance < Mathf.Pow(radius, 2)) {
					dt.takeDamage(this.gameObject);
				}
			}
		}
	}
	
	
}
