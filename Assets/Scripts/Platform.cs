using UnityEngine;
using System.Collections;

public class Platform : Block
{

    public Actor.SizeEnum TargetSize;

    void Start()
    {
          	
    }

    void Update()
    {
        if (sensor == null)
            return; 

        foreach (var actor in sensor.actors)
        {
            if (actor.Size == TargetSize)
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }

}
