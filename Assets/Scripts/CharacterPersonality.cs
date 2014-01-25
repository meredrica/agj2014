using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CharacterPersonality : MonoBehaviour
{
	public Material Skin;
	public GameObject Body;

	// Use this for initialization
	void Start ()
	{
		ChangeSkin(Skin);
	}

	public void ChangeSkin (Material skin)
	{
		Body.renderer.material = skin;
	}
}
