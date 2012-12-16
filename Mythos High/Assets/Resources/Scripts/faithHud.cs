using UnityEngine;
using System.Collections;

public class faithHud : MonoBehaviour {
	
	private int maxFaith = 150;
	public int shrineLevel = 1;
	public float currentFaith = 0;
	private float resourceGatherRate = .55f;
	private int xOffset = 0, yOffset = 0;
	private bool hideHelp = false;
	
	private static faithHud instance;
	public Texture2D helpBoxBG;
	
	public static faithHud getInstance() {
		if(instance == null) 
			instance = (faithHud)FindObjectOfType(typeof(faithHud));
		return instance;
	}
	
	// Use this for initialization
	void Start(){
		StartCoroutine("CoStart");
	}
	IEnumerator CoStart() {
		print ("CoStart() called.");
		while(true) {
			yield return StartCoroutine(CoUpdate());
		}
	}
	
	IEnumerator CoUpdate() {
		//print ("CoUpdate() called.");
		yield return StartCoroutine(resourceIncrement());
	}
	
	
	void OnGUI() {
		displayResource();
		displayHelp();
	}
	
	void Update() {
		if(Input.GetKeyUp(KeyCode.H)) {
			hideHelp = true;
		}
		if(hideHelp) {
			if(yOffset <= 300)
				yOffset++;
		}
	}
	
	void displayHelp(){
        Rect HelpBox = new Rect(0 + xOffset, Screen.height*5/8 + yOffset, Screen.width,Screen.height*3/8);
        GUI.DrawTexture(HelpBox, helpBoxBG, ScaleMode.StretchToFill, true, 10f);
		string helpText = "Help:" +
						  "\nQ = Upgrade Shrine(80,120,150)" +
						  "\nA = Summon Archer (35)" +
						  "\nS = Summon Swordsman (20)" +
						  "\nD = Summon Mage(50)";
        GUI.Label(new Rect(10, (Screen.height*5/8)+10, Screen.width-10,(Screen.height*3/8)-10), helpText);

	}
	
	IEnumerator resourceIncrement(){
		if(currentFaith<=maxFaith){
			currentFaith++;
		}
		yield return new WaitForSeconds(resourceGatherRate);
	}
	void displayResource(){
		GUI.Label(new Rect(10, 10, 100, 50), "Faith:	"+(int)currentFaith);
	}
	
	public void levelShrine(){
		shrineLevel++;
		if(shrineLevel==2){
			resourceGatherRate = .45f;
			maxFaith = 200;
			currentFaith -=80;
		}
		if(shrineLevel==3){
			resourceGatherRate = .2f;
			maxFaith = 250;
			currentFaith -=120;
		}
		if(shrineLevel==4){
			resourceGatherRate = .15f;
			maxFaith = 300;
			currentFaith -=150;
		}
	}
}