using UnityEngine;
using System.Collections;

public class HeroControl : SpriteControl {
	int unitCountDiff;
	public Transform fallbackPoint;
	
	// Use this for initialization
	void Start () {
		currentState = heroState.fallingBack;
		fallbackPoint = GameObject.Find("AIFallbackPoint").transform;
		InvokeRepeating("AIBehaviour", 50f, 2f); //run AIBehaviour every half a second
	}
	
	void AIBehaviour () {
		unitCountDiff = unit.getUnitManager().getTheirUnits().Count - unit.getUnitManager().getYourUnits().Count;
		if(unit.HP > (unit.maxHP * 0.75)) {
			//activate super bass;
			if(unitCountDiff >= -3) {
				//charge at enemy hero
				chargeAt(unit.getUnitManager().getHero(8));
			}
			else if(unitCountDiff >= -5) {
				//charge at closest enemy unit
				chargeAt(searchNearestTarget());
			}
			//else if(
			else {
				// fall back
				chargeAt(null);
			}
		}
		else if((unit.maxHP * 0.75) > unit.HP && unit.HP > (unit.maxHP * 0.5)) {
			if(unitCountDiff >= -2) {
				//charge at enemy hero
				chargeAt(unit.getUnitManager().getHero(8));
			}
			else if(unitCountDiff >= -3) {
				//charge at closest enemy unit
				chargeAt(searchNearestTarget());
			}
			//else if(
			else {
				chargeAt(null);
			}
		}
		else if((unit.maxHP * 0.5) > unit.HP && unit.HP > (unit.maxHP * 0.25)) {
			if(unitCountDiff >= 1) {
				//charge at enemy hero
				chargeAt(unit.getUnitManager().getHero(8));
			}
			else if(unitCountDiff >= 0) {
				//charge at closest enemy unit
				chargeAt(searchNearestTarget());
			}
			//else if(
			else {
				chargeAt(null);
			}
		}
		else if((unit.maxHP * 0.25) > unit.HP && unit.HP > 0) {
			//cast 'total eclipse of the heart'
			if(beingAttacked()) {
				if(currentState != heroState.attacking)
					currentState = heroState.attacking;
			} else {
				target = null;
				targetUnit = null;
				if(currentState != heroState.fallingBack)
					currentState = heroState.fallingBack;
			}
		}
	}
	
	void chargeAt(Unit u) {
		targetUnit = u;
		target = targetUnit.transform;
		if(u != null) {
			if(currentState != heroState.attacking)
				currentState = heroState.attacking;
		}
		else {
			if(currentState != heroState.fallingBack)
				currentState = heroState.fallingBack;
		}
	}
	
	bool beingAttacked() {
		if(unit.getUnitManager().getYourUnits().Count > 0) {
			foreach(Unit u in unit.getUnitManager().getYourUnits()) {
				if(u.getSpriteControl().getTargetUnit() != null && u.getSpriteControl().getTargetUnit().name == gameObject.name) {
					targetUnit = u;
					target = targetUnit.transform;
					return true;
				}
			}
		}
		return false;
	}
	
	void Update() {
		switch(currentState) {
		case heroState.attacking:
			if(isAttacking) {
				if (sprite.CurrentFrame().index  == 4){
					targetUnit.HP -= unit.damage/10;
					isAttacking = false;
					playAnimation = false;
				}
			}
			if(playAnimation) {
				sprite.PlayLoop("hero-attack");
				isAttacking = true;
			}
			if(Mathf.Abs(target.position.x- transform.position.x) <= range) {
				playAnimation = true;
			} else currentState = heroState.chasing;
			break;
			
		case heroState.chasing:
			move((target.position - transform.position).normalized, unitType);
			if(Mathf.Abs(target.position.x- transform.position.x) <= range) {
				currentState = heroState.attacking;
			}
			break;
			
		case heroState.castingSpell1:
			if(isCasting) {
				if (sprite.CurrentFrame().index  == 4){
					targetUnit.HP -= unit.damage/10;
					isAttacking = false;
					playAnimation = false;
				}
			}
			if(playAnimation) {
				sprite.PlayLoop("hero-attack");
				isAttacking = true;
			}
			if(Mathf.Abs(target.position.x- transform.position.x) <= range) {
				playAnimation = true;
			} else currentState = heroState.chasing;
			break;
			
		case heroState.fallingBack:
			move ((fallbackPoint.position - transform.position).normalized, unitType);
			if(Mathf.Abs(fallbackPoint.position.x - transform.position.x) <= 0.1) {
				currentState = heroState.standby;
			}
			break;
			
		case heroState.standby:
			sprite.PlayLoop("hero-idle");
			break;
		}
	}
}
