using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class ShooterController : MonoBehaviour {

	public List<Transform> shooterTransformList
	{ get; protected set; }
	
	// When the game starts
	public void Start() {
		
		// Shooters reference to its Children
		this.shooterTransformList = new List<Transform>();

		shooterTransformList.Add(this.transform.FindChild("Shooter Position 0"));
		shooterTransformList.Add(this.transform.FindChild("Shooter Position 1"));
	}
	
	// Update is called once per frame
	public void Update () {
	}
	
	public void Shoot() {

		foreach(Transform shooterTransform in shooterTransformList) {

			// Instantiate the Lasers GameObject
			GameObject laser = GameObject.Instantiate(Resources.Load("Prefabs/Projectiles/Laser")) as GameObject;
			// Set the Parent transform to null
			laser.transform.parent = null;
			// Set the Rotation so that it matches the Spaceships.
			laser.transform.rotation = shooterTransform.parent.rotation;
			// Set the Position  so that it matches the Spaceships Shooter Position.
			laser.transform.position = shooterTransform.position - this.transform.forward * 2.5f;
			
			// Initialize the Rockets Controller
			LaserController laserController = laser.GetComponent<LaserController>();
			laserController.parent = shooterTransform.parent;

			// Activate the Rocket
			laserController.Activate();
		}
	}
}
