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
		List<int> usedSkins = new List<int>(players.Length);
		for(int i = 0; i<playerCount; i++)
		{
			var newX = (int)Random.Range(minBounds.x, maxBounds.x);
			var newZ = (int)Random.Range(minBounds.z, maxBounds.z);
			var position = new Vector3(newX, 0, newZ);
			
			players[i] = (GameObject) Instantiate(Resources.Load<GameObject>("Player"),position,quat);
			players[i].GetComponent<PlayerMover>().wrapper = gameManager.inputs[i];
			var skinId = Random.Range(0,skins.Length);
			int counter = 0;
			while(usedSkins.Contains(skinId) == false && counter++ < playerCount * 10) {
				skinId = Random.Range(0,skins.Length);
			}
			usedSkins.Add(skinId);
			var skin = (GameObject)Instantiate(skins[skinId]);
			skin.transform.parent = players[i].transform;
			skin.transform.localPosition = Vector3.zero;
			// TODO: ui binding
		}
		for(uint i = 0; i < numNPCs; i++) {
			var newX = (int)Random.Range(minBounds.x, maxBounds.x);
			var newZ = (int)Random.Range(minBounds.z, maxBounds.z);
			var position = new Vector3(newX, 0, newZ);

			GameObject npc = (GameObject)Instantiate(Resources.Load<GameObject>("NPC"),position,quat);
			
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
}
