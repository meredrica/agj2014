using UnityEngine;
using System.Collections;

public class SpawnEffect : MonoBehaviour {
	public float mSpawnTime = 1;
	public AnimationCurve Curve;
	
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
			float scaleValue = Curve.Evaluate(f/mSpawnTime);
			transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
			yield return null;
		}
		transform.localScale = new Vector3(1, 1, 1);
	}
}
