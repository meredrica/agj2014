using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SystemMessages : MonoBehaviour
{
	public Font MyFont;
	public string Message;
	public int FontSize = 240;

	void OnGUI()
	{
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = MyFont;
		myStyle.fontSize = FontSize;

		Vector2 size = myStyle.CalcSize( new GUIContent(Message));

		GUI.Label(new Rect((Screen.width - size.x)/2, (Screen.height-size.y)/2, size.x, size.y), Message, myStyle);
	}
}
