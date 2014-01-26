using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCStorage : MonoBehaviour {
	public SceneSetup sceneSetup;
	public float minSpawn = 3;
	public float maxSpawn = 6;
	public float areaSize = 1;

	private Queue<GameObject> npcQueue = new Queue<GameObject>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void addNPC(GameObject npc) {
		npcQueue.Enqueue(npc);
	}
	
	public void spawnAtPosition(Vector3 spawnPosition) {
		int spawnCount = 0;
		
		while(spawnCount < maxSpawn && npcQueue.Count > 0) {
			GameObject npc = npcQueue.Dequeue();
			npc.GetComponent<InBoundMover>().moveToPosition(generateRandomPosition(spawnPosition));
			npc.GetComponent<AIController>().recalculate();
			npc.GetComponent<NPCDamageTaker>().respawnObject();
			spawnCount++;
		}
		
		while(spawnCount < minSpawn) {
			
			//Create a new NPC
			var position = generateRandomPosition(spawnPosition);
			
			GameObject npc = (GameObject)Instantiate(Resources.Load<GameObject>("NPC"),position,Quaternion.identity);
			
			GameObject skin = (GameObject)Instantiate(sceneSetup.skins[spawnCount % sceneSetup.skins.Length]);
			skin.transform.parent = npc.transform;
			skin.transform.localPosition = Vector3.zero;
			
			AIController npcController = npc.GetComponent<AIController>();
			npcController.MinBounds = sceneSetup.minBounds;
			npcController.MaxBounds = sceneSetup.maxBounds;
			spawnCount++;
		}
	}
	
	private Vector3 generateRandomPosition(Vector3 spawnPosition) {
		System.Random rand = new System.Random();
		return new Vector3((float)(spawnPosition.x - areaSize/2 + areaSize * rand.NextDouble()), spawnPosition.y,
			(float)(spawnPosition.z - areaSize/2 + areaSize * rand.NextDouble()));
 	}
}
