using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour {
	public int numNPCs;
	public Vector3 minBounds;
	public Vector3 maxBounds;

	
	// Use this for initialization
	void Start () {
		for(uint i = 0; i < numNPCs; i++) {
			GameObject npc = null;
		
			switch(i % 4) {
				case 0:
					npc = (GameObject)Instantiate(Resources.Load<GameObject>("Character_01"));
					break;
				case 1:
					npc = (GameObject)Instantiate(Resources.Load<GameObject>("Character_02"));
					break;
				case 2:
		           	npc = (GameObject)Instantiate(Resources.Load<GameObject>("Character_03"));
					break;
				case 3:
					npc = (GameObject)Instantiate(Resources.Load<GameObject>("Character_04"));
					break;
			}
			
			var newX = (int)Random.Range(minBounds.x, maxBounds.x);
			var newZ = (int)Random.Range(minBounds.z, maxBounds.z);
			
			AIController npcController = npc.GetComponent<AIController>();
			npcController.MinBounds = minBounds;
			npcController.MaxBounds = maxBounds;
			
			npc.transform.position = new Vector3(newX, 0, newZ);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
