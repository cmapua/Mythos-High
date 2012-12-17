using UnityEngine;
using System.Collections;

public class Slideshow : MonoBehaviour {
	
	public GUITexture gt;
	public Texture2D[] slides;
	public int currSlide = 0;
	public float x, y, w = 400, h = 40;
	
	// Use this for initialization
	void Start () {
		x = Screen.width - ( w + 10 );
		y = Screen.height - ( h + 10 );
		gt.texture = slides[currSlide];
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(x, y, w, h), "I get it, on with the battle!")) {
			Application.LoadLevel("hello3");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.KeypadEnter)) {
			if(currSlide < slides.Length-1) {
				gt.texture = slides[currSlide+=1];
			}
		}
		if(Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Backspace)) {
			if(currSlide > 0) {
				gt.texture = slides[currSlide-=1];
			}
		}
	}
}
