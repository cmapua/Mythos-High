using UnityEngine;
using System.Collections;

public class faithHud : MonoBehaviour {
	
	private int maxFaith = 150;
	public int shrineLevel = 1;
	public float currentFaith = 0;
	private float resourceGatherRate = .55f;
	protected int xOffset = 0, yOffset = 0;
	private bool hideHelp = false;
	private bool dialogue = true;
	private Unit playerCastle, aiCastle, playerHero;
	private static faithHud instance;
	private static UnitManager manager;
	public Texture2D helpBoxBG;
	
	public void dialogueOver(){
		dialogue = false;
	}
	
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

				print("Player Castle Found!");
			}
			if(u.name=="Hero"){
				playerHero=u;

				print("Player Hero Found!");
			}
		}
	}

	protected int checkVictory(){
		if(aiCastle == null || playerCastle == null || playerHero == null){
			searchCastle();
		}
		else if(aiCastle.HP <= 0){
			return 1;
		}
		else if(playerCastle.HP <= 0){
			return 2;
		}else if (playerHero.HP <= 0){
			return 2;
		}
		//AI Shrine
		if(aiCastle.HP<= aiCastle.maxHP*.25){
			aiCastle.getSpriteControl().sprite.PlayLoop("quarter-health");
		}
		else if(aiCastle.HP<= aiCastle.maxHP*.5){
			aiCastle.getSpriteControl().sprite.PlayLoop("half-health");
		}
		else if(aiCastle.HP<= aiCastle.maxHP*.75){
			aiCastle.getSpriteControl().sprite.PlayLoop("3/4-health");
		}
		//PLAYER Shrine
		if(playerCastle.HP<= playerCastle.maxHP*.25){
			playerCastle.getSpriteControl().sprite.PlayLoop("quarter-health");
		}
		else if(playerCastle.HP<= playerCastle.maxHP*.5){
			playerCastle.getSpriteControl().sprite.PlayLoop("half-health");
		}
		else if(playerCastle.HP<= playerCastle.maxHP*.75){
			playerCastle.getSpriteControl().sprite.PlayLoop("3/4-health");
		}
		return 0;
	}

	public static faithHud getInstance() {
		if(instance == null) 
			instance = (faithHud)FindObjectOfType(typeof(faithHud));
		return instance;
	}
	
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
		yield return StartCoroutine(resourceIncrement());
	}
	
	
	void OnGUI() {
		if(!dialogue){
			displayResource();
			displayHelp();
			switch(checkVictory()){
				case 0:
					break;
				case 1:
					displayVictory (1);
					break;
				case 2:
					displayVictory (2);
					break;
			}
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
		int hallo = Screen.height*5/8 + yOffset;
		if(dialogue){
			hallo = -(Screen.height*3/8);
		}
        Rect HelpBox = new Rect(0 + xOffset,hallo , Screen.width,Screen.height*3/8);
        GUI.DrawTexture(HelpBox, helpBoxBG, ScaleMode.StretchToFill, true, 10f);
		string helpText = "Help (Press 'H' to remove)" +
						  "\n'R' = Upgrade Shrine(80,120,150)" +
						  "\n'Q' = Summon Swordsman (20)" +
						  "\n'W' = Summon Archer (35)" +
						  "\n'E' = Summon Mage(50)";
        GUI.Label(new Rect(10, hallo + 10, Screen.width-10,(Screen.height*3/8)-10), helpText);
	}

	void displayVictory(int winner){
		gameGUI.getInstance().dialogue = true;
		yOffset = -Screen.height;
		Rect victoryBox = new Rect(Screen.width*1/4, Screen.height*1/4, Screen.width*1/2,Screen.height*1/2);
//        GUI.DrawTexture(victoryBox, helpBoxBG, ScaleMode.StretchToFill, true, 10f);
		string victoryText = "";
		switch (winner){
			case 1:
				victoryText = 	"Player Won!" +
								"\n Click me to restart battle!" +
								"\n Enemy Spawn behaviour will be randomized";
			
				break;
			case 2:
				victoryText = "Enemy Won! :(" +
								"\n Click me to restart battle!" +
								"\n Enemy Spawn behaviour will be randomized";
			break;
		}
		Time.timeScale=0;
		if(GUI.Button(victoryBox, victoryText)){
			if(MinionSpawner.getInstance().level==1)
				Application.LoadLevel("hello4");
			else if(MinionSpawner.getInstance().level==2)
				Application.LoadLevel("hello5");

		}

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
	
	public int getYOffset(){
		return yOffset;
	}

}