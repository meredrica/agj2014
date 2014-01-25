using UnityEngine;
using System.Collections;

public class PlayerDamageTaker : DamageTaker {

	public override void takeDamage (GameObject killer) {
		if(!killer.Equals(this.gameObject)) {
			Destroy(this.gameObject);
		}
	}
}
