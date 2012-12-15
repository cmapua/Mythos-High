using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(OTAnimatingSprite))]
public class SpriteControl : MonoBehaviour {
	public float moveSpeed, range = 70; //temporary value for effective weapon range (varies with hero)
	public OTAnimation anim;
	public OTAnimatingSprite sprite;
	protected bool isArcher = false, isMage = false, isSwordsman = false, isCastle = false;
	protected bool wait = false;
	protected string unitType;
	protected Unit unit, targetUnit;
	protected int frames = 0, lastFrame = 22; //default last frame for hero
	public Transform target, arrowPoint, hitVector; //various places to do certain things
	protected enum heroState {
		attacking,
		castingSpell1,
		castingSpell2,
		castingSpell3
	}
	
	protected heroState currentState = heroState.attacking; //default state
	
	void Awake () {
		anim = GetComponent<OTAnimation>();
		sprite = GetComponent<OTAnimatingSprite>();
		unit = GetComponent<Unit>();
	}
	
	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		if(wait) {
			if(sprite.CurrentFrame().index  == lastFrame) {
				//frames++;
				//if(frames == 1) { //this ensures that the action won't be repeated so many times (I hope)
					actionChooser(currentState);
					//frames = 0;
				//}
				wait = false;
				//dc.activate = true;
			}
		}
		//handle movement
		else {
			unitType = "hero";
			
			//handle attack
			if(Input.GetKeyDown(KeyCode.Z)) {
				sprite.PlayOnce("Attack");
				wait = true;
				currentState = heroState.attacking;
			}
			//handle skills (they all play attack animation since no cast animation is available
			else if(Input.GetKeyDown(KeyCode.X)) {
				sprite.PlayOnce("Attack");
				wait = true;
				currentState = heroState.castingSpell1;
				//set lastFrame to whatever the last frame of the animation is here
			}
			else if(Input.GetKeyDown(KeyCode.C)) {
				sprite.PlayOnce("Attack");
				wait = true;
				currentState = heroState.castingSpell2;
				//set lastFrame to whatever the last frame of the animation is here
			}
			else if(Input.GetKeyDown(KeyCode.V)) {
				sprite.PlayOnce("Attack");
				wait = true;
				currentState = heroState.castingSpell3;
				//set lastFrame to whatever the last frame of the animation is here
			}
			
			else if(Input.GetKey(KeyCode.LeftArrow)) {
				if(sprite.transform.position.x>-600)
				move(-Vector3.right, unitType);
			}
			else if(Input.GetKey(KeyCode.RightArrow)) {
				if(sprite.transform.position.x<600)
				move(Vector3.right, unitType);
			}
			else if(Input.GetKey(KeyCode.UpArrow)) {
				if(sprite.transform.position.z<90)
				move (Vector3.forward, unitType);
			}
			else if(Input.GetKey(KeyCode.DownArrow)) {
				if(sprite.transform.position.z>-90)
				move (-Vector3.forward, unitType);
			}
			else {
				sprite.PlayLoop ("Idle");
			}
		}
	}
	
	void actionChooser(heroState hs) {
		switch(hs) {
		case heroState.attacking:
			attack();
			break;
		case heroState.castingSpell1:
			break;
		case heroState.castingSpell2:
			break;
		case heroState.castingSpell3:
			break;
		}
	}
	
	void attack() {
		foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
			float dist = Vector3.Distance(u.transform.position, transform.position);
			if(dist < range) {
				u.HP -= unit.damage;
			}
		}
	}
	
	protected void move(Vector3 dir, string unitType) {
		transform.Translate(dir * moveSpeed * Time.deltaTime);
		if(dir.x < 0 && !sprite._flipHorizontal)
			sprite.flipHorizontal = true;
		if(dir.x >= 0 && sprite._flipHorizontal)
			sprite.flipHorizontal = false;
		if(unitType == "hero")
			sprite.PlayLoop ("Run");
		if(unitType == "archer")
			sprite.PlayLoop ("archer-run");
		if(unitType == "swordsman") 
			sprite.PlayLoop ("swordie-run");
		if(unitType == "mage")
			sprite.PlayLoop ("mage-run");
	}
	
	protected Unit searchNearestTarget() {
		print (gameObject.name + " is searching for enemies...");
		Unit newTarget = null;
		if(unit.getLayer() == 8) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
				float newDist = u.getUnitTransform().position.x - unit.getUnitTransform().position.x;
				if(newDist<=(range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
				if(u.sc && u.sc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		if(unit.getLayer() == 9) {
			float dist = Mathf.Infinity;
			
			foreach(Unit u in unit.getUnitManager().getYourUnits()) {
				float newDist = -(u.getUnitTransform().position.x - unit.getUnitTransform().position.x);
				if(newDist<=(range+200)&& newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
			foreach(Unit u in unit.getUnitManager().getYourUnits()) {
				if(u.sc && u.sc.target == gameObject.transform) {
					newTarget = u;
					break;
				}
			}
		}
		return newTarget;
	}
	
	public virtual bool canSearch() { return true; }
	public void setTargetUnit(Unit u) { targetUnit = u; }
}