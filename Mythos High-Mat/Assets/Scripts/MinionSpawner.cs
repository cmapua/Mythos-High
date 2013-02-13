using UnityEngine;
using System.Collections;


public class MinionSpawner : MonoBehaviour {
	//public Transform yourSpawn, theirSpawn;
	public float frequency;
	public GameObject minion, enemyMinion;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % frequency == 0) {
			//float randZ = Random.Range(-100, 100);
			Instantiate(minion, new Vector3(-500, 0, Random.Range(-100, 100)), Quaternion.identity);
			Instantiate(enemyMinion, new Vector3(500, 0, Random.Range(-100, 100)), Quaternion.identity);
		}
	}
}
