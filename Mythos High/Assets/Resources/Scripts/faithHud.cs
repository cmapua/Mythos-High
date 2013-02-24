using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    //textures for HUD
    public Texture2D helpBoxBG, shieldIcon, 
        lv1bar, lv2bar, lv3bar, lv4bar, 
        lv1barBack, lv2barBack, lv3barBack, lv4barBack;

    //for unit selection window
    public float unitYOffset = 30;
    [HideInInspector]
    public Unit selectedUnit;
    public CameraLookat cam;
	
	public void dialogueOver(){
		dialogue = false;
	}

    public bool toggleUnitSelection = false;
    private float mover = 0;

    void displayUnitSelectionWindow()
    {
        GUI.BeginGroup(new Rect(20, Screen.height/2, 500, 5 * unitYOffset));
        GUI.Box(new Rect(0, 2 * unitYOffset, 200, unitYOffset), "Select target: >");
        generateButtons(manager.getYourUnits(), 210, 0, false, mover);

        GUI.EndGroup();
    }

    void generateButtons(List<Unit> units, float x, float y, bool easing, float direction)
    {
        //float yOffset = 20f;
        GUI.BeginGroup(new Rect(x, y, 200, 5 * unitYOffset)); //show at most 5 units at a time

        Color guiColor = GUI.color;

        for (int i = 0; i < units.Count; i++)
        {
            float yValue = (unitYOffset * i) + direction;
            
            if (yValue == 0 || yValue == (4 * unitYOffset)) guiColor.a = .25f;
            if (yValue == (1 * unitYOffset) || yValue == (3 * unitYOffset)) guiColor.a = .5f;
            if (yValue == (2 * unitYOffset))
            {
                guiColor.a = 1f;
                selectedUnit = units[i];
            }
            GUI.color = guiColor;

            GUI.Box(new Rect(0, yValue, 200, unitYOffset), units[i].name);
        }

        //reset alpha
        guiColor.a = 1f;
        GUI.color = guiColor;

        GUI.EndGroup();
    }

	protected void searchCastle() {
		foreach(Unit u in manager.getTheirUnits()) {
			if(u.name=="enemyShrine"){
				aiCastle=u;
				//print("Enemy Castle Found!");
			}
		}
		foreach(Unit u in manager.getYourUnits()) {
			if(u.name=="playerShrine"){
				playerCastle=u;

				//print("Player Castle Found!");
			}
			if(u.name=="Hero"){
				playerHero=u;

				//print("Player Hero Found!");
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
		//print ("CoStart() called.");
		while(true) {
			yield return StartCoroutine(CoUpdate());
		}
	}
	
	IEnumerator CoUpdate() {
		yield return StartCoroutine(resourceIncrement());
	}
	
	
	void OnGUI() {
		if(!dialogue){
            if (toggleUnitSelection)
            {
                displayUnitSelectionWindow();
                //print(">>>>>>>>> display selection window called");
            }

			displayResource();
			//displayHelp();
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

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (toggleUnitSelection)
            {
                Time.timeScale = 1;
                toggleUnitSelection = false;
            }
            else
            {
                Time.timeScale = 0;
                toggleUnitSelection = true;
            }
        }
        if (toggleUnitSelection)
        {
            if (selectedUnit)
            {
                if (selectedUnit.isSprite)
                    cam.target = selectedUnit.sc.sprite.transform;
                else cam.target = selectedUnit.getUnitTransform();
            }
            else cam.target = cam.defaultTarget;
            if (Input.GetKeyUp(KeyCode.UpArrow)) mover += unitYOffset;
            if (Input.GetKeyUp(KeyCode.DownArrow)) mover += -unitYOffset;
            //else mover = 0;
        }
        else cam.target = cam.defaultTarget;
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

    //
    public static Rect screenRect(float x, float y, float w, float h)
    {
        return new Rect(Screen.width - x, Screen.height - y, w, h);
    }

    public static void draw(Texture2D icon, float x, float y)
    {
        GUI.DrawTexture(new Rect(x, y, icon.width, icon.height), icon);
    }

	void displayResource(){
        GUI.BeginGroup(new Rect(10, 10, 800, 100));
        
        float lv1width = lv1bar.width * (currentFaith / 150f);
        float lv2width = lv2bar.width * ((currentFaith - 150) / 200f);
        float lv3width = lv3bar.width * ((currentFaith - 200) / 250f);
        float lv4width = lv4bar.width * ((currentFaith - 250) / 300f);

        if (shrineLevel >= 4)
        {
            faithHud.draw(lv4barBack, lv1barBack.width + lv2barBack.width + lv3barBack.width + 64, 10);
            GUI.DrawTexture(new Rect(lv1barBack.width + lv2barBack.width + lv3barBack.width + 64, 10, lv4width, lv4bar.height), lv4bar);
        }
        if (shrineLevel >= 3)
        {
            faithHud.draw(lv3barBack, lv1barBack.width + lv2barBack.width + 64, 10);
            GUI.DrawTexture(new Rect(lv1barBack.width + lv2barBack.width + 64, 10, lv3width, lv3bar.height), lv3bar);
        }
        if (shrineLevel >= 2)
        {
            faithHud.draw(lv2barBack, lv1barBack.width + 64, 10);
            GUI.DrawTexture(new Rect(lv1barBack.width + 64, 10, lv2width, lv2bar.height), lv2bar);
        }

        faithHud.draw(lv1barBack, 64, 10);
        GUI.DrawTexture(new Rect(64, 10, lv1width, lv1bar.height), lv1bar);

		GUI.Label(new Rect(100, 10, 100, 50), "Faith:	"+(int)currentFaith);

        faithHud.draw(shieldIcon, 0, 0);

        GUI.EndGroup();
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