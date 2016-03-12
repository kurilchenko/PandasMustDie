using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutSceneHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoOn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
