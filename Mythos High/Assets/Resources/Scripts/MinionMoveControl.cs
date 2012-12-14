using UnityEngine;
using System.Collections;

public class MinionMoveControl : SpriteControl {
	public Transform target;
	
	public float range, unitTypeNumber;
	private bool isAttacking = false, playAnimation = false;
	private Unit unit, targetUnit;
	
	public bool canSearch(){
		if(!target){
			if (!isAttacking)
			return true;
		}
		return false;
	}
	
	
	void Awake() { 
		base.Awake();
		unit = GetComponent<Unit>();
		if(unitTypeNumber == 1){
			isMage = true;
			unitType = "mage";
		}
		else if(unitTypeNumber == 2){
			isArcher = true;
			unitType = "archer";
		}
		else if(unitTypeNumber == 3){
			isSwordsman = true;
			unitType = "swordsman";
		}
		else{
			unitType = "hero";
		}
	}
	
	void Start() {
		if(target)
			targetUnit = target.gameObject.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {

		//if at last frame of attack animation for archer, stop playing and deal damage
		if(isArcher && isAttacking && sprite.CurrentFrame().index  == 13) {
			targetUnit.HP -= unit.damage/5;

			isAttacking = false;
			playAnimation = false;
		}
		//if at last frame of attack animation for swordsman, stop playing and deal damage
		else if(isSwordsman && isAttacking && sprite.CurrentFrame().index  == 32) {
			targetUnit.HP -= unit.damage/5;

			isAttacking = false;
			playAnimation = false;
		}
		//if at last frame of attack animation for mage, stop playing and deal damage
		else if(isMage && isAttacking && sprite.CurrentFrame().index  == 53) {
			targetUnit.HP -= unit.damage/5;

			isAttacking = false;
			playAnimation = false;
		}
		
		//if it has a target within range, play the attack animation
		if(playAnimation) {
			if(isMage) {
				sprite.PlayLoop("mage-attack");
			}
			else if(isSwordsman) {
				sprite.PlayLoop("swordie-attack");
			}
			else if(isArcher) {
				sprite.PlayLoop("archer-attack");
			}			
			isAttacking = true;
		}
		
		else {
			// if it has no target, move forward
			if(!target) {
				if(unit.getLayer() == 8) {
					if(sprite.transform.position.x<400)
						move(Vector3.right, unitType);
				}
				else {
					if(sprite.transform.position.x>-400)
						move (-Vector3.right, unitType);
				}
			}
			// if it has a target, either start playing attack animation (if close enough) or move closer
			else {
				if(Vector3.Distance(target.position, transform.position) < range) { //within range
					//attack
					playAnimation = true;
				}
				else {
					if(unit.getLayer() == 8) {
						if(sprite.transform.position.x<400)
							move((target.position - transform.position).normalized, unitType); //move closer
					}
					else {
						if(sprite.transform.position.x>-400)
							move((target.position - transform.position).normalized, unitType); //move closer
					}
	
				}
			}
		}
	}
	
	public void setTargetUnit(Unit u) { targetUnit = u; }
}
