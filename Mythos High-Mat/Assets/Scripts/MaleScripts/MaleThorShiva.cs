using UnityEngine;
using System.Collections;

public class MaleThorShiva : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "Choose Wisely!!!");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A) Woah! That Hammer looks cool.")) {
			Application.LoadLevel ("MaleThorDialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B) I choose the Trident. Looks interesting.")) {
			Application.LoadLevel ("MaleShivaDialogue");
		}
	}
}
