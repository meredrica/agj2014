using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
	public CharacterController CharController;
	public Vector3 MinBounds = new Vector3(-15, 0, 10);
	public Vector3 MaxBounds = new Vector3(15, 0, -10);

	public float NewTargetPositionInterval = 9.0f;
	float randomTime;

	float mTimeAccum = 0;

	void Start()
	{
		recalculate();
	}

	// Update is called once per frame
	void Update ()
	{
		mTimeAccum += Time.deltaTime;

		if (mTimeAccum >= randomTime)
		{
			recalculate();
			mTimeAccum = 0;
		}
	}
	
	public void recalculate() {
		randomTime = Random.Range(3,NewTargetPositionInterval);
		// decide on a quadrant first
		var rand = Random.Range(0,4);
		// reuse magic from scene setup
		CharController.TargetPosition = SceneSetup.rangedPosition(rand,MinBounds,MaxBounds);
		CharController.MoveToPosition();
	}
}
