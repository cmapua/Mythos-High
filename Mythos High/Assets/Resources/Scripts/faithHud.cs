using UnityEngine;
using System.Collections;

public class faithHud : MonoBehaviour {
	
	private int maxFaith = 150;
	public int shrineLevel = 1;
	public float currentFaith = 0;
	private float resourceGatherRate = .6f;
	
	private static faithHud instance;
	
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
			resourceGatherRate = .4f;
			maxFaith = 200;
			currentFaith -=80;
		}
		if(shrineLevel==3){
			resourceGatherRate = .2f;
			maxFaith = 250;
			currentFaith -=120;
		}
		if(shrineLevel==4){
			resourceGatherRate = .1f;
			maxFaith = 300;
			currentFaith -=150;
		}
	}
}