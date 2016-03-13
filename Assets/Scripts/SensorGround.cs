using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SensorGround : MonoBehaviour
{

    public delegate void SensorEvent(GameObject target);
    public SensorEvent GroundEntered;
    public SensorEvent GroundExited;

    public List<GameObject> grounds = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D target)
    {

        var potentialGround = target.gameObject;

        if (potentialGround.tag != "Ground")
            return;

        grounds.Remove(potentialGround);
        grounds.Add(potentialGround);

        if (GroundEntered != null)
        {
            GroundEntered.Invoke(potentialGround);
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {

        var potentialGround = target.attachedRigidbody != null ? target.attachedRigidbody.gameObject : target.gameObject;

        if (potentialGround.tag != "Ground")
            return;

        grounds.Remove(potentialGround);
            
        if (GroundExited != null)
        {
            GroundExited.Invoke(potentialGround);
        }
    }

}
