using UnityEngine;
using System.Collections;

public abstract class DamageTaker : MonoBehaviour {
	protected bool alive = true;
	protected float timer = 0;
	public float respawnTime = 4;
	
	// Use this for initialization
	void Start () {
	
	}
	
	protected abstract void rewardPoints(Killer killer);
	
	public void takeDamage (GameObject killer) {
		var allRenderer = GetComponentsInChildren<Renderer>();
		foreach(Renderer rend in allRenderer)
		{
			rend.enabled = false;
		}
		
		alive = false;
		rewardPoints(killer.GetComponent<Killer>());
	}
	
	public bool isAlive() {
		return alive;
	}
	
	void Update () {
		if(!alive) {
			timer += Time.deltaTime;
			
			if(timer >= respawnTime) {
				var allRenderer = GetComponentsInChildren<Renderer>();
				foreach(Renderer rend in allRenderer)
				{
					rend.enabled = true;
					alive = true;
				}
				timer = 0;
			}
		}
	}
}
