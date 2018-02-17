using UnityEngine;

public class HoverController : MonoBehaviour
{
	public float hoverSpeed;

	public float hoverDistance;

	public Vector3 hoverPosition { get; protected set; }
	public Vector3 hoverDirection { get; protected set; }

	// Use this for initialization
	public void Start()
	{
		this.hoverPosition = new Vector3(0.0f, 0.0f, 0.0f);
		this.hoverDirection = new Vector3(0.0f, -1.0f, 0.0f);
	}

	// Update is called once per frame
	public void Update()
	{
	}

	public void FixedUpdate()
	{
		if (this.hoverPosition.magnitude > hoverDistance)
		{
			this.hoverPosition = new Vector3(0.0f, 0.0f, 0.0f);
			this.hoverDirection = -this.hoverDirection;
		}

		this.hoverPosition += this.hoverDirection * this.hoverSpeed;

		// Adjust the Objects Position
		this.transform.localPosition += this.hoverDirection * this.hoverSpeed;
	}
}