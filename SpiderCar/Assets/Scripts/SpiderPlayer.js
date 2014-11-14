public var forwardSpeed:Number;
public var steerAngle:Number;
public var rotationT:float = 0.25;

function FixedUpdate () {
	var x:Number = Input.GetAxis("Horizontal");
	var y:Number = Input.GetAxis("Vertical");
	
	if (HitTestWithRoad()) {
	}
	this.rigidbody.velocity += y * transform.forward * forwardSpeed;
		
	this.rigidbody.AddTorque(transform.up * x * steerAngle, ForceMode.Acceleration);
}

function OnCollisionEnter(collision:Collision) {
	if (collision.gameObject.tag == 'roadWall') {
		//var contact:ContactPoint = collision.contacts[0];
		//this.gameObject.transform.position += contact.normal * 0.2;
	}
}

function HitTestWithRoad() {
	var position:Vector3 = transform.position + transform.TransformDirection(Vector3.up) * 0.2;
	var direction:Vector3 = transform.TransformDirection(Vector3.down);
    var ray:Ray = new Ray(position, direction);
    var hit:RaycastHit;
    var distance:float = 2;
    
    Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
    var inGround:boolean = false;
    if (Physics.Raycast(ray, hit, distance)) {
    	if (hit.collider.tag == 'road'){
		    this.transform.position = hit.point;
		    
		    //Debug.DrawLine(ray.origin, hit.point, Color.blue);
		    Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);
		    
		    var curUpVector:Vector3 = position - hit.point;
		    var hitUpVector:Vector3 = hit.normal;
		    Debug.DrawLine(hit.point, hit.point + curUpVector.normalized, Color.white);
		    
		    var targetQ:Quaternion;
			//TODO: これから進む先でrayを落とす(sphereのvelocity.normalizeを参照)
			var fPosition:Vector3 = transform.position + transform.TransformDirection(Vector3(0, 2.5, 1.0));
			var fDirection:Vector3 = transform.TransformDirection(Vector3.down);
		    var fRay:Ray = new Ray(fPosition, fDirection);
		    var fHit:RaycastHit;
		    var fDistance:float = 5;
		    Debug.DrawLine(fRay.origin, fRay.origin + fRay.direction * fDistance, Color.cyan);
		    if (Physics.Raycast(fRay, fHit, fDistance)) {
		    	if (fHit.collider.tag == 'road'){
				    Debug.DrawLine(fHit.point, fHit.point + fHit.normal * fDistance, Color.magenta);
				    targetQ.SetLookRotation(fHit.point - transform.position, hitUpVector);
		    	}
		    }
		    if (targetQ == null) {
				Debug.Log("None Forward");
			    targetQ.SetLookRotation(transform.TransformDirection(Vector3.forward), hitUpVector);
		    }
		    this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetQ, rotationT);

		    inGround = true;
    	}
    }
	
	return inGround;
}
