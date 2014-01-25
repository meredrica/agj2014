using UnityEngine;
using System.Collections;

public class InBoundMover : MonoBehaviour {
	public Vector3 MinBounds = new Vector3(-15, 0, 10);
	public Vector3 MaxBounds = new Vector3(15, 0, -10);
	
	public void moveToPosition(Vector3 newPosition) {
		float minX = Mathf.Min(MinBounds.x, MaxBounds.x);
		float maxX = Mathf.Max(MinBounds.x, MaxBounds.x);
		float minZ = Mathf.Min(MinBounds.z, MaxBounds.z);
		float maxZ = Mathf.Max(MinBounds.z, MaxBounds.z);
		
		if(newPosition.x < minX){
			newPosition.x = minX;
		}
		else if(newPosition.x > maxX) {
			newPosition.x = maxX;
		}
		
		if(newPosition.z < minZ) {
			newPosition.z = minZ;
		}
		else if(newPosition.z > maxZ) {
			newPosition.z = maxZ;
		}
		
		transform.position = newPosition;
	}
}
