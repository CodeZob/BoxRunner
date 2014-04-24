using UnityEngine;
using System.Collections;

public enum PlayerState
{
	RUN,
	JUMP,
	DJUMP,
	DEATH
}

public enum ESound
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

	public GameObject AnotherSpeaker;

	public ParticleSystem Cloud;

	public GameManager GM;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && ePS != PlayerState.DEATH)
		{
			bIsJump = true;
		}

		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				bIsJump = true;
			}
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
//		SoundPlay(ESound.JUMP);
		AnotherSpeaker.SendMessage("SoundPlay");

		animator.SetTrigger("Jump");
		animator.SetBool("Ground", false);

		Cloud.Stop();
	}

	void DJump()
	{
		ePS = PlayerState.DJUMP;
//		fJumpPower = (fJumpPower * 70.0f) * Time.deltaTime;
//		rigidbody.AddForce(new Vector3(0.0f, fJumpPower, 0.0f));
		rigidbody.velocity = new Vector3(0.0f, fJumpPower, 0.0f);
//		SoundPlay(ESound.JUMP);
		AnotherSpeaker.SendMessage("SoundPlay");

		animator.SetTrigger("D_Jump");
		animator.SetBool("Ground", false);

		Cloud.Stop();
	}

	void Run()
	{
		ePS = PlayerState.RUN;

		animator.SetBool("Ground", true);

		Cloud.Play();
	}

	void CoinGet()
	{
		audio.PlayOneShot(Sounds[0]);

		if(GM != null)
		{
			GM.CoinGet();
		}
	}

	void GameOver()
	{
		ePS = PlayerState.DEATH;
		SoundPlay(ESound.DEATH);

		GM.GameOver();
	}

	void SoundPlay(ESound snd)
	{
		audio.clip = Sounds[(int)snd];
		audio.Play();
	}
}
