using UnityEngine;
using System.Collections;

public class HeroControl : SpriteControl {
	int unitCountDiff;
	public Transform fallbackPoint;
	
	// Use this for initialization
	void Start () {
		fallbackPoint = GameObject.Find("AIFallbackPoint").transform;
		InvokeRepeating("AIBehaviour", 0f, 0.5f); //run AIBehaviour every half a second
	}
	
	// Update is called once per frame
	void AIBehaviour () {
		unitCountDiff = unit.getUnitManager().getTheirUnits().Count - unit.getUnitManager().getYourUnits().Count;
		if(unit.HP > (unit.maxHP * 0.75)) {
			//activate super bass;
			if(unitCountDiff >= 0) {
				//charge at enemy hero
			}
			else if(unitCountDiff >= -2) {
			}
			//else if(
			else {
				//fall back;
			}
		}
		else if((unit.maxHP * 0.75) > unit.HP && unit.HP > (unit.maxHP * 0.5)) {
		}
		else if((unit.maxHP * 0.5) > unit.HP && unit.HP > (unit.maxHP * 0.25)) {
		}
		else if((unit.maxHP * 0.25) > unit.HP && unit.HP > 0) {
		}
	}
}
