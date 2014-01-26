using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {
	public GameObject HelpScreen;

	void OnMouseDown ()
	{
		HelpScreen.SetActive(true);
	}

	void OnMouseUp ()
	{
		HelpScreen.SetActive(false);
	}
}
