using UnityEngine;
using System.Collections;

public class Slideshow : MonoBehaviour {
	
	public Texture2D[] slides;
	public int currSlide = 0;
	bool isEnabled = true;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	void OnGUI() {
		if(isEnabled) {
            float grpWidth = Screen.width * 0.6f; //1024
            float grpHeight = Screen.height * 0.6f; //600 
            GUI.BeginGroup(new Rect(Screen.width/2 - grpWidth/2, Screen.height/2 - grpHeight/2, grpWidth, grpHeight));

            //display slides
            GUI.DrawTexture(new Rect(0, 0, 1024, 512), slides[currSlide]);

			if(GUI.Button(new Rect(grpWidth-100, grpHeight-35, 100, 35), "I get it, on with the battle!")) {
				//Application.LoadLevel("hello3");
				Time.timeScale = 1;
				isEnabled = false;
				faithHud.getInstance().dialogueOver();
				gameGUI.getInstance().dialogue = false;
				MinionSpawner.getInstance().dialoguePlaying = false;
			}

            GUI.EndGroup();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isEnabled) {
			if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.KeypadEnter)) {
				if(currSlide < slides.Length-1) {
					currSlide+=1;
				}
			}
			if(Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Backspace)) {
				if(currSlide > 0) {
					currSlide-=1;
				}
			}
		}
	}
}
