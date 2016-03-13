using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Gun : MonoBehaviour
{

    public enum GunRayType
    {
        None,
        Enlarger,
        Reducer
    }

    public Transform gunpoint;
    public Actor.SizeEnum rayState;

    LineRenderer line;
    float rayMinWidth = 0.5f;
    float rayMaxWidth = 0.1f;
    Vector3 rayOrigin;
    Vector3 rayHit;
    bool isVisualizingRay;
	public LayerMask shootLayer;
	public Color enlargerColor = Color.red;
	public Color reducerColor = Color.blue;

	Player player;
	int layer = 0, layer2 = 0;

    void Start()
    {
        line = GetComponent<LineRenderer>();
		player = GetComponent<Player> ();
		layer = LayerMask.NameToLayer ("Player");
		layer2 = LayerMask.NameToLayer ("Player2");
    }

    void Update()
    {
        var isShooting = true;

		if ((Input.GetKeyDown(player.firstPlayer ? KeyCode.Q : KeyCode.U)) || Input.GetKeyDown(player.firstPlayer ? KeyCode.Joystick1Button8 : KeyCode.Joystick2Button8))
        {
			if(rayState == Actor.SizeEnum.Regular)
            	rayState = Actor.SizeEnum.Small;
			else if(rayState == Actor.SizeEnum.Large)
				rayState = Actor.SizeEnum.Regular;

			line.material.color = reducerColor;

			Debug.LogWarning (rayState);
        }
		else if((Input.GetKeyDown(player.firstPlayer ? KeyCode.E : KeyCode.O)) || Input.GetKeyDown(player.firstPlayer ? KeyCode.Joystick1Button9 : KeyCode.Joystick2Button9))
		{
			if (rayState == Actor.SizeEnum.Small)
				rayState = Actor.SizeEnum.Regular;
			else if(rayState == Actor.SizeEnum.Regular)
				rayState = Actor.SizeEnum.Large;

			line.material.color = enlargerColor;

			Debug.LogWarning (rayState);
		}
//        else if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            Debug.Log("Make larger ");
//            rayState = Actor.SizeEnum.Regular;
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            Debug.Log("Make larger ");
//            rayState = Actor.SizeEnum.Large;
//        }
        else
        {
            isShooting = false;
            //rayState = Actor.SizeEnum.Regular;

            if (isVisualizingRay)
            {
                Invoke("SetOffVisualization", 0.1f);
            }            
        }

        if (isShooting)
        {
            isVisualizingRay = true;
            CancelInvoke("SetOffVisualization");
            Shoot(rayState);
        }

        if (isVisualizingRay)
        {
            Visualize();

            if (!isShooting)
            {
                rayHit = GetHit().point;
            }
        }
    }

    RaycastHit2D GetHit()
    {
        var originPoint2D = new Vector2(gunpoint.position.x, gunpoint.position.y);
		var direction2D = new Vector2(gunpoint.right.x, gunpoint.right.y);
		var hit = Physics2D.Raycast(originPoint2D, direction2D, Mathf.Infinity, shootLayer);

        if (hit.transform == null)
        {
            var directionToVoid = new Vector3(gunpoint.right.x, gunpoint.right.y) * 100f;
            hit.point = gunpoint.position + directionToVoid;
        }

        return hit;
    }

    void Shoot(Actor.SizeEnum size)
    {
        var hit = GetHit();
        rayHit = hit.point;

        if (hit.transform == null)
            return;

        var actorHit = hit.transform.GetComponent<Actor>();

        if (actorHit != null)
        {
            actorHit.Size = size;
        }
    }

    void Visualize()
    {
        line.SetPosition(0, gunpoint.transform.position);
        line.SetPosition(1, rayHit);
        line.SetWidth(rayMinWidth, rayMinWidth);
    }

    void SetOffVisualization()
    {
        Debug.Log("Stop visualization");
        isVisualizingRay = false;
        line.SetWidth(0f, 0f);
        CancelInvoke("SetOffVisualization");
    }

    void OnDrawGizmos()
    {
        if (gunpoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(gunpoint.transform.position, 0.1f);
        Gizmos.DrawLine(gunpoint.transform.position, gunpoint.transform.position + gunpoint.transform.right * 5f);
    }
}
