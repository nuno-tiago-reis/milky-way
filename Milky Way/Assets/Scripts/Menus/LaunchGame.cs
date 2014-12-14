using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class LaunchGame : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick() {

        Application.LoadLevel(levelToLoad);
    }
}
