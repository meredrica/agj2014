using UnityEngine;
using System.Collections;

public class KillInSeconds : MonoBehaviour {
	public float KillTimer;
	public float FadeTime;
	float mTimeAccum;

	void Update ()
	{
		mTimeAccum += Time.deltaTime;
		
		if(FadeTime > 0) {
			float fadeStart = KillTimer - FadeTime;
			if(mTimeAccum >= fadeStart) {
				float alphaValue = (mTimeAccum - fadeStart)/FadeTime;
				this.transform.GetChild(0).gameObject.renderer.material.SetFloat("_Cutoff", 1 - alphaValue);
			}
		}
		
		if (mTimeAccum >= KillTimer)
			gameObject.SetActive(false);
	}
}
