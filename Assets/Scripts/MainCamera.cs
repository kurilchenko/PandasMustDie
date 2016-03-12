using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	public Transform target, target2;
	private float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	public float normalCameraSize = 7.5f;
	public float newCameraSize;
	public bool collectedFinalSym;
	public float cameraZoomSpeed = 1;

	void Start(){
		newCameraSize = normalCameraSize;
		//target = GameObject.FindObjectOfType<Character>().transform;
	}



	void OnTriggerEnter2D()
	{

	}


	void Update() {

		float currentCameraSize = GetComponent<Camera>().orthographicSize;

		GetComponent<Camera>().orthographicSize = Mathf.Lerp(currentCameraSize,newCameraSize,cameraZoomSpeed * Time.deltaTime);

		Vector3 center = target2.transform.position + (target.transform.position - target2.transform.position) / 2f + Vector3.up * 10f;

		// Добавить
		if(collectedFinalSym)
		{
			newCameraSize = 30;
			cameraZoomSpeed = 0.5f;
			dampTime = 1;

			transform.position = Vector3.SmoothDamp (transform.position, new Vector3(center.x,
					center.y + 10,
				-10f), ref velocity, dampTime);
		}
		//конец.


		else {
				Vector3 point = GetComponent<Camera>().WorldToViewportPoint(center);
				Vector3 delta = center - GetComponent<Camera>().ViewportToWorldPoint (new Vector3 (0.5f, 0.4f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
		} 
	}
}