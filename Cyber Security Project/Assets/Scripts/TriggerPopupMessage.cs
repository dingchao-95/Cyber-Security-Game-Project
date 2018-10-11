/// <summary>
/// Popup Note Script
///
/// Attach to a trigger object in the game.
/// You can use the graphic provided, please give credit to the author:
/// Shadows of Elear - Deviantart.com
///
/// You may use this script for commercial purposes
///
/// http://lostmystic.com
/// Written in Unity 4.3.1
/// </summary>
using UnityEngine;
using System.Collections;

public class TriggerPopupMessage : MonoBehaviour {
	
	public string Text;
	public string LevelName;
	public GUISkin guiskin;
	public Texture2D PageTexture;
	
	public GUIStyle myGuiStyle = new GUIStyle();
	
	// If true, will only show once
	// If false, will show every time trigger area is entered
	public bool bShowOnlyOnce = false;
	
	// Show button at bottom of screen
	public bool bShowButton = true;
	
	private bool bShowNote = false;
	private Rect rectBmpBg;
	private Rect rectText;
	private Rect rectBtn;
	
	
	
	void Start() {
		
		if (myGuiStyle == null) {
			myGuiStyle.wordWrap = true;
		}
		
		rectBmpBg = MakeRectByPercent(0.7f,0.8f,TextAnchor.MiddleCenter);
		rectText = MakeRectByPercent(0.65f,0.45f,TextAnchor.MiddleCenter);
		rectBtn = new Rect(rectText.x+rectText.width/2-40,rectText.y+rectText.height-25,80,20);
		
	}

	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D target) {

		if (target.tag == "Player")
		{
			bShowNote = true;
			AudioSource mySfx = GetComponent<AudioSource>() as AudioSource;
			if (mySfx != null) audio.Play();
		}

	}
	
	void OnGUI() {
		
		if (!bShowNote) return;
		
		GUI.skin = guiskin;
		
		// If you haven't added a page texture
		if (PageTexture == null) {
			GUI.Box(rectBmpBg,"");
		} else
			GUI.DrawTextureWithTexCoords(rectBmpBg,PageTexture,new Rect(0,0.6f,1,0.4f));
		
		GUI.Box(rectText, Text,myGuiStyle);
		
		// If escape, or enter or left click
		if (Input.GetKeyDown(KeyCode.Return)) bShowNote = false;
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) bShowNote = false;
		if (Input.GetKeyDown(KeyCode.Escape)) bShowNote = false;

		
		if (bShowButton) {
			
			if (GUI.Button(rectBtn, "Continue") )
			{
				LevelManager.Instance.GoToNextLevel(LevelName);
				bShowNote = false;
			}
			
		} else {
			if (Input.GetMouseButtonDown(0)) bShowNote = false;
		}
		
	}
	public static Rect MakeRectByPercent(float x, float y, TextAnchor position) {
		
		
		if ((x > 1) || (y > 1)) {
			return new Rect(0,0,Screen.width,Screen.height);
		}
		
		x = Screen.width * x;
		y = Screen.height * y;
		
		return MakeRectByPixel(x,y,position);
	}
	
	
	// Returns a rectangle centered on the screen according to the positions below
	public static Rect MakeRectByPixel(float x, float y, TextAnchor position) {
		
		if (position == TextAnchor.MiddleCenter) {
			return new Rect(Screen.width/2-(x/2),Screen.height/2-(y/2),x,y);
		}
		
		if (position == TextAnchor.UpperCenter) {
			return new Rect(Screen.width/2-(x/2),0,x,y);
		}
		
		if (position == TextAnchor.LowerCenter) {
			return new Rect(Screen.width/2-(x/2),Screen.height-y,x,y);
		}
		
		if (position == TextAnchor.UpperLeft) {
			return new Rect(0,0,x,y);
		}
		
		if (position == TextAnchor.MiddleLeft) {
			return new Rect(0,Screen.height/2-(y/2),x,y);
		}
		
		if (position == TextAnchor.LowerLeft) {
			return new Rect(0,Screen.height-y,x,y);
		}
		
		if (position == TextAnchor.UpperRight) {
			return new Rect(Screen.width-x,0,x,y);
		}
		
		if (position == TextAnchor.MiddleRight) {
			return new Rect(Screen.width-x,Screen.height/2-(y/2),x,y);
		}
		
		if (position == TextAnchor.LowerRight) {
			return new Rect(Screen.width-x,Screen.height-y,x,y);
		}
		
		return new Rect(0,0,0,0);
	}
}