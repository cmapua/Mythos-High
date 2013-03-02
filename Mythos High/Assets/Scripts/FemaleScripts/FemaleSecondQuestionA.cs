using UnityEngine;
using System.Collections;

public class FemaleSecondQuestionA : MonoBehaviour {

	public GUIStyle customButton;
	public GUIStyle customStyle;
		
	void OnGUI () {
		GUI.Label(new Rect(75,50,400,100), 
		"Please check the circle of your choice",customStyle);
		GUI.Label(new Rect(75,100,400,100), 
		"A foreigner starts talking to you on the street, but you have no clue what he's saying. How do you reply?",customStyle);
		
		if (GUI.Button (new Rect (75,200,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("FemaleAnansiBuddha");
		}
		GUI.Label(new Rect(200,240,400,100), 
		"A. No speak Engrish.",customStyle);
		
		if (GUI.Button (new Rect (75,400,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("FemaleAphroditeOsiris");
		}
		GUI.Label(new Rect(200,440,400,100), 
		"B. Try communicating in sign language.",customStyle);
		
	}
	
}