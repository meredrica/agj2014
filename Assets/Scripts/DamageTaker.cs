using UnityEngine;
using System.Collections;

public abstract class DamageTaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public abstract void takeDamage(GameObject killer);
}
