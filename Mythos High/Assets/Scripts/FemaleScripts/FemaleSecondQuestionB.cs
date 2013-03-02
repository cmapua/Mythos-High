using UnityEngine;
using System.Collections;

public class FemaleSecondQuestionB : MonoBehaviour {
	
	public GUIStyle customButton;
	public GUIStyle customStyle;
		
	void OnGUI () {
		GUI.Label(new Rect(75,50,400,100), 
		"Please check the circle of your choice",customStyle);
		GUI.Label(new Rect(75,100,400,100), 
		"You are locked inside a pitch-black room! What do you do?",customStyle);
		
		if (GUI.Button (new Rect (75,200,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("FemaleThorShiva");
		}
		GUI.Label(new Rect(200,240,400,100), 
		"A. Kick down the door. Like a boss.",customStyle);
		
		if (GUI.Button (new Rect (75,400,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("FemaleAnansiBuddha");
		}
		GUI.Label(new Rect(200,440,400,100), 
		"B. Sit in the dark, and wait.",customStyle);
		
	}
}