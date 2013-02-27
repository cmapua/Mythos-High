using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	public GUIStyle newgame_btn, options_btn, exit_btn;
	
	void OnGUI () {
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2),150,(Screen.height/6)), "", newgame_btn)) {
			Application.LoadLevel ("dialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+(Screen.height/6),150,(Screen.height/6)), "", options_btn)) {
			
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+(Screen.height/3),150,(Screen.height/6)), "", exit_btn)) {
			Application.Quit();
		}
	}
	void Update(){
		
	}
}