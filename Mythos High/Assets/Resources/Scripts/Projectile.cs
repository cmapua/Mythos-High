using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed;
	private UnitManager manager;
	
	void Awake() {
		manager = UnitManager.getInstance();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
