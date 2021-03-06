﻿using UnityEngine;
using System.Collections;

public class PlayerDamageTaker : DamageTaker {
	public static int reward = 25;
	
	void Update () {
		if(!alive) {
			timer += Time.deltaTime;
			
			if(timer >= respawnTime) {
				timer = 0;
				respawnObject();
				
				NPCStorage npcStorage = GameObject.Find("NPCContainer").GetComponent<NPCStorage>();
				npcStorage.spawnAtPosition(transform.position);
			}
		}
	}
	
	private IEnumerator respawn() {
		yield return new WaitForSeconds(4);
	}
	
	protected override void rewardPoints (Killer killer)
	{
		//Rewards points
		
		killer.KillScore += reward;
	}
	
	protected override void resetPosition ()
	{
		//We know this is hacky, but who cares!?
		InBoundMover boundMover = GetComponent<InBoundMover>();
		Vector3 minBounds = boundMover.MinBounds;
		Vector3 maxBounds = boundMover.MaxBounds;
		
		var newX = (int)Random.Range(minBounds.x, maxBounds.x);
		var newZ = (int)Random.Range(minBounds.z, maxBounds.z);
		
		transform.position = new Vector3(newX, 0, newZ);
	}
}
