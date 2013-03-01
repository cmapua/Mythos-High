using UnityEngine;
using System.Collections;

public class FemaleAnansiBuddha : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "Choose Wisely!!!");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A) I’ll take the Cloak.")) {
			Application.LoadLevel ("FemaleAnansiDialogue");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B) I’ll have the Prayer Beads.")) {
			Application.LoadLevel ("FemaleBuddhaDialogue");
		}
	}
}