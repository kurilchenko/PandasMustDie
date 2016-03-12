using UnityEngine;
using System.Collections;

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

    Vector3 rayOrigin;
    Vector3 rayHit;

    void Start()
    {

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
            rayOrigin = Vector3.zero;
            rayHit = Vector3.zero;
        }

        if (isShooting)
        {
            Shoot(rayState);
        }
    }

    void Shoot(Actor.SizeEnum size)
    {
        var originPoint2D = new Vector2(gunpoint.position.x, gunpoint.position.y);
        var direction2D = new Vector2(gunpoint.right.x, gunpoint.right.y);
        var hit = Physics2D.Raycast(originPoint2D, direction2D);

        if (hit.transform == null)
            return;

        var actorHit = hit.transform.GetComponent<Actor>();

        if (actorHit != null)
        {
            actorHit.Size = size;
        }
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
