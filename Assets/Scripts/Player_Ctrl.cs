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

	public float fJumpPower = 13.0f;
	private bool bIsJump;

	public AudioClip[] Sounds;

	public Animator animator;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && ePS != PlayerState.DEATH)
		{
			bIsJump = true;
		}
	}

	void FixedUpdate()
	{
		if(bIsJump)
		{
			if(ePS == PlayerState.JUMP)
			{
				DJump();
			}
			
			if(ePS == PlayerState.RUN)
			{
				Jump();
			}
			bIsJump = false;
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
//		fJumpPower = (fJumpPower * 50.0f) * Time.deltaTime;
//		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
		rigidbody.velocity = new Vector3(0.0f, fJumpPower, 0.0f);
		SoundPlay(Sound.JUMP);

		animator.SetTrigger("Jump");
		animator.SetBool("Ground", false);
	}

	void DJump()
	{
		ePS = PlayerState.DJUMP;
//		fJumpPower = (fJumpPower * 70.0f) * Time.deltaTime;
//		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
		rigidbody.velocity = new Vector3(0.0f, fJumpPower, 0.0f);
		SoundPlay(Sound.JUMP);

		animator.SetTrigger("D_Jump");
		animator.SetBool("Ground", false);
	}

	void Run()
	{
		ePS = PlayerState.RUN;

		animator.SetBool("Ground", true);
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
