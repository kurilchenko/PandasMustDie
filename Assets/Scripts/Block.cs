using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

    public SensorActor sensor;

    protected virtual void OnEnable()
    {
        sensor.ActorEntered += OnActorEnter;
        sensor.ActorExited += OnActorExit;
    }

    protected virtual void OnDisable()
    {
        sensor.ActorEntered -= OnActorEnter;
        sensor.ActorExited -= OnActorExit;
    }

    protected virtual void OnActorEnter(Actor actor)
    {
        //Debug.Log("Actor entered platform: " + actor);

        actor.transform.parent = transform;
    }

    protected virtual void OnActorExit(Actor actor)
    {
        //Debug.Log("Actor exited platform: " + actor);

        actor.transform.parent = null;
    }

}
