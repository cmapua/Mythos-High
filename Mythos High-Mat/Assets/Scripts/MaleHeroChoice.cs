using UnityEngine;
using System.Collections;

public class MaleHeroChoice : MonoBehaviour {

	public GUIStyle customButton;
	
	void OnGUI () {
		if (GUI.Button (new Rect ((Screen.width/2)-175,(Screen.height/2),150,100), "Anansi")) 
		{
			Application.LoadLevel ("");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-175,(Screen.height/2)+100,150,100), "Aphrodite")) 
		{
			Application.LoadLevel ("");
		}
		if (GUI.Button (new Rect ((Screen.width/2)-175,(Screen.height/2)+200,150,100), "Buddha")) 
		{
			Application.LoadLevel ("");
		}
			if (GUI.Button (new Rect ((Screen.width/2)+25,(Screen.height/2),150,100), "Osiris")) 
		{ 
			Application.LoadLevel ("");
		}
		if (GUI.Button (new Rect ((Screen.width/2)+25,(Screen.height/2)+100,150,100), "Shiva")) 
		{
			Application.LoadLevel ("");
		}
		if (GUI.Button (new Rect ((Screen.width/2)+25,(Screen.height/2)+200,150,100), "Thor")) 
		{
			Application.LoadLevel ("");
		}
	}
}