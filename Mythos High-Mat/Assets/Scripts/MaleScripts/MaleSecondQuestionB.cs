using UnityEngine;
using System.Collections;

public class MaleSecondQuestionB : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "You are locked inside a pitch-black room! What do you do?");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A. Kick down the door. Like a boss.")) {
			Application.LoadLevel ("MaleThorShiva");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B. Sit in the dark, and wait.")) {
			Application.LoadLevel ("MaleAnansiBuddha");
		}
	}
}