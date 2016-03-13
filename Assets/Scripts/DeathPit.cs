using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DeathPit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.attachedRigidbody.gameObject.GetComponent<Player>())
        {
            Debug.Log("respawn");
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {

    }

}
