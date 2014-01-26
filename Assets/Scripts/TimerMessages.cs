using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class TimerMessages : MonoBehaviour
{
	public Font MyFont;
	public string Message;

	void OnGUI()
	{
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = MyFont;
		myStyle.fontSize = 80;

		Vector2 size = myStyle.CalcSize( new GUIContent(Message));

		GUI.Label(new Rect((Screen.width - size.x)/2, 40, size.x, size.y), Message, myStyle);
	}
}
