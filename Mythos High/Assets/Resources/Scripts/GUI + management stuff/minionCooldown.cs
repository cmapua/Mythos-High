using UnityEngine;
using System.Collections;

public class minionCooldown : MonoBehaviour {
	
	public float archerCooldown = 0, swordsmanCooldown = 0, mageCooldown = 0;
	public bool archerCanSpawn = true, swordsmanCanSpawn = true, mageCanSpawn = true;
	private int archerCost=35, swordCost=20, mageCost=50;
	
	private static minionCooldown instance;
	private static faithHud faith;
	
	public static minionCooldown getInstance() {
		if(instance == null) 
			instance = (minionCooldown)FindObjectOfType(typeof(minionCooldown));
		return instance;
	}
	
	// Use this for initialization
	void Start(){
		faith = faithHud.getInstance ();
		StartCoroutine("CoStart");
	}
	
	IEnumerator CoStart() {
		//print ("CoStart() called.");
		while(true) {
			yield return StartCoroutine(CoUpdate());
		}
	}
	
	IEnumerator CoUpdate() {
		//print ("CoUpdate() called.");
		yield return StartCoroutine(cooldown());
	}
	
	IEnumerator cooldown(){
		if(archerCooldown>0){
			archerCooldown--;
		}else{
			archerCanSpawn=true;
		}
		if(swordsmanCooldown>0){
			swordsmanCooldown--;
		}else{
			swordsmanCanSpawn=true;
		}
		if(mageCooldown>0){
			mageCooldown--;
		}else{
			mageCanSpawn=true;
		}
		yield return new WaitForSeconds(1);
	}
	
    //note: changed instantiation type from 'OTOBject' to 'GameObject' (seems there's an issue destroying them if type is OTObject :/)
	public void startCooldown(string cooldownType){
		switch (cooldownType){
		case "archer":
			GameObject nArcher = OT.CreateObject("minion-Archer");
			nArcher.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
			faith.currentFaith -= archerCost;

			archerCanSpawn = false;
			archerCooldown = 4;
			break;
		case "swordsman":
			GameObject nSwordsman = OT.CreateObject("minion-Swordsman");
			nSwordsman.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
			faith.currentFaith -= swordCost;

			swordsmanCanSpawn = false;
			swordsmanCooldown = 2;
			break;
		case "mage":
			GameObject nMage = OT.CreateObject("minion-Mage");
			nMage.transform.position = new Vector3(-900, 0, Random.Range(-100, 100));
			faith.currentFaith -= mageCost;

			mageCanSpawn = false;
			mageCooldown = 7;
			break;
		}
	}
	public float mageCooldownTime(){return mageCooldown;}
	public float archerCooldownTime(){return archerCooldown;}
	public float swordsmanCooldownTime(){return swordsmanCooldown;}
}