using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public GUIStyle startButton;
	public GUIStyle exitButton;
	
	void OnGUI () {
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2),150,100), "", startButton)) {
			 

			Application.LoadLevel ("Opening Dialog");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,150,100), "", exitButton)) {
			Application.Quit();
		}
	}
}