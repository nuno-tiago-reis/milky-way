using UnityEngine;

using System.Collections.Generic;

public class StyleContainer {
	
	/* Singleton Instance */
	private static StyleContainer instance = null;
	
	/* GUIStyle Dictionary */
	private Dictionary<string,GUIStyle> styleMap
	{ get; set; }
	
	/* Default Construtor - Initializes the Default styles */
	private StyleContainer() {
		
		/* Initialize the Dictionary */
		styleMap = new Dictionary<string, GUIStyle>();
		
		/* Black Label Style */
		Texture2D blackTexture = new Texture2D(1, 1);
		blackTexture.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f));
		blackTexture.Apply();
		
		GUIStyle blackLabel = new GUIStyle();
		
		blackLabel.alignment = TextAnchor.MiddleCenter;
		blackLabel.fontSize = 14;
		blackLabel.fontStyle = FontStyle.Bold;
		blackLabel.normal.textColor = Color.white;
		
		blackLabel.normal.background = blackTexture;
		blackLabel.active.background = blackTexture;
		
		styleMap.Add("Black Label", blackLabel);
		
		/* Transparent Label Style */
		GUIStyle transparentLabel = new GUIStyle();
		
		transparentLabel.alignment = TextAnchor.MiddleCenter;
		transparentLabel.fontSize = 16;
		transparentLabel.fontStyle = FontStyle.Bold;
		transparentLabel.normal.textColor = Color.white;
		
		transparentLabel.normal.background = null;
		transparentLabel.active.background = null;
		
		transparentLabel.hover.background = null;
		transparentLabel.onHover.background = null;
		
		transparentLabel.focused.background = null;
		transparentLabel.onFocused.background = null;
		
		styleMap.Add("Transparent Label", transparentLabel);
		
		/* Red Bar Style */
		Texture2D redTexture = new Texture2D(1, 1);
		redTexture.SetPixel(0, 0, new Color(0.75f, 0.0f, 0.0f));
		redTexture.Apply();
		
		GUIStyle redBar = new GUIStyle();
		
		redBar.normal.background = redTexture;
		redBar.alignment = TextAnchor.MiddleCenter;
		
		styleMap.Add("Red Bar", redBar);
		
		/* Yellow Bar Style */
		Texture2D yellowTexture = new Texture2D(1, 1);
		yellowTexture.SetPixel(0, 0, new Color(0.75f, 0.75f, 0.0f));
		yellowTexture.Apply();
		
		GUIStyle yellowBar = new GUIStyle();
		
		yellowBar.normal.background = yellowTexture;
		yellowBar.alignment = TextAnchor.MiddleCenter;
		
		styleMap.Add("Yellow Bar", yellowBar);
		
		/* Green Bar Style */
		Texture2D greenTexture = new Texture2D(1, 1);
		greenTexture.SetPixel(0, 0, new Color(0.0f, 0.75f, 0.0f));
		greenTexture.Apply();
		
		GUIStyle greenBar = new GUIStyle();
		
		greenBar.normal.background = greenTexture;
		greenBar.alignment = TextAnchor.MiddleCenter;
		
		styleMap.Add("Green Bar", greenBar);
		
		/* Black Bar Style */		
		GUIStyle blackBar = new GUIStyle();
		
		blackBar.normal.background = blackTexture;
		blackBar.alignment = TextAnchor.MiddleCenter;
		
		styleMap.Add("Black Bar", blackBar);
		
		/* Gray Bar Style */
		Texture2D grayTexture = new Texture2D(1, 1);
		grayTexture.SetPixel(0, 0, new Color(0.95f, 0.95f, 0.95f));
		grayTexture.Apply();
		
		GUIStyle grayBar = new GUIStyle();
		
		grayBar.normal.background = grayTexture;
		grayBar.alignment = TextAnchor.MiddleCenter;
		
		styleMap.Add("Gray Bar", grayBar);
		
		/* Minimap Style */
		Texture2D borderTexture = (Texture2D)Resources.Load("Textures/Minimap",typeof(Texture2D)) as Texture2D;
		
		GUIStyle minimapBox = new GUIStyle();
		minimapBox.normal.background = borderTexture;
		
		styleMap.Add("Minimap Box", minimapBox);
		
		/* Crosshair Style */
		Texture2D crossHairTexture = (Texture2D)Resources.Load("Textures/Crosshair",typeof(Texture2D)) as Texture2D;
		
		GUIStyle crosshair = new GUIStyle();
		crosshair.normal.background = crossHairTexture;
		
		styleMap.Add("Crosshair", minimapBox);
	}
	
	/* Singleton Instance Getter */
	public static StyleContainer Instance() {
		
		if(instance == null)
			instance = new StyleContainer();
		
		return instance;
	}
	
	public void AddStyle(string key, GUIStyle style) {
		
		if(styleMap.ContainsKey(key) == false)
			styleMap.Add(key,style);
	}
	
	public void RemoveStyle(string key, GUIStyle style) {
		
		if(styleMap.ContainsKey(key) == true)
			styleMap.Remove(key);
	}
	
	public GUIStyle GetStyle(string key) {
		
		if(styleMap.ContainsKey(key) == true)
			return styleMap[key];
		
		return null;
	}
}