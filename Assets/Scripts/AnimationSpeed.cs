using UnityEngine;
using System.Collections;

public class AnimationSpeed : MonoBehaviour
{
	public CharacterController CharController;
	float mSpeed;
	Animator[] mAnimators;

	// Use this for initialization
	void Start ()
	{
		mAnimators = GetComponentsInChildren<Animator>();

		if (mAnimators.Length == 0) this.enabled = false;

		UpdateAnimationSpeed(CharController.Speed);
	}

	void Update ()
	{
		if (mSpeed != CharController.Speed)
			UpdateAnimationSpeed(CharController.Speed);
	}
	
	void UpdateAnimationSpeed (float speed)
	{
		mSpeed = speed;
		foreach(Animator animator in mAnimators)
		{
			animator.speed = mSpeed;
		}
	}
}
