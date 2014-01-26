using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
	public CharacterController CharController;
	public Vector3 MinBounds = new Vector3(-15, 0, 10);
	public Vector3 MaxBounds = new Vector3(15, 0, -10);

	public float NewTargetPositionInterval = 3.0f;

	float mTimeAccum = 0;

	void Start()
	{
		var newTargetPosition = CalculateNewTargetPosition();
		CharController.TargetPosition = newTargetPosition;
		CharController.MoveToPosition();
		mTimeAccum = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		mTimeAccum += Time.deltaTime;

		if (mTimeAccum >= NewTargetPositionInterval)
		{
			recalculate();
		}
	}
	
	public void recalculate() {
		var newTargetPosition = CalculateNewTargetPosition();
		CharController.TargetPosition = newTargetPosition;
		CharController.MoveToPosition();
		mTimeAccum = 0;
	}

	Vector3 CalculateNewTargetPosition ()
	{
		var newX = (int)Random.Range(MinBounds.x, MaxBounds.x);
		var newZ = (int)Random.Range(MinBounds.z, MaxBounds.z);

		return new Vector3(newX, transform.position.y, newZ);
	}
}
