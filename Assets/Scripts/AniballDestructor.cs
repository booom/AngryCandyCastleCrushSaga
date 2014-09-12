using UnityEngine;
using System.Collections;

public class AniballDestructor : MonoBehaviour 
{
	private const float border = 14f;
	public GameObject explosion;

	void Update() 
	{
		if (transform.position.x > border || transform.position.x < -border)
		{
			Destruct();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.name == "ground")
		{
			Destruct();
		}
	}

	void Destruct()
	{
		if (explosion) Instantiate(explosion, transform.position, Quaternion.identity);
		NotificationCenter.DefaultCenter.PostNotification(this, "NextAniball");
		Destroy(gameObject);
	}
}
