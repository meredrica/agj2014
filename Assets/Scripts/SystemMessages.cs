using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SystemMessages : MonoBehaviour
{
	public Font MyFont;
	public string Message;

	void OnGUI()
	{
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = MyFont;
		myStyle.fontSize = 240;

		GUI.Label(new Rect((Screen.width - 200)/2, (Screen.height-200)/2, 200, 200), Message, myStyle);
	}
}
