using UnityEngine;
using System.Collections;

public class FemaleAphroditeOsiris : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "Choose Wisely!!!");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A) Hey, the Bangle is pretty.")) {
			Application.LoadLevel ("FemaleAphroditeDialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B) I’ll go with the Crook. It’s more sensible.")) {
			Application.LoadLevel ("FemaleOsirisDialogue");
		}
	}
}