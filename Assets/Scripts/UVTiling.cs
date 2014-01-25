using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UVTiling : MonoBehaviour {
	public float ScaleX;
	public float ScaleY;
	public bool Refresh;

	// Use this for initialization
	void Start ()
	{
		Repaint();
	}

	void Update ()
	{
		if (!Refresh) return;
		Repaint();
	}

	void Repaint ()
	{
		renderer.sharedMaterial.mainTextureScale = new Vector2(ScaleX, ScaleY);
		Refresh = false;
	}
}
