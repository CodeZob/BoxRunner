using UnityEngine;
using System.Collections;

public class Scroll_Mapping : MonoBehaviour {

	public float fScrollSpeed = 0.5f;
	float fTargetOffset;

	void Update()
	{
		fTargetOffset += Time.deltaTime * fScrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(fTargetOffset, 0.0f);
	}
}
