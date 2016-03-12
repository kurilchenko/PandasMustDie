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
    float rayMinWidth = 0.05f;
    float rayMaxWidth = 0.1f;
    Vector3 rayOrigin;
    Vector3 rayHit;
    bool isVisualizingRay;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        var isShooting = true;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Make smaller");
            rayState = Actor.SizeEnum.Small;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Make larger ");
            rayState = Actor.SizeEnum.Regular;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Make larger ");
            rayState = Actor.SizeEnum.Large;
        }
        else
        {
            isShooting = false;
            rayState = Actor.SizeEnum.Regular;

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
        var hit = Physics2D.Raycast(originPoint2D, direction2D);

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
        Gizmos.DrawLine(gunpoint.transform.position, gunpoint.transform.position + gunpoint.transform.right);
    }
}
