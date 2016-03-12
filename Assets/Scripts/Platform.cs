using UnityEngine;
using System.Collections;

public class Platform : Block
{

    void Start()
    {
          	
    }

    void Update()
    {
        if (sensor == null)
            return; 

        foreach (var actor in sensor.actors)
        {
            if (actor.Size == Actor.SizeEnum.Large)
            {
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }

}
