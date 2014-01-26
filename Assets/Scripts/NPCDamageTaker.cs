using UnityEngine;
using System.Collections;

public class NPCDamageTaker : DamageTaker {
	public static int penalty = 10;

	protected override void rewardPoints (Killer killer)
	{
		//Implement negative points
		killer.KillScore -= penalty;
		
		NPCStorage npcStorage = GameObject.Find("NPCContainer").GetComponent<NPCStorage>();
		npcStorage.addNPC(this.gameObject);
	}
	
	protected override void resetPosition ()
	{
	}
}
