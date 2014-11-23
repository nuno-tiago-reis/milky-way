using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

	private Spaceship spaceship
	{ get; set; }
	
	private Texture2D placeTexture
	{ get; set; }
	
	private Texture2D lapTexture
	{ get; set; }

	private Texture2D powerUpsBarTexture
	{ get; set; }

	private Texture2D speedometerTexture
	{ get; set; }

	private Texture2D pointerTexture
	{ get; set; }

	private Vector2 start
	{ get; set; }

	private Vector2 end
	{ get; set; }

	private Vector2 distance;

	private int width = 5;

	// Use this for initialization
	void Start () {

		Debug.Log ("Start of HUD");

		this.spaceship = GameObject.Find("Spaceship").GetComponent<Spaceship>();

		this.placeTexture = (Texture2D)Resources.Load("Textures/HUD/1stPlace",typeof(Texture2D)) as Texture2D;

		this.lapTexture = (Texture2D)Resources.Load("Textures/HUD/1Lap",typeof(Texture2D)) as Texture2D;

		this.powerUpsBarTexture = (Texture2D)Resources.Load("Textures/HUD/PowerUpBar",typeof(Texture2D)) as Texture2D;

		this.speedometerTexture = (Texture2D)Resources.Load("Textures/HUD/Speedometer",typeof(Texture2D)) as Texture2D;
	
		this.pointerTexture = (Texture2D)Resources.Load("Textures/HUD/Pointer",typeof(Texture2D)) as Texture2D;

		this.start = new Vector2 (Screen.width - 215.0f, Screen.height - 35.0f);

		this.end = new Vector2 (Screen.width - 375.0f, Screen.height - 35.0f);

		this.distance = end - start;
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 newEnd;

		float velocity = Mathf.Abs(spaceship.rigidbody.velocity.magnitude);

		float angle = (velocity / 150.0f  + 0.75f) * Mathf.PI;

		newEnd.x = distance.magnitude * Mathf.Cos (angle) - distance.magnitude * Mathf.Sin (angle);
		newEnd.y = distance.magnitude * Mathf.Sin (angle) + distance.magnitude * Mathf.Cos (angle);

		end = start + newEnd;
	}
			
	public void OnGUI() {

		//GUI.DrawTexture(new Rect(Screen.width * 0.51f, Screen.height * 0.5f, 450.0f, 298.0f), this.speedometerTexture);

		GUI.DrawTexture(new Rect(Screen.width * 0.01f, Screen.height * 0.01f, 150.0f, 45.0f), this.placeTexture);
		
		GUI.DrawTexture(new Rect(Screen.width * 0.01f, Screen.height * 0.01f + 55.0f, 150.0f, 45.0f), this.lapTexture);

		GUI.DrawTexture(new Rect(Screen.width * 0.71f, Screen.height * 0.62f, 450.0f, 298.0f), this.speedometerTexture);

		float size = 0.0f;

		// Draw the Pointer
		Vector2 distance = end - start;
		float a = Mathf.Rad2Deg * Mathf.Atan(distance.y / distance.x);
		if (distance.x < 0)
			a += 180;
		
		int width2 = (int) Mathf.Ceil(2 / 2);
		
		GUIUtility.RotateAroundPivot(a, start);
		GUI.DrawTexture(new Rect(start.x, start.y - width2, distance.magnitude, width), pointerTexture);
		GUIUtility.RotateAroundPivot(-a, start);

		if(spaceship.abilityMap.Keys != null)
			foreach (Ability ability in spaceship.abilityMap.Values) {
				
				GUI.DrawTexture (new Rect (Screen.width * 0.01f +  size, Screen.height * 0.8f - 10.0f, 100.0f, 120.0f), ability.getTexture());
				size += 120.0f;
			}
	}
}
