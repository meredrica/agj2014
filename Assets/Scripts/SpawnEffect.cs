using UnityEngine;
using System.Collections;

public class SpawnEffect : MonoBehaviour {
	public float mSpawnTime = 1;

	// Use this for initialization
	void Start () {
		startEffect();
	}
	
	public void startEffect() {
		transform.localScale = new Vector3(0, 0, 0);
		StartCoroutine("Fade");
	}
	
	IEnumerator Fade() {
		for(float f = 0; f < mSpawnTime; f += Time.deltaTime) {
			transform.localScale = new Vector3(f/mSpawnTime, f/mSpawnTime, f/mSpawnTime);
			yield return null;
		}
		
		transform.localScale = new Vector3(1, 1, 1);
	}
}
