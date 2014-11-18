using UnityEngine;

public class HoverController : MonoBehaviour {
	
	private float hoverSpeed;

	private Vector3 hoverPosition;
	private Vector3 hoverDirection;

	// Use this for initialization
	public void Start () {
		
		this.hoverSpeed = 0.01f;
		this.hoverPosition = new Vector3(0.0f, 0.0f, 0.0f);
		this.hoverDirection = new Vector3(0.0f, -1.0f, 0.0f);
	}
	
	// Update is called once per frame
	public void Update () {
		
	}
	
	public void FixedUpdate() {

		Debug.Log("HoverSpeed = " + this.hoverSpeed);
		Debug.Log("HoverPosition = " + this.hoverPosition);
		Debug.Log("HoverDirection = " + this.hoverDirection);

		if(this.hoverPosition.magnitude > 0.5f) {

			this.hoverPosition = new Vector3(0.0f, 0.0f, 0.0f);
			this.hoverDirection = -this.hoverDirection;
		}

		this.hoverPosition += this.hoverDirection * this.hoverSpeed;
			
		// Adjust the Objects Position
		this.transform.localPosition += this.hoverDirection * this.hoverSpeed;
	}
}