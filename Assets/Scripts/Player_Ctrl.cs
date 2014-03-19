using UnityEngine;
using System.Collections;

public enum PlayerState
{
	RUN,
	JUMP,
	DJUMP,
	DEATH
}

public enum Sound
{
	COIN,
	DEATH,
	JUMP
}

public class Player_Ctrl : MonoBehaviour {

	public PlayerState ePS;

	public float fJumpPower = 500.0f;

	public AudioClip[] Sounds;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && ePS != PlayerState.DEATH)
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
		SoundPlay(Sound.JUMP);
	}

	void DJump()
	{
		ePS = PlayerState.DJUMP;
		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
		SoundPlay(Sound.JUMP);
	}

	void Run()
	{
		ePS = PlayerState.RUN;
	}

	void CoinGet()
	{
		audio.PlayOneShot(Sounds[0]);
	}

	void GameOver()
	{
		ePS = PlayerState.DEATH;
		SoundPlay(Sound.DEATH);
	}

	void SoundPlay(Sound snd)
	{
		audio.clip = Sounds[(int)snd];
		audio.Play();
	}
}
