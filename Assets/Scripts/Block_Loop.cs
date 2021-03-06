﻿using UnityEngine;
using System.Collections;

public class Block_Loop : MonoBehaviour {

	public float fSpeed = 3;
	public GameObject[] Blocks;
	public GameObject A_Zone;
	public GameObject B_Zone;

	void Awake()
	{
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		A_Zone.transform.Translate(Vector3.left * fSpeed * Time.deltaTime);
		B_Zone.transform.Translate(Vector3.left * fSpeed * Time.deltaTime);

		if(B_Zone.transform.position.x <= 0)
		{
			//SwapZone();
			Destroy(A_Zone);
			A_Zone = B_Zone;
			Make();
		}
	}

	void Make()
	{
		int nIndex = Random.Range(0, Blocks.Length);
		B_Zone = Instantiate(Blocks[nIndex], new Vector3(A_Zone.transform.position.x + 30, -5, 0), transform.rotation) as GameObject;
	}

	void SwapZone()
	{
		GameObject Temp;
		
		Temp = A_Zone;
		A_Zone = B_Zone;
		B_Zone = Temp;
		B_Zone.transform.Translate(A_Zone.transform.position.x + 30, 0, 0);
	}
}
