using UnityEngine;

public class ModelController : MonoBehaviour {

	public void Start () {
	}

	public void FixedUpdate () {
		
		float angle = this.transform.localRotation.eulerAngles.z;

		if(angle > 180.0f)
			angle -= 360.0f;

		angle = angle * 0.97f;

		this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
	}
}
