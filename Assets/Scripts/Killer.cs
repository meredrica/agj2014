using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour {
	public float radius;
	private int killScore;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void kill() {
		var gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		DamageTaker nearestDt = null;
		float nearestDistance = Mathf.Pow(radius, 2);
		
		foreach(GameObject go in gameObjects) {
			DamageTaker dt = go.GetComponent<DamageTaker>();
			if(dt != null && !go.Equals(this.gameObject) && go.GetComponent<DamageTaker>().isAlive()) {
				float squareDistance = Mathf.Pow(go.transform.position.x - transform.position.x, 2)
					+ Mathf.Pow(go.transform.position.z - transform.position.z, 2);
				
				if(squareDistance <= nearestDistance) {
					nearestDt = dt;
					nearestDistance = squareDistance;
				}
			}
		}
		
		if(nearestDt != null) {
			nearestDt.takeDamage(this.gameObject);
		}
		
		Debug.Log("Score: " + killScore);
	}
	
	public int KillScore {
		get {
			return this.killScore;
		}
		set {
			killScore = value;
		}
	}
	
}
