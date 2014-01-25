using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour {
	public int numNPCs;
	public Vector3 minBounds;
	public Vector3 maxBounds;
	public GameObject[] players;
	
	// Use this for initialization
	public void StartGame ()
	{
		PlayerSelection gameManager = FindObjectOfType(typeof(PlayerSelection)) as PlayerSelection;

		int playerCount = gameManager.inputs.Count;
		players = new GameObject[playerCount];
		var quat = Quaternion.identity;
		Debug.Log("spawning "+playerCount+" players");
		for(int i = 0; i<playerCount; i++)
		{
			// TODO: random position
			var position = new Vector3(0,0,0);
			
			players[i] = (GameObject) Instantiate(Resources.Load<GameObject>("Player"),position,quat);
			players[i].GetComponent<PlayerMover>().wrapper = gameManager.inputs[i];

			var skin = (GameObject) Instantiate(Resources.Load<GameObject>("Skins/Skin_0"+(i+1)));
			skin.transform.parent = players[i].transform;
			skin.transform.localPosition = Vector3.zero;

			// TODO: ui binding
		}
		for(uint i = 0; i < numNPCs; i++) {
			var newX = (int)Random.Range(minBounds.x, maxBounds.x);
			var newZ = (int)Random.Range(minBounds.z, maxBounds.z);
			var position = new Vector3(newX, 0, newZ);

			GameObject npc = (GameObject)Instantiate(Resources.Load<GameObject>("NPC"));
			var skinName = "Skins/Skin_01";

			switch(i % playerCount) {
				case 1:
					skinName = "Skins/Skin_02";
					break;
				case 2:
					skinName = "Skins/Skin_03";
					break;
				case 3:
					skinName = "Skins/Skin_04";
					break;
			}
			
			GameObject skin = (GameObject)Instantiate(Resources.Load<GameObject>(skinName));
			skin.transform.parent = npc.transform;

			AIController npcController = npc.GetComponent<AIController>();
			npcController.MinBounds = minBounds;
			npcController.MaxBounds = maxBounds;
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
