using UnityEngine;
using System.Collections;

public class MaleFirstQuestion : MonoBehaviour {

	public GUIStyle customButton;
	public GUIStyle customStyle;
		
	void OnGUI () {
		GUI.Label(new Rect(75,50,400,100), 
		"Please check the circle of your choice",customStyle);
		GUI.Label(new Rect(75,100,400,100), 
		"You're getting a midnight snack, when suddenly someone breaks into your house. What do you do?",customStyle);
		
		if (GUI.Button (new Rect (75,200,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("MaleSecondQuestionB");
		}
		GUI.Label(new Rect(200,240,400,100), 
		"A. Offer him a cup of tea, and try to change his life",customStyle);
		
		if (GUI.Button (new Rect (75,400,100,100), 
		"",customButton))
			
		{
			Application.LoadLevel ("MaleSecondQuestionA");
		}
		GUI.Label(new Rect(200,440,400,100), 
		"B. Grab my baseball bat and show him who's boss",customStyle);
		
	}
}