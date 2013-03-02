using UnityEngine;
using System.Collections;

public class MaleAphroditeOsiris : MonoBehaviour {

	public GUIStyle AphroditeItem;
	public GUIStyle OsirisItem;
	public GUIStyle Aphrodite;
	public GUIStyle Osiris;
	
	void OnGUI () {

		GUI.Label(new Rect((Screen.width/2)-75,(Screen.height/2)-400,400,100), "Choose Wisely!!!");
		GUI.Label(new Rect((Screen.width/2)-350,(Screen.height/2)-200,200,400), "",Aphrodite);
		if (GUI.Button (new Rect ((Screen.width/2)-150,(Screen.height/2)-200,200,400), 
			"",AphroditeItem)) 
		{
			Application.LoadLevel ("MaleAphroditeDialogue1");
		}
		GUI.Label(new Rect((Screen.width/2)+350,(Screen.height/2)-200,200,400), "",Osiris);
		if (GUI.Button (new Rect ((Screen.width/2)+150,(Screen.height/2)-200,200,400),
			"",OsirisItem)) 
		{
			Application.LoadLevel ("MaleOsirisDialogue");
		}
		GUI.Label(new Rect((Screen.width/2)-200,(Screen.height/2)+300,400,100), 
			"Hey, the Bangle is pretty.");
		GUI.Label(new Rect((Screen.width/2)+200,(Screen.height/2)+300,400,100), 
			"Ill go with the Crook. Its more sensible.");
	}
}

