using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	public Vector3 TargetPosition;
	public bool UpdatePositionOffset;
	public float Speed;
	
	Vector3 mStartPosition;
	Vector3 mTargetPosition;
	public bool mMove;
	float mTimeAccum;
	
	public void MoveToPosition ()
	{
		mTimeAccum = 0;
		
		mStartPosition = transform.localPosition;
		mTargetPosition = TargetPosition;
		
		UpdatePositionOffset = false;
		
		mMove = true;
	}
	
	void Update ()
	{
		if (UpdatePositionOffset)
			MoveToPosition();
		
		if (!mMove) return;
		mTimeAccum += Time.deltaTime;
		
		var distance = Vector3.Distance(mTargetPosition, mStartPosition);
		
		transform.localPosition = Vector3.Lerp(mStartPosition, mTargetPosition, mTimeAccum * Speed / distance);
		
		if (Vector3.Distance(transform.position, mTargetPosition) <= 0.01f)
			mMove = false;
	}
}
