using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Actor
{
	public float characterSpeed = 0.3f;
	public float jumpPower = 20f;
	public float laserRotationSpeed = 2f;

	public bool firstPlayer = true;

	Animator animator;


	public GameObject gun;


	private bool isGrounded = true;
	//int dir = 1;

	Rigidbody2D rigidbody;

	protected override void Start()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		gameObject.tag = "Astronaut";
		animator = GetComponent<Animator> ();
	}

	void Update()
	{

		InputControl ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Ground") || col.gameObject.tag.Equals ("Astronaut"))
			isGrounded = true;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Ground") || col.gameObject.tag.Equals ("Astronaut"))
			isGrounded = false;
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (!isGrounded && (col.gameObject.tag.Equals ("Ground") || col.gameObject.tag.Equals ("Astronaut")))
		{
			isGrounded = true;
		}
	}

	void InputControl()
	{
		float horizontal = Input.GetAxis (firstPlayer ? "Horizontal" : "Horizontal2");
		if (horizontal > 0.2)
			horizontal = 1;
		else if (horizontal < -0.2)
			horizontal = -1;
		else
			horizontal = 0;

		//if (horizontal != 0)


		//{

		float dir = Input.GetAxis (firstPlayer ? "Horizontal_" : "Horizontal2_");

		if (dir < 0 && transform.localScale.x > 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (dir > 0 && transform.localScale.x < 0)
			transform.localScale = Vector3.one;

		//rigidbody.AddForce (Vector2.right * horizontal, ForceMode2D.Impulse);
		if (horizontal != 0)
		{
			animator.CrossFade ("New Animation", 0);
			rigidbody.velocity = new Vector2 (horizontal * 20f, rigidbody.velocity.y);

		} else
		{
			animator.CrossFade ("New State", 0);
		}


		float gunRotation = -Input.GetAxis (firstPlayer ? "Vertical" : "Vertical2") * 90f;



		//float angle = gunRotation * laserRotationSpeed;
		//if(gun.transform.localEulerAngles.z - angle < 60 || gun.transform.localEulerAngles.z - angle > 300)
			//gun.transform.localEulerAngles -= Vector3.forward * gunRotation * laserRotationSpeed;
		gun.transform.localEulerAngles = new Vector3(0, 0, gunRotation);


		if (isGrounded && Input.GetKeyDown (firstPlayer ? KeyCode.Joystick3Button14 : KeyCode.Joystick4Button14))
		{
			rigidbody.velocity = new Vector2 (rigidbody.velocity.x, jumpPower);
			//rigidbody.AddRelativeForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}
}
