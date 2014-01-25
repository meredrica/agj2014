using UnityEngine;
using System.Collections;

public class NPCDamageTaker : DamageTaker {
	public override void takeDamage (GameObject killer) {
		//Play sounds and do other cool stuff
		Destroy(this.gameObject);
	}
}
