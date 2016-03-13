using UnityEngine;
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
		if (Input.GetKeyDown (KeyCode.Joystick2Button15))
			Debug.LogWarning (player.firstPlayer);
		if (Input.GetKeyDown(player.firstPlayer ? KeyCode.Joystick1Button15 : KeyCode.Joystick2Button15))
        {
            if (pandaInHand == null && pandas.Count != 0)
            {
                pandaInHand = pandas[0];
                pandaInHand.transform.parent = transform;
                pandaInHand.transform.position = holdPoint.position;
                pandaInHand.transform.rotation = Quaternion.identity;
                pandaInHand.GetComponent<Rigidbody2D>().isKinematic = true;
                pandaInHand.GetComponent<Rigidbody2D>().GetComponentInChildren<Collider2D>().isTrigger = true;
                pandaInHand.isBusy = true;
            }
            else if (pandaInHand != null)
            {
                Debug.Log("Throw panda by " + gameObject.name);
                pandaInHand.SetFly();
                pandaInHand.transform.parent = null;
                pandaInHand.GetComponent<Rigidbody2D>().isKinematic = false;
                pandaInHand.GetComponent<Rigidbody2D>().AddForce(holdPoint.right * throwForce, ForceMode2D.Impulse);
                Debug.Log(pandaInHand.GetComponent<Rigidbody2D>().velocity);
                pandaInHand.GetComponent<Rigidbody2D>().GetComponentInChildren<Collider2D>().isTrigger = false;
                pandaInHand.isBusy = false;
                pandaInHand = null;
            }
        }
    }

    

}
