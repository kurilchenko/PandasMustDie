﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class PandaManipulator : MonoBehaviour
{
    public SensorActor sensor;
    public List<ActorNPC> pandas = new List<ActorNPC>();
    public Transform holdPoint;
    public float throwForce = 5000f;
    [HideInInspector]
    public ActorNPC pandaInHand;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void OnEnable()
    {
        sensor.ActorEntered += OnActorEnter;
        sensor.ActorExited += OnActorExit;
    }

    void OnDisable()
    {
        sensor.ActorEntered += OnActorEnter;
        sensor.ActorExited += OnActorExit;
    }

    void OnActorEnter(Actor actor)
    {
        if (!(actor is ActorNPC))
            return;

        var panda = actor as ActorNPC;

        if (panda.isBusy)
            return;

        pandas.Remove(panda);
        pandas.Add(panda);

        Debug.Log("Panda is around!");
    }

    void OnActorExit(Actor actor)
    {
        if (!(actor is ActorNPC))
            return;

        var panda = actor as ActorNPC;

        pandas.Remove(panda);
    }

    void Update()
    {
        if (Input.GetKeyDown(player.firstPlayer ? KeyCode.LeftControl : KeyCode.RightControl))
        {
            if (pandaInHand == null && pandas.Count != 0)
            {
                pandaInHand = pandas[0];
                pandaInHand.transform.parent = transform;
                pandaInHand.transform.transform.position = holdPoint.position;
                pandaInHand.GetComponent<Rigidbody2D>().isKinematic = true;
                pandaInHand.GetComponent<Rigidbody2D>().GetComponentInChildren<Collider2D>().isTrigger = true;
            }
            else if (pandaInHand != null)
            {
                Debug.Log("Throw panda by " + gameObject.name);
                pandaInHand.SetFly();
                pandaInHand.transform.parent = null;
                pandaInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                pandaInHand.GetComponent<Rigidbody2D>().GetComponentInChildren<Collider2D>().isTrigger = false;
                pandaInHand.GetComponent<Rigidbody2D>().AddForce(holdPoint.right * Time.deltaTime * throwForce, ForceMode2D.Impulse);
                pandaInHand = null;
            }
        }
    }

    

}
