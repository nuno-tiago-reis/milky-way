using UnityEngine;

using System.Collections;

public class HUD : MonoBehaviour {

	private Spaceship spaceShip
	{ get; set; }
	
	private Texture2D placeTexture
	{get; set; }
	
	private Texture2D lapTexture
	{get; set; }

	// Use this for initialization
	void Start () {

		Debug.Log ("Start of HUD");

		//this.spaceShip = GameObject.Find("SpaceShip").GetComponent<Spaceship>();
		
		// Place Texture change name
		this.placeTexture = (Texture2D)Resources.Load("Textures/HUD/1stPlace",typeof(Texture2D)) as Texture2D;
		
		// Lap Texture
		this.lapTexture = (Texture2D)Resources.Load("Textures/HUD/1Lap",typeof(Texture2D)) as Texture2D;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI() {

		//Debug.Log ("OnGUI of HUD");
		
		GUI.DrawTexture(new Rect(Screen.width * 0.01f, Screen.height * 0.01f, 150.0f, 45.0f), this.placeTexture);
		
		GUI.DrawTexture(new Rect(Screen.width * 0.01f, Screen.height * 0.01f + 55.0f, 150.0f, 45.0f), lapTexture);
	
	}
}
