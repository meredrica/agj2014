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
			transform.localScale = generateScale(transform.localScale, scaleValue);
			
			yield return null;
		}
		
		transform.localScale = generateScale(transform.localScale, 1);
	}
	
	private Vector3 generateScale(Vector3 oldScale, float scale) {
				return new Vector3(oldScale.x < 0 ? -scale : scale,
				                   oldScale.y < 0 ? -scale : scale,
				                   oldScale.z < 0 ? -scale : scale);
	}
}
