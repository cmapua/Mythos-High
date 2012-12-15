using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	//reference to unitmanager
	private UnitManager manager;
	//
	private int layer;
	private Transform unitTransform;
	public SpriteControl sc;
	public bool isStatic = false, isHero = false;
	public string name;

	//put stats whatevs here
	public float HP, maxHP;
	public float damage;
	
	protected void Awake() {
		manager = UnitManager.getInstance();
		unitTransform = transform;
		sc = GetComponent<SpriteControl>();
	}
	
	// Use this for initialization
	protected void Start () {
		name = gameObject.name;
		layer = gameObject.layer;
		manager.addUnit(this);
		print ("Unit "+gameObject.name+" added.");
		if(!isStatic || !isHero) {
			StartCoroutine("CoStart");
		}
	}
	
	protected virtual IEnumerator CoStart() {
		print (gameObject.name + "'s CoStart() called.");
		while(true) {
			yield return StartCoroutine(CoUpdate());
		}
	}
	
	protected virtual IEnumerator CoUpdate() {
		//print ("CoUpdate() called.");
		if(sc.canSearch() && (!isStatic || !isHero))
			yield return StartCoroutine(SearchForTarget());
	}
	
	//search for closest enemy
	protected IEnumerator SearchForTarget() {
		print (gameObject.name + " is searching for enemies...");
		Unit newTarget = null;
		if(layer == 8) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in manager.getTheirUnits()) {
				float newDist = u.unitTransform.position.x-unitTransform.position.x;
				if(newDist<=(sc.range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in manager.getTheirUnits()) {
				if(u.sc && u.sc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		if(layer == 9) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in manager.getYourUnits()) {
				float newDist = -(u.unitTransform.position.x-unitTransform.position.x);
				if(newDist<=(sc.range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in manager.getYourUnits()) {
				if(u.sc && u.sc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		if(newTarget) {
			sc.target = newTarget.transform;
			sc.setTargetUnit(newTarget);
			print (gameObject.name + " found a target. --> " + newTarget.name);
		}
		yield return new WaitForSeconds(0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void LateUpdate() {
		if(HP < 0) {
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
}
