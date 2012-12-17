using UnityEngine;
using System.Collections;


public class MinionSpawner : MonoBehaviour {
	//public Transform yourSpawn, theirSpawn;
	public int level;
	private int spawnCycle=0;
	
	private static MinionSpawner instance;
	
		
	public static MinionSpawner getInstance() {
		if(instance == null) 
			instance = (MinionSpawner)FindObjectOfType(typeof(MinionSpawner));
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
		yield return StartCoroutine(spawnUnitBehaviour (spawnCycle));
	}	
	
	void createUnit(int enemyType){
		OTObject enemy;
		switch (enemyType){
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
	
	IEnumerator spawnUnitBehaviour(int cycle){
		if(Time.timeScale > 0) {
		float spawnCountdown=1;
		if (level==1){
			switch(cycle){
				case 0:
					spawnCountdown = 4.4f;
					createUnit(0);
					spawnCycle++;
					break;
				case 1:
					spawnCountdown = 4.4f;
					createUnit(0);
					spawnCycle++;
					break;
				case 2:
					spawnCountdown = 7f;
					createUnit(2);
					spawnCycle++;
					break;
				case 3:
					spawnCountdown = 4.4f;
					createUnit(0);
					spawnCycle++;
					break;
				case 4:
					spawnCountdown = 9.4f;
					createUnit(1);
					spawnCycle++;
					break;
				case 5:
					spawnCountdown = 4.4f;
					createUnit(0);
					spawnCycle++;
					break;
				case 6:
					spawnCountdown = 12f;
					createUnit(0);
					createUnit(2);
					spawnCycle=0;
					break;
			}
		}
		else if (level==2){
			switch(cycle){
				case 0:
					spawnCountdown = 1.2f;
					createUnit(0);
					spawnCycle++;
					break;
				case 1:
					spawnCountdown = 1.3f;
					createUnit(0);
					spawnCycle++;
					break;
				case 2:
					spawnCountdown = 1.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 3:
					spawnCountdown = 19f;
					createUnit(2);
					spawnCycle++;
					break;
				case 4:
					spawnCountdown = 2.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 5:
					spawnCountdown = 1f;
					createUnit(1);
					spawnCycle++;
					break;
				case 6:
					spawnCountdown = 30f;
					createUnit(0);
					spawnCycle=0;
					break;
			}
		}
		else if (level==3){
			switch(cycle){
				case 0:
					spawnCountdown = 16f;
					createUnit(0);
					createUnit(0);
					createUnit(0);
					createUnit(2);
					spawnCycle++;
					break;
				case 1:
					spawnCountdown = 1.3f;
					createUnit(0);
					spawnCycle++;
					break;
				case 2:
					spawnCountdown = 1.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 3:
					spawnCountdown = 19f;
					createUnit(2);
					spawnCycle++;
					break;
				case 4:
					spawnCountdown = 4.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 5:
					spawnCountdown = 6f;
					createUnit(0);
					createUnit(1);
					spawnCycle++;
					break;
				case 6:
					spawnCountdown = 30f;
					createUnit(0);
					createUnit(2);
					spawnCycle=0;
					break;
			}
		}
		else if (level==4){
			switch(cycle){
				case 0:
					spawnCountdown = 1.2f;
					createUnit(0);
					spawnCycle++;
					break;
				case 1:
					spawnCountdown = 1.3f;
					createUnit(0);
					spawnCycle++;
					break;
				case 2:
					spawnCountdown = 1.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 3:
					spawnCountdown = 19f;
					createUnit(2);
					spawnCycle++;
					break;
				case 4:
					spawnCountdown = 2.1f;
					createUnit(0);
					spawnCycle++;
					break;
				case 5:
					spawnCountdown = 1f;
					createUnit(1);
					spawnCycle++;
					break;
				case 6:
					spawnCountdown = 30f;
					createUnit(0);
					spawnCycle=0;
					break;
			}
		}

		else{
			spawnCountdown = 5;
			createUnit (0);
		}
		yield return new WaitForSeconds(spawnCountdown);
		}
	}
}
