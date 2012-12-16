using UnityEngine;
using System.Collections;

public class SpritePlayer : MonoBehaviour {
	
	public GameObject Archer, Swordsman, Mage;
	private int archerCost=35, swordCost=20, mageCost=50;
	private faithHud faith;
	private minionCooldown cool;
	// Use this for initialization
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
		if(Input.GetKeyUp(KeyCode.A)) {
			if(faith.currentFaith>=archerCost && cool.archerCanSpawn){
				OTObject nArcher = OT.CreateSprite("minion-Archer");
				nArcher.gameObject.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
				faith.currentFaith -= archerCost;
				cool.startCooldown("archer");
			}
		}
		else if(Input.GetKeyUp(KeyCode.S)) {
			if(faith.currentFaith>=swordCost && cool.swordsmanCanSpawn){
				OTObject nSwordsman = OT.CreateSprite("minion-Swordsman");
				nSwordsman.gameObject.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
				faith.currentFaith -= swordCost;
				cool.startCooldown("swordsman");			}
		}
		else if(Input.GetKeyUp(KeyCode.D)) {
			if(faith.currentFaith>=mageCost && cool.mageCanSpawn){
				OTObject nMage = OT.CreateSprite("minion-Mage");
				nMage.gameObject.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
				faith.currentFaith -= mageCost;
				cool.startCooldown("mage");
			}
		}
		else if(Input.GetKeyUp(KeyCode.Q)) {
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
