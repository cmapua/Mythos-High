using UnityEngine;
using System.Collections;

public class SpritePlayer : MonoBehaviour {
	
	public GameObject Archer, Swordsman, Mage;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//a = archer, s = swordie, d = mage
		//1 = run, 2 = attack
		if(Input.GetKeyUp(KeyCode.A)) {
			Instantiate(Archer, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
		}
		else if(Input.GetKeyUp(KeyCode.S)) {
			Instantiate(Swordsman, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
		}
		else if(Input.GetKeyUp(KeyCode.D)) {
			Instantiate(Mage, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
		}
	}
}
