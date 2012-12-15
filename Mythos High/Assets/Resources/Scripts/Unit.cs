using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	//reference to unitmanager
	private UnitManager manager;
	//
	private int layer;
	private Transform unitTransform;
	public MinionMoveControl mmc;
	public bool isStatic = false;
	public string name;

	//put stats whatevs here
	public float HP, maxHP;
	public float damage;
	
	void Awake() {
		manager = UnitManager.getInstance();
		unitTransform = transform;
		mmc = GetComponent<MinionMoveControl>();
	}
	
	// Use this for initialization
	void Start () {
		name = gameObject.name;
		layer = gameObject.layer;
		manager.addUnit(this);
		print ("Unit "+gameObject.name+" added.");
		if(!isStatic)
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
		if(mmc.canSearch())
			yield return StartCoroutine(SearchForTarget());
	}
	
	//search for closest enemy
	IEnumerator SearchForTarget() {
		print (gameObject.name + " is searching for enemies...");
		Unit newTarget = null;
		if(layer == 8) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in manager.getTheirUnits()) {
				float newDist = u.unitTransform.position.x-unitTransform.position.x;
				if(newDist<=(mmc.range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in manager.getTheirUnits()) {
				if(u.mmc && u.mmc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		if(layer == 9) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in manager.getYourUnits()) {
				float newDist = -(u.unitTransform.position.x-unitTransform.position.x);
				if(newDist<=(mmc.range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in manager.getYourUnits()) {
				if(u.mmc && u.mmc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		if(newTarget) {
			mmc.target = newTarget.transform;
			mmc.setTargetUnit(newTarget);
			print (gameObject.name + " found a target. --> " + newTarget.name);
		}
		yield return new WaitForSeconds(0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate() {
		if(HP < 0) {
			DestroyObject(gameObject);
		}
	}
	
	void OnDestroy() {
		print (gameObject.name + " has died. :(");
		manager.removeUnit(this);
	}
	
	public int getLayer() { return layer; }
	public MinionMoveControl getMinionMoveControl() { return mmc; }
}
