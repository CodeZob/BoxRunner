using UnityEngine;
using System.Collections;

public enum GameState
{
	PLAY,
	PAUSE,
	END
}

public class GameManager : MonoBehaviour {

	GameState GS;

	public GUIText Text_Meter;
	public GUIText Text_Gold;

	public GameObject Final_GUI;

	public GUIText Text_FinalMeter;
	public GUIText Text_FinalGold;

	public GameObject Pause_GUI;

	public float Speed = 10;
	public float Meter = 0;
	public int Gold = 0;

	void Awake()
	{
		Speed = 10;
		Meter = 0;
		Gold = 0;
		GS = GameState.PLAY;
	}

	void Update()
	{
		if(GS == GameState.PLAY)
		{
			Meter += Time.deltaTime * Speed;

			Text_Meter.text = string.Format("{0:N0}m", Meter);
		}
	}

	public void CoinGet()
	{
		Gold++;
		Text_Gold.text = string.Format("{0}", Gold);
	}

	public void GameOver()
	{
		Text_FinalMeter.text = string.Format("{0:N1}", Meter);
		Text_FinalGold.text = string.Format("{0}", Gold);

		GS = GameState.END;
		Final_GUI.SetActive(true);
	}

	public void Replay()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel("PlayScene");
	}

	public void MainGo()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel("IntroScene");
	}

	public void Pause()
	{
		GS = GameState.PAUSE;
		Time.timeScale = 0.0f;
		Pause_GUI.SetActive(true);
	}

	public void Unpause()
	{
		GS = GameState.PLAY;
		Time.timeScale = 1.0f;
		Pause_GUI.SetActive(false);
	}
}
