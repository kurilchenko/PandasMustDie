using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour 
{
	public float characterSpeed = 0.3f;
	public float jumpPower = 20f;
	public float laserRotationSpeed = 2f;

	public bool firstPlayer = true;



	public GameObject gun;


	private bool isGrounded = true;
	//int dir = 1;

	Rigidbody2D rigidbody;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
		gameObject.tag = "Astronaut";
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

		//if (horizontal != 0)


		//{

		float dir = Input.GetAxis (firstPlayer ? "Horizontal_" : "Horizontal2_");

		if (dir < 0 && transform.localScale.x > 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (dir > 0 && transform.localScale.x < 0)
			transform.localScale = Vector3.one;

		rigidbody.AddForce (Vector2.right * horizontal, ForceMode2D.Impulse);

		float gunRotation = -Input.GetAxis (firstPlayer ? "Vertical" : "Vertical2") * 90f;



		//float angle = gunRotation * laserRotationSpeed;
		//if(gun.transform.localEulerAngles.z - angle < 60 || gun.transform.localEulerAngles.z - angle > 300)
			//gun.transform.localEulerAngles -= Vector3.forward * gunRotation * laserRotationSpeed;
		gun.transform.localEulerAngles = new Vector3(0, 0, gunRotation);


		if (isGrounded && Input.GetKeyDown (firstPlayer ? KeyCode.Joystick1Button14 : KeyCode.Joystick2Button14))
		{
			rigidbody.AddRelativeForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}
}
