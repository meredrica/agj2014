using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {
	public Anchoring Anchor;
	public Texture Flag;
	public Texture KillIcon;
	public Texture DeathIcon;
	public Font MyFont;

	public int Kills;
	public int Deaths;

	public enum Anchoring
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	void OnGUI()
	{
		var offset = Vector2.zero;

		var screenWidth = Screen.width;
		var screenHeight = Screen.height;

		if (Anchor == Anchoring.TopLeft)
			offset = new Vector2(0,20);
		else if (Anchor == Anchoring.TopRight)
			offset = new Vector2(screenWidth - 486, 20);
		else if (Anchor == Anchoring.BottomLeft)
			offset = new Vector2(0, screenHeight - 80);
		else if (Anchor == Anchoring.BottomRight)
			offset = new Vector2(screenWidth - 486, screenHeight - 80);

		GUIStyle myStyle = new GUIStyle();
		myStyle.font = MyFont;
		myStyle.fontSize = 40;

		GUI.DrawTexture(new Rect(offset.x,offset.y, Flag.width, Flag.height), Flag);
		offset += new Vector2(Flag.width, 0);

		GUI.DrawTexture(new Rect(offset.x, offset.y, KillIcon.width, KillIcon.height), KillIcon);
		offset += new Vector2(KillIcon.width, 0);

		GUI.Label(new Rect(offset.x + 20, offset.y + 10, 80, 30), Kills.ToString(), myStyle);
		offset += new Vector2(100, 0);

		GUI.DrawTexture(new Rect(offset.x, offset.y, DeathIcon.width, DeathIcon.height), DeathIcon);
		offset += new Vector2(KillIcon.width, 0);

		GUI.Label(new Rect(offset.x + 20, offset.y + 10, 80, 30), Deaths.ToString(), myStyle);
		offset += new Vector2(100, 0);
	}
}
