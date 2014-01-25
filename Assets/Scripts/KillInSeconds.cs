using UnityEngine;
using System.Collections;

public class KillInSeconds : MonoBehaviour {
	public float KillTimer;
	float mTimeAccum;

	void Update ()
	{
		mTimeAccum += Time.deltaTime;
		if (mTimeAccum >= KillTimer)
			gameObject.SetActive(false);
	}
}
