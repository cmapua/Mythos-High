using UnityEngine;
using System.Collections;

public class minionHUD : MonoBehaviour {
	
	private static minionCooldown mCool;
	private static faithHud faith;
	private int archerCost=35, swordCost=20, mageCost=50, shrineLevel = 1;
	public float swordOffset, archerOffset, mageOffset, shrineOffset;
	// Use this for initialization
	void Start(){
		mCool = minionCooldown.getInstance();
		faith = faithHud.getInstance();
	}
	
	void OnGUI() {
		shrineLevel = faith.shrineLevel;
		string shrineText = "Upgrade Shrine";

		float currentOffset = 300-faith.getYOffset();
		swordOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.swordsmanCooldownTime();
		archerOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.archerCooldownTime();
		mageOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.mageCooldownTime();
		shrineOffset = Screen.height*5/6+currentOffset;
		
		if(GUI.Button(new Rect(0 , swordOffset, Screen.width/5,Screen.height/6),"Swordsman\n20")){
			if(faith.currentFaith>=swordCost && mCool.swordsmanCanSpawn){
				mCool.startCooldown("swordsman");
			}
		}
		else if(GUI.Button(new Rect(Screen.width/5 , archerOffset , Screen.width/5,Screen.height/6),"Archer\n35")){
			if(faith.currentFaith>=archerCost && mCool.archerCanSpawn){
				mCool.startCooldown("archer");
			}
		}
		else if(GUI.Button(new Rect(Screen.width*2/5 , mageOffset, Screen.width/5,Screen.height/6),"Mage\n50")){
			if(faith.currentFaith>=mageCost && mCool.mageCanSpawn){
				mCool.startCooldown("mage");
			}
		}
		switch(shrineLevel){
			case 1:
			shrineText = "Upgrade Shrine\n80";
			break;
			case 2:
			shrineText = "Upgrade Shrine\n120";
			break;
			case 3:
			shrineText = "Upgrade Shrine\n150";
			break;
		}
		if( shrineLevel != 4 && GUI.Button(new Rect(Screen.width*3/5 , shrineOffset, Screen.width/5,Screen.height/6),shrineText)){
			if(faith.currentFaith>=mageCost && mCool.mageCanSpawn){
				mCool.startCooldown("mage");
			}
		}
		else if (shrineLevel == 4){
			shrineOffset = Screen.height;
		}
	}
	
	void Update() {
	}
	

}