using UnityEngine;
using System.Collections;

public class SpritePlayer : MonoBehaviour {
	
	public GameObject Archer, Swordsman, Mage;
	private int archerCost=35, swordCost=20, mageCost=50;
	private faithHud faith;
	// Use this for initialization
	void Start () {
	
	}
	void Awake(){
		faith = faithHud.getInstance();
	}
	// Update is called once per frame
	void Update () {
		//a = archer, s = swordie, d = mage
		//1 = run, 2 = attack
		if(Input.GetKeyUp(KeyCode.A)) {
			if(faith.currentFaith>=archerCost){
				Instantiate(Archer, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
				faith.currentFaith -= archerCost;
			}
		}
		else if(Input.GetKeyUp(KeyCode.S)) {
			if(faith.currentFaith>=swordCost){
				Instantiate(Swordsman, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
				faith.currentFaith -= swordCost;
			}
		}
		else if(Input.GetKeyUp(KeyCode.D)) {
			if(faith.currentFaith>=mageCost){
				Instantiate(Mage, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
				faith.currentFaith -= mageCost;
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
