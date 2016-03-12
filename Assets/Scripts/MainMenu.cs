using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button CommenceBtn;
    public Button AbortBtn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxisRaw("Vertical") > 0) {
            CommenceBtn.Select();
            
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            AbortBtn.Select();
        }
	}

    public void Commence()
    {
        SceneManager.LoadScene("");
    }

    public void Abort()
    {
        Application.Quit();
    }
}
