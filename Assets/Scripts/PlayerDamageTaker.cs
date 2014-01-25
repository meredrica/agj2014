using UnityEngine;
using System.Collections;

public class PlayerDamageTaker : DamageTaker {

	public override void takeDamage (GameObject killer) {
		Destroy(this.gameObject);
	}
}
