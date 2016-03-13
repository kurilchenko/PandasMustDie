using UnityEngine;
using System.Collections;

public class ActorNPC : Actor
{
	public float speed = 3f;

	void FixedUpdate()
	{
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Border"))
		{
			transform.localEulerAngles += Vector3.up * 180f;
		}
	}
}
