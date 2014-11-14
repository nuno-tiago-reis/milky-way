var smokeObject : GameObject;

private var stopTween = false;
function Awake(){
	SendMessage("startDestroy");
}

function startDestroy(){
	yield WaitForSeconds(4);
	if (!stopTween){ 
		Destroy(this.gameObject);
	}
}

function OnCollisionEnter(hit:Collision){
	if (hit.gameObject.name == "RoadBody") {
		stopTween = true;
		
		this.particleEmitter.emit = false;
		this.collider.isTrigger = true;
		
		var t : Transform = this.gameObject.transform;
		var smoke : GameObject = GameObject.Instantiate(smokeObject, t.position, t.rotation);
		
		yield WaitForSeconds(4);
		Destroy(smoke);
		Destroy(this.gameObject);
	} 
}

