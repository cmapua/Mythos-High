using UnityEngine;
using System.Collections;

public class Slideshow : MonoBehaviour {
	
	public GUITexture gt;
	public Texture2D[] slides;
	public int currSlide = 0;
	public float x, y, w = 400, h = 40;
	bool isEnabled = true;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		x = Screen.width - ( w + 10 );
		y = Screen.height - ( h + 10 );
		gt.texture = slides[currSlide];
	}
	
	void OnGUI() {
		if(isEnabled) {
			if(GUI.Button(new Rect(x, y, w, h), "I get it, on with the battle!")) {
				//Application.LoadLevel("hello3");
				Time.timeScale = 1;
				isEnabled = false;
				faithHud.getInstance().dialogueOver();
				gameGUI.getInstance().dialogue = false;
				MinionSpawner.getInstance().dialoguePlaying = false;
				DestroyObject(gt);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isEnabled) {
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
}
