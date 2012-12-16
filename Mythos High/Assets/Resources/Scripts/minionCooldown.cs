using UnityEngine;
using System.Collections;

public class minionCooldown : MonoBehaviour {
	
	public float archerCooldown = 0, swordsmanCooldown = 0, mageCooldown = 0;
	public bool archerCanSpawn = true, swordsmanCanSpawn = true, mageCanSpawn = true;
	
	private static minionCooldown instance;
	
	public static minionCooldown getInstance() {
		if(instance == null) 
			instance = (minionCooldown)FindObjectOfType(typeof(minionCooldown));
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
	
	public void startCooldown(string cooldownType){
		switch (cooldownType){
		case "archer":
			archerCanSpawn = false;
			archerCooldown = 4;
			break;
		case "swordsman":
			swordsmanCanSpawn = false;
			swordsmanCooldown = 2;
			break;
		case "mage":
			mageCanSpawn = false;
			mageCooldown = 7;
			break;
		}
	}
}