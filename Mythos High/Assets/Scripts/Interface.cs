using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2),150,(Screen.height/6)), "Start Game")) {
			Application.LoadLevel ("dialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+(Screen.height/6),150,(Screen.height/6)), "Options")) {
			
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+(Screen.height/3),150,(Screen.height/6)), "Exit")) {
			Application.Quit();
		}
	}
	void Update(){
		
	}
}