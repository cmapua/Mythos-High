using UnityEngine;
using System.Collections;

public class SpritePlayer : MonoBehaviour {
	
	public GameObject Archer, Swordsman, Mage;
	private int archerCost=35, swordCost=20, mageCost=50;
	private faithHud faith;
	private minionCooldown cool;
	private static SpritePlayer spriteP;


	public static SpritePlayer getInstance() {
		if(spriteP == null) 
			spriteP = (SpritePlayer)FindObjectOfType(typeof(SpritePlayer));
		return spriteP;
	}

	
	void Start () {
	
	}
	void Awake(){
		faith = faithHud.getInstance();
		cool = minionCooldown.getInstance();
	}
	// Update is called once per frame
	void Update () {
		//a = archer, s = swordie, d = mage
		//1 = run, 2 = attack
		if(Input.GetKeyUp(KeyCode.W)) {
			if(faith.currentFaith>=archerCost && cool.archerCanSpawn){
				cool.startCooldown("archer");
			}
		}
		else if(Input.GetKeyUp(KeyCode.Q)) {
			if(faith.currentFaith>=swordCost && cool.swordsmanCanSpawn){
				cool.startCooldown("swordsman");
			}
		}
		else if(Input.GetKeyUp(KeyCode.E)) {
			if(faith.currentFaith>=mageCost && cool.mageCanSpawn){
				cool.startCooldown("mage");
			}
		}
		else if(Input.GetKeyUp(KeyCode.R)) {
			if(faith.shrineLevel==1 && faith.currentFaith>=80){
				faith.levelShrine();
			}
			if(faith.shrineLevel==2 && faith.currentFaith>=120){
				faith.levelShrine();
			}
			if(faith.shrineLevel==3 && faith.currentFaith>=150){
				faith.levelShrine();
			}
		}
	}
}
