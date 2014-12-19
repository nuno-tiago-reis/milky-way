using UnityEngine;

public class HUDBar {
	
	/* Bar Label */
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
	
	/* Bar fill amount */
	public float amount
	{ get; set; }
	/* Bar fill maximum amount */
	public float maximumAmount
	{ get; set; }
	/* Bar fill minimum amount */
	public float minimumAmount
	{ get; set; } 
	
	public GUIStyle defaultStyle
	{ get; set; } 

	public bool textActive 
	{ get; set; } 	

	/* Default Constructor */
	public HUDBar() {
		
		this.text = "Uninitialized";

		this.textActive = true;
		
		this.position = new Vector2(0.0f,0.0f);
		
		this.width = 0.0f;
		this.height = 0.0f;
		
		this.amount = 0.0f;
		this.maximumAmount = 0.0f;
		this.minimumAmount = 0.0f;
	}
	
	public void Draw() {
		
		/* Get the Style Manager */
		StyleContainer styleManager = StyleContainer.Instance();
		
		/* Calculate the bars current fill percentage */
		float percentage = Mathf.Clamp(amount / maximumAmount,0.0f,1.0f);
		
		if(maximumAmount == 0)
			percentage = 1.0f;
		
		/* Draw the background */
		GUIStyle style = styleManager.GetStyle("Black Bar");
		
		GUI.Box(new Rect(position.x, position.y, width, height), "", style);
		
		/* Draw the foreground */
		if(percentage < 0.33f)
			style = styleManager.GetStyle("Red Bar");
		else if(percentage < 0.66f)
			style = styleManager.GetStyle("Yellow Bar");
		else 
			style = styleManager.GetStyle("Green Bar");
		
		GUI.Box(new Rect(position.x + 5.0f, position.y + 2.0f, (width - 10.0f) * percentage, height - 4.0f), "", style);
		
		/* Draw the Bar Label */
		if(textActive == true) {

			style = styleManager.GetStyle("Black Label");
			
			GUI.TextField(new Rect(position.x + 10, position.y - 20, width * 0.5f, height * 0.75f), text, style);
			
			/* Draw the Text Overlay */
			style = styleManager.GetStyle("Transparent Label");

			if(this.maximumAmount != 0.0f)
				GUI.TextField(new Rect(position.x + 5.0f, position.y + 2.0f, (width - 10.0f), height - 5.0f), amount.ToString("0.0") + "/" + maximumAmount.ToString("0.0"), style);	
			else
				GUI.TextField(new Rect(position.x + 5.0f, position.y + 2.0f, (width - 10.0f), height - 5.0f), amount.ToString("F0"), style);	
		}
	}
}