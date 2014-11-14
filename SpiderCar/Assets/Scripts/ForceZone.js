public var direction:Vector3;

function OnTriggerStay (collider:Collider) {
	Debug.Log("hey0");
	if (collider.tag == 'Player') {
		Debug.Log("hey");
		collider.rigidbody.AddForce(direction, ForceMode.Acceleration);
	}
}