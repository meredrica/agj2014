using UnityEngine;
using System.Collections;

public abstract class DamageTaker : MonoBehaviour {
	public bool alive = true;
	protected float timer = 0;
	public float respawnTime = 4;
	
	// Use this for initialization
	void Start () {
	
	}
	
	protected abstract void rewardPoints(Killer killer);
	
	public void takeDamage (GameObject killer) {
		var allRenderer = GetComponentsInChildren<Renderer>();
		var parent = GetParent(allRenderer[0].transform);

		var blood = Instantiate(Resources.Load<GameObject>("Blood"),parent.position,Quaternion.identity) as Transform;
		//blood.localScale *= Random.Range(0.8f, 1.2f);
		//blood.localEulerAngles = new Vector3(0, Random.Range(0, 359), 0);

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

	Transform GetParent(Transform trans)
	{
		var parent = trans;
		var result = parent;
		while (parent != null)
		{
			parent = parent.parent;
			if (parent != null)
				result = parent;
		}

		return result;
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
