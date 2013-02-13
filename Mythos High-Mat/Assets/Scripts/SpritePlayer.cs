using UnityEngine;
using System.Collections;

public class SpritePlayer : SpriteControl {
	
	private string minion = "archer";
	private string frameset = "run";
	
	void Awake() { base.Awake(); }
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//a = archer, s = swordie, d = mage
		//1 = run, 2 = attack
		if(Input.GetKeyUp(KeyCode.A)) {
			minion = "archer";
		}
		else if(Input.GetKeyUp(KeyCode.S)) {
			minion = "swordie";
		}
		else if(Input.GetKeyUp(KeyCode.D)) {
			minion = "mage";
		}
		if(Input.GetKeyUp(KeyCode.Alpha1)) {
			frameset = "run";
		}
		else if(Input.GetKeyUp(KeyCode.Alpha2)) {
			frameset = "attack";
		}
		
		sprite.PlayLoop(minion+"-"+frameset);
	}
}
