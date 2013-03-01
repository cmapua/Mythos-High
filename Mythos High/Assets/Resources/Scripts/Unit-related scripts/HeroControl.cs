using UnityEngine;
using System.Collections;

public class HeroControl : SpriteControl {
	int unitCountDiff;
    public int attackLastFrame, castLastFrame, ability1CD = 30;

	bool action1, action2, action3, action4;
	
	[HideInInspector]
	public Transform fallbackPoint;
	
	// Use this for initialization
	void Start () {
        skill1Triggered = false;
        skill2Triggered = false;
        skill3Triggered = false;
		currentState = heroState.fallingBack;
		fallbackPoint = GameObject.Find("AIFallbackPoint").transform;
	}
	
	void AIBehaviour () {
		unitCountDiff = unit.getUnitManager().getTheirUnits().Count - unit.getUnitManager().getYourUnits().Count;
		if(unit.HP > (unit.maxHP * 0.75)) {
			//activate super bass;
            if (ability1CD<=0)
            {
                currentState = heroState.castingSpell1;
				ability1CD = 300;
            }
            if (beingAttacked())
            {
                currentState = heroState.attacking;
            }
            else if (unitCountDiff >= -3)
            {
                //charge at enemy hero
                chargeAt(unit.getUnitManager().getHero(8));
            }
            else if (unitCountDiff >= -5)
            {
                //charge at closest enemy unit
                chargeAt(searchNearestTarget());
            }
            //else if(
            else
            {
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
				if(currentState != heroState.attacking || currentState != heroState.chasing){
					if(currentState == heroState.chasing)
						currentState = SpriteControl.heroState.chasing;
					else 
						currentState = heroState.attacking;
				}
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
        target = u.transform;
		print ("Charging at " + targetUnit.name);
		if(u != null) {
			if(currentState != heroState.attacking || currentState != heroState.chasing || currentState != heroState.castingSpell1){
				if(currentState == heroState.chasing)
					currentState = SpriteControl.heroState.chasing;
				else 
					currentState = heroState.attacking;
			}
		}
		else {
			if(currentState != heroState.fallingBack)
				currentState = heroState.fallingBack;
		}
	}
	
	bool beingAttacked() {
		if(unit.getUnitManager().getYourUnits().Count > 0) {
			foreach(Unit u in unit.getUnitManager().getYourUnits()) {
				if(u.getSpriteControl() != null && u.getSpriteControl().getTargetUnit() != null && u.getSpriteControl().getTargetUnit().name == gameObject.name) {
					targetUnit = u;
					target = targetUnit.transform;
					return true;
				}
			}
		}
		return false;
	}

    void AIactionChooser()
    {
        switch (currentState)
        {
            case heroState.attacking:
                //if playing attack animation
                if (isAttacking)
                {
                    //print ("not yet...");
                    if (sprite.CurrentFrame().index == attackLastFrame)
                    {
                        //print ("DAMAGE HIM!!!");
                        targetUnit.dealDamage(unit.damage / 10);
                        playAnimation = false;
                        isAttacking = false;
                    }
                }
                //if target within range, give permission to play attack animation; else chase target
                if (Mathf.Abs(target.position.x - transform.position.x) <= unit.range)
                {
                    playAnimation = true;
                }
                else
                {
                    playAnimation = false;
                    isAttacking = false;
                    currentState = heroState.chasing;
                }
                //if given permission to play attack animation, proceed
                if (playAnimation)
                {
                    sprite.PlayLoop("hero-attack");
                    isAttacking = true;
                }
                break;

            case heroState.chasing:
                move((target.position - transform.position).normalized);
                if (Mathf.Abs(target.position.x - transform.position.x) <= unit.range)
                {
                    currentState = heroState.attacking;
                }
                else
                {
                    sprite.PlayLoop("hero-run");
                }
                break;

            case heroState.castingSpell1:
                print("entered casting state");
                if (isCasting)
                {
                    if (sprite.CurrentFrame().index == castLastFrame)
                    {
                        SkillManager.getInstance().activateEnemySkill(1);
                        isCasting = false;
                        playAnimation = false;
                    }
                }
                if (playAnimation)
                {
                    sprite.PlayLoop("hero-cast");
                    isCasting = true;
                }
                if (SkillManager.getInstance().findSkill("War Song", 9)) playAnimation = true;
                //else if (Mathf.Abs(target.position.x - transform.position.x) <= unit.range)
                //{
                //    playAnimation = true;
                //}
                else currentState = heroState.chasing;
                break;

            case heroState.fallingBack:
                move((fallbackPoint.position - transform.position).normalized);
                if (Mathf.Abs(fallbackPoint.position.x - transform.position.x) <= 0.1)
                {
                    currentState = heroState.standby;
                }
                break;

            case heroState.standby:
                sprite.PlayLoop("hero-idle");
                break;
        }
    }

	void Update() {
		if (ability1CD >0){
			ability1CD--;
		}
		if(Time.timeScale > 0 && unit.moveSpeed > 0) {
			sprite.Resume();
			
			if(unit.HP<unit.maxHP){
				hpRegen();
			}
			AIBehaviour ();
            AIactionChooser();
		} else sprite.Pauze();
	}
}
