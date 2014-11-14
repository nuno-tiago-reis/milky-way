
function OnTriggerExit (collider:Collider) {
	if (collider.tag == 'Player') {
		Application.LoadLevel('SimpleRoad');
	}
}