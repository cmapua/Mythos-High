using UnityEngine;
using System.Collections;

public class MaleSecondQuestionA : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "A foreigner starts talking to you on the street, but you have no clue what he's saying. How do you reply?");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A. No speak Engrish.")) {
			Application.LoadLevel ("MaleAnansiBuddha");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B. Try communicating in sign language.")) {
			Application.LoadLevel ("MaleAphroditeOsiris");
		}
	}
}