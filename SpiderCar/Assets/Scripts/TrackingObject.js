public var trackingTarget:GameObject;
public var positionT:float = 0.5;
public var rotationT:float = 0.5;

function FixedUpdate () {
	//trackingTarget.transform.position
	this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, trackingTarget.transform.position, positionT);
	this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, trackingTarget.transform.rotation, rotationT);
}
