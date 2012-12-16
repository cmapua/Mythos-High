using UnityEngine;
using System.Collections;

public class minionHUD : MonoBehaviour {
	
	private static minionCooldown mCool;
	private static faithHud faith;
	private int archerCost=35, swordCost=20, mageCost=50;
	public float swordOffset, archerOffset, mageOffset;
	// Use this for initialization
	void Start(){
		mCool = minionCooldown.getInstance();
		faith = faithHud.getInstance();
	}
	
	void OnGUI() {
		float currentOffset = 300-faith.getYOffset();
		swordOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.swordsmanCooldownTime();
		archerOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.archerCooldownTime();
		mageOffset = Screen.height*5/6+currentOffset+Screen.height/6*mCool.mageCooldownTime();
		
		if(GUI.Button(new Rect(0 , swordOffset, Screen.width/8,Screen.height/6),"Swordsman\n20")){
			if(faith.currentFaith>=swordCost && mCool.swordsmanCanSpawn){
				mCool.startCooldown("swordsman");
			}
		}
		else if(GUI.Button(new Rect(Screen.width/8 , archerOffset , Screen.width/8,Screen.height/6),"Archer\n35")){
			if(faith.currentFaith>=archerCost && mCool.archerCanSpawn){
				mCool.startCooldown("archer");
			}
		}
		else if(GUI.Button(new Rect(Screen.width/4 , mageOffset, Screen.width/8,Screen.height/6),"Mage\n50")){
			if(faith.currentFaith>=mageCost && mCool.mageCanSpawn){
				mCool.startCooldown("mage");
			}
		}
	}
	
	void Update() {
	}
	

}