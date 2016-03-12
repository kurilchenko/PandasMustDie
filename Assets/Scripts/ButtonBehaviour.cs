using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public Animator Anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnSelect(BaseEventData eventData)
    {
        Anim.SetBool("Selected", true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Anim.SetBool("Selected", false);
    }
}
