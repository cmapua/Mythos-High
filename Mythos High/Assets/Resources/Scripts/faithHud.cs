using UnityEngine;
using System.Collections;

public class faithHud : MonoBehaviour {
	
	private int maxFaith = 150;
	public int shrineLevel = 1;
	public float currentFaith = 0;
	private float resourceGatherRate = .55f;
	private int xOffset = 0, yOffset = 0;
	private bool hideHelp = false;
	private Unit playerCastle, aiCastle;
	private static faithHud instance;
	private static UnitManager manager;
	public Texture2D helpBoxBG;
	
	protected void searchCastle() {
		foreach(Unit u in manager.getTheirUnits()) {
			if(u.name=="enemyShrine"){
				aiCastle=u;
				print("Enemy Castle Found!");
			}
		}
		foreach(Unit u in manager.getYourUnits()) {
			if(u.name=="playerShrine"){
				playerCastle=u;

				print("Player Castle Found!");			}
		}
	}

	protected int checkVictory(){
		if(aiCastle == null || playerCastle == null){
			searchCastle();
		}
		else if(aiCastle.HP <= 0){
			return 1;
		}
		else if(playerCastle.HP <= 0){
			return 2;
		}
		return 0;
	}

	public static faithHud getInstance() {
		if(instance == null) 
			instance = (faithHud)FindObjectOfType(typeof(faithHud));
		return instance;
	}
	
	// Use this for initialization
	void Start(){
		manager = UnitManager.getInstance();
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
		switch(checkVictory()){
			case 0:
				break;
			case 1:
				Time.timeScale=0;
				displayVictory (1);
				break;
			case 2:
				Time.timeScale=0;
				displayVictory (2);
				break;
		}
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

	void displayVictory(int winner){
		Rect victoryBox = new Rect(Screen.width*1/4, Screen.height*1/4, Screen.width*3/4,Screen.height*3/4);
        GUI.DrawTexture(victoryBox, helpBoxBG, ScaleMode.StretchToFill, true, 10f);
		string victoryText = "";
		switch (winner){
			case 1:
				victoryText = "Player Won!";
				break;
			case 2:
				victoryText = "Enemy Won! :(";
			break;
		}
        GUI.Label(new Rect(Screen.width*1/4+30, Screen.height*1/4+30, Screen.width*3/4-30,Screen.height*3/4-30), victoryText);
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