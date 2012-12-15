using UnityEngine;
using System.Collections;


public class MinionSpawner : MonoBehaviour {
	//public Transform yourSpawn, theirSpawn;
	public float frequency;
	public GameObject enemyMinion;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % frequency == 0) {
			//float randZ = Random.Range(-100, 100);
			//Instantiate(enemyMinion, new Vector3(500, 0, Random.Range(-100, 100)), Quaternion.identity);
			OTObject enemy;
			switch (Random.Range (0,2)){
				case 0:
					enemy = OT.CreateSprite("enemy-Swordsman");
					enemy.gameObject.transform.position = new Vector3(900, 0, Random.Range(-100, 100));
					break;				
				case 1:
					enemy = OT.CreateSprite("enemy-Mage");
					enemy.gameObject.transform.position = new Vector3(900, 0, Random.Range(-100, 100));
					break;				
				case 2:
					enemy = OT.CreateSprite("enemy-Archer");
					enemy.gameObject.transform.position = new Vector3(900, 0, Random.Range(-100, 100));
					break;				
			}
		}
	}
}
