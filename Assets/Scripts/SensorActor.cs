using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class SensorActor : MonoBehaviour
{
    public delegate void SensorEvent(Actor actor);
    public SensorEvent ActorEntered;
    public SensorEvent ActorExited;

    public List<Actor> actors = new List<Actor>();

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.attachedRigidbody == null)
            return;

        var actor = target.attachedRigidbody.gameObject.GetComponent<Actor>();

        actors.Remove(actor);
        actors.Add(actor);

        if (ActorEntered != null)
        {
            ActorEntered.Invoke(actor);
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.attachedRigidbody == null)
            return;

        var actor = target.attachedRigidbody.gameObject.GetComponent<Actor>();

        actors.Remove(actor);

        if (ActorExited != null)
        {
            ActorExited.Invoke(actor);
        }
    }
}
