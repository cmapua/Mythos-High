using UnityEngine;
using System.Collections;

public class PrepareBattle1 : MonoBehaviour {

	public GUIStyle customButton;

	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),150,100), "What's Your Gender?");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,150,100), "Male")) {
			Application.LoadLevel ("MaleDialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,150,100), "Female")) {
			Application.LoadLevel ("FemaleDialogue");
		}
	}
	
}
