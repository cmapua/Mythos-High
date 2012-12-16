using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	//reference to unitmanager
	private UnitManager manager;
	//
	private int layer;
	private Transform unitTransform; //cached
	public SpriteControl sc;
	public bool isStatic = false, isHero = false;
	//public string name;

	//put stats whatevs here
	public float HP, maxHP;
	public float damage;
	
	void Awake() {
		manager = UnitManager.getInstance();
		unitTransform = transform;
		sc = GetComponent<SpriteControl>();
	}
	
	// Use this for initialization
	void Start () {
		//name = gameObject.name;
		layer = gameObject.layer;
		manager.addUnit(this);
		print ("Unit "+gameObject.name+" added.");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void LateUpdate() {
		if(name == "enemyShrine" || name == "playerShrine"){
			
		}
		else if(HP < 0) {
			DestroyObject(gameObject);
		}
	}
	
	void OnDestroy() {
		print (gameObject.name + " has died. :(");
		manager.removeUnit(this);
	}
	
	public int getLayer() { return layer; }
	public SpriteControl getSpriteControl() { return sc; }
	public UnitManager getUnitManager() { return manager; }
	public Transform getUnitTransform() { return unitTransform; }
}
