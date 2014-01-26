using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneSetup : MonoBehaviour {
	public int numNPCs = 30;
	public Vector3 minBounds;
	public Vector3 maxBounds;
	public GameObject[] players;
	public GameObject[] skins;
	AudioManager audioManager;

	// Use this for initialization
	public void StartGame ()
	{
		PlayerSelection gameManager = FindObjectOfType(typeof(PlayerSelection)) as PlayerSelection;
		audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;
		audioManager.setup = this;
		audioManager.PlayMusic();
		int playerCount = gameManager.inputs.Count;
		players = new GameObject[playerCount];
		var quat = Quaternion.identity;
		Debug.Log("spawning "+playerCount+" players");
		// shuffle the skins
		int n = skins.Length;
		while(n>1) {
			n--;
			int k = Random.Range(0,n);
			var t = skins[n];
			skins[n] = skins[k];
			skins[k] = t;
		}
		
		for(int i = 0; i<playerCount; i++)
		{
			players[i] = (GameObject) Instantiate(Resources.Load<GameObject>("Player"),rangedPosition(i),quat);
			players[i].GetComponent<PlayerMover>().wrapper = gameManager.inputs[i];
			var skin = (GameObject)Instantiate(skins[i % skins.Length]);
			skin.transform.parent = players[i].transform;
			skin.transform.localPosition = Vector3.zero;
			// TODO: ui binding
		}
		
		
		for(int i = 0; i < numNPCs; i++) {
		
		
			GameObject npc = (GameObject)Instantiate(Resources.Load<GameObject>("NPC"),rangedPosition(i),quat);
			GameObject skin = (GameObject)Instantiate(skins[i % skins.Length]);
			skin.transform.parent = npc.transform;
			skin.transform.localPosition = Vector3.zero;

			AIController npcController = npc.GetComponent<AIController>();
			npcController.MinBounds = minBounds;
			npcController.MaxBounds = maxBounds;
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	Vector3 rangedPosition(int i){
		int div = i%4;
		
		float minX = 0;
		float maxX = 0;
		float minZ = 0;
		float maxZ = 0;
		switch (div) {
		case 0: // top left
			minX = minBounds.x;
			maxX = 0;
			minZ = minBounds.z ;
			maxZ = 0;
			break;
		case 1: // top right
			minX = 0;
			maxX = maxBounds.x;
			minZ = minBounds.z ;
			maxZ = 0;
			break;
		case 2: // bottom left
			minX = minBounds.x;
			maxX = 0;
			minZ = 0;
			maxZ = maxBounds.z;
			break;
		default: // bottom right
			
			minX = 0;
			maxX = maxBounds.x;
			minZ = 0;
			maxZ = maxBounds.z;
			break;
		}
		
		//Debug.Log("x: ["+minX+","+maxX+"] z: ["+minZ+","+maxZ+"]");
		
		var newX = Random.Range(minX, maxX);
		var newZ = Random.Range(minZ, maxZ);
		
		//Debug.Log("i: " + i +" nexX: "+newX+" newZ: "+newZ);
		return new Vector3((int)newX, 0, (int)newZ);
	}
	
	
}
