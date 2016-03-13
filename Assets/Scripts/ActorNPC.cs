using UnityEngine;
using System.Collections;

public class ActorNPC : Actor
{
	public float speed = 3f;
	public float gravity = 10f;
    public bool isFlying;

    public void SetFly()
    {
        isFlying = true;
        Invoke("StopFlying", 3f);
    }

    void StopFlying()
    {
        isFlying = false;
    }

    void FixedUpdate()
	{
        if (isBusy || isFlying)
            return;

		GetComponent<Rigidbody2D> ().velocity = transform.right * speed - Vector3.up * gravity;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
        if (isBusy || isFlying)
            return;

        if (col.gameObject.tag.Equals ("Border"))
		{
			transform.localEulerAngles += Vector3.up * 180f;
		}
	}
}
