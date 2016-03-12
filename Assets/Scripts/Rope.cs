using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rope : MonoBehaviour 
{
	public GameObject firstPlayer, secondPlayer;

	public Sprite sprite;

	List<GameObject> parts;

	int count = 10;

	void Start () 
	{
		parts = new List<GameObject> ();


		float step = Mathf.Abs (firstPlayer.transform.position.x - secondPlayer.transform.position.x) / (float)count;

		HingeJoint2D joint = secondPlayer.AddComponent<HingeJoint2D> ();

		for (int i = 1; i < count; ++i)
		{
			if (i < count - 1)
			{

				joint.connectedBody = firstPlayer.GetComponent<Rigidbody2D> ();
			} else
			{
				GameObject point = new GameObject ("Point");
				point.transform.position = secondPlayer.transform.position + Vector3.up + Vector3.right * step * (float)i;
				HingeJoint2D tempJoint = point.AddComponent<HingeJoint2D> ();
				joint.connectedBody = point.AddComponent<Rigidbody2D> ();
				joint = tempJoint;
			}
		}
	}

}
