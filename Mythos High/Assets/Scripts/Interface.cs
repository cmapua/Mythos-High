using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2),150,100), "Start Game")) {
			 

			Application.LoadLevel ("dialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,150,100), "Options")) {
			
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,150,100), "Exit")) {
			Application.Quit();
		}
	}
}