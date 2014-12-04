using UnityEngine;

using System.Collections;

public class HUDLabel {
	
	/* Labels label */
	public string label
	{ get; set; }
	/* Labels Text */
	public string text
	{ get; set; }
	
	/* Bar Position */	
	public Vector2 position
	{ get; set; }
	
	/* Bar Dimensions */
	public float width
	{ get; set; }
	public float height
	{ get; set; }
	
	/* Default Constructor */
	public HUDLabel() {
		
		this.label = "Uninitialized";
		this.text = "Uninitialized";
		
		this.position = new Vector2(0.0f,0.0f);
		
		this.width = 0.0f;
		this.height = 0.0f;
	}
	
	public void Draw() {
		
		/* Get the Style Manager */
		StyleContainer styleManager = StyleContainer.Instance();
		
		/* Draw the Bar Label */
		GUIStyle style = styleManager.GetStyle("Black Label");
		
		GUI.TextField(new Rect(position.x + 10, position.y - 20, width, height * 0.75f), label, style);
		GUI.TextField(new Rect(position.x, position.y, width, height), text, style);	
	}
}