using UnityEngine;
using System.Collections;

public abstract class DamageTaker : MonoBehaviour {
	public bool alive = true;
	protected float timer = 0;
	public float respawnTime = 4;
	public float dietime = 2;
	public AnimationCurve Curve;
	
	// Use this for initialization
	void Start () {
	
	}
	
	protected abstract void rewardPoints(Killer killer);
	
	protected abstract void resetPosition();
	
	public void takeDamage (GameObject killer) {
		
		//blood.localScale *= Random.Range(0.8f, 1.2f);
		//blood.localEulerAngles = new Vector3(0, Random.Range(0, 359), 0);
		
		alive = false;
		
		StartCoroutine("Die");
		
		rewardPoints(killer.GetComponent<Killer>());
	}
	
	IEnumerator Die() {
		for(float timer = 0; timer < dietime; timer += Time.deltaTime) {
			transform.localScale = generateScale(transform.localScale, Curve.Evaluate(timer/dietime));
			yield return null;
		}
		
		var allRenderer = GetComponentsInChildren<Renderer>();
		var parent = GetParent(allRenderer[0].transform);
		
		foreach(Renderer rend in allRenderer)
		{
			rend.enabled = false;
		}
		Quaternion quat = Quaternion.identity;
		
		var blood = Instantiate(Resources.Load<GameObject>("Blood"),parent.position,quat) as GameObject;
		blood.transform.Rotate(Vector3.up,Random.Range(0,360));
		
		resetPosition();
	}
	
	
	private Vector3 generateScale(Vector3 oldScale, float scale) {
		return new Vector3(oldScale.x < 0 ? -scale : scale,
		                   oldScale.y < 0 ? -scale : scale,
		                   oldScale.z < 0 ? -scale : scale);
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
	
	public void respawnObject() {
		var allRenderer = GetComponentsInChildren<Renderer>();
		foreach(Renderer rend in allRenderer)
		{
			rend.enabled = true;
			alive = true;
		}
		GetComponent<SpawnEffect>().startEffect();
	}
}
