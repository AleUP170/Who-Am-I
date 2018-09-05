using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIgnore : MonoBehaviour {

	GameObject[] enemies;

	void Start()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

	void FixedUpdate()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject ene in enemies)
		{
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(),ene.GetComponent<Collider2D>());
		}
	}
}
