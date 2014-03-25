using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public Camera CameraObj;
	public GameObject Player;

	public float Speed = 0.5f;
	public float MinSize = -5.0f;
	public float MaxSize = 10.0f;

	float fCameraSize = 5.0f;
	const float fDefaultCameraSize = 5.0f;
	
	void Update()
	{
		fCameraSize = fDefaultCameraSize + Player.transform.position.y;

		if(fCameraSize >= MaxSize)
			fCameraSize = MaxSize;
		else if(fCameraSize <= MinSize)
			fCameraSize = MinSize;

		CameraObj.orthographicSize = Mathf.Lerp(CameraObj.orthographicSize, fCameraSize, Time.deltaTime / Speed);
	}
}
