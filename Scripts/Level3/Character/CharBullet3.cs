using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBullet3 : MonoBehaviour
{
	Rigidbody2D rb2D;
	internal Vector3 direction;
	void Start()
	{
		Destroy(gameObject, 4);
		rb2D = GetComponent<Rigidbody2D>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
		{
			//print(" hit enemy ");
			SoundObject.audioComp3.Play_HitByFishman();
			collision.GetComponent<Fishman>().Destroy();
			Destroy(gameObject);
		}
	}
}
