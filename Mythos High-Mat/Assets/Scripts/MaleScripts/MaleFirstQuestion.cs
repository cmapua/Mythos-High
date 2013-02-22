using UnityEngine;
using System.Collections;

public class MaleFirstQuestion : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		 GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2),400,100), "You're getting a midnight snack, when suddenly someone breaks into your house. What do you do?");
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+100,400,100), "A. Offer him a cup of tea, and try to change his life")) {
			Application.LoadLevel ("MaleSecondQuestionB");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-75,(Screen.height/2)+200,400,100), "B. Grab my baseball bat and show him who's boss")) {
			Application.LoadLevel ("MaleSecondQuestionA");
		}
	}
}