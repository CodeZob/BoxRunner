using UnityEngine;
using System.Collections;

public enum PlayerState
{
	RUN,
	JUMP,
	DJUMP,
	DEATH
}

public class Player_Ctrl : MonoBehaviour {

	public PlayerState ePS;

	public float fJumpPower = 500.0f;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(ePS == PlayerState.JUMP)
			{
				DJump();
			}

			if(ePS == PlayerState.RUN)
			{
				Jump();
			}
		}
	}

	void OnCollisionEnter(Collision co)
	{
		if(ePS != PlayerState.RUN && ePS != PlayerState.DEATH)
		{
			Run();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		rigidbody.WakeUp();
		if(other.gameObject.name == "Coin")
		{
			Destroy(other.gameObject);
			CoinGet();
		}

		if(other.gameObject.name == "DeathZone" && ePS != PlayerState.DEATH)
		{
			GameOver();
		}
	}

	//--------------------------------------------------------------

	void Jump()
	{
		ePS = PlayerState.JUMP;

		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
	}

	void DJump()
	{
		ePS = PlayerState.DJUMP;

		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
	}

	void Run()
	{
		ePS = PlayerState.RUN;
	}

	void CoinGet()
	{
	}

	void GameOver()
	{
		ePS = PlayerState.DEATH;
		this.collider.enabled = false;
	}
}
