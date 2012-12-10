using UnityEngine;
using System.Collections;

public class MinionMoveControl : SpriteControl {
	public Transform target;
	
	public float range;
	private bool isAttacking = false, playAnimation = false;
	private Unit unit, targetUnit;
	
	void Awake() { 
		base.Awake();
		unit = GetComponent<Unit>();
	}
	
	void Start() {
		if(target)
			targetUnit = target.gameObject.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {

		//if at last frame of attack animation, stop playing and deal damage
		if(isAttacking && sprite.CurrentFrame().index  == 22) {
			isAttacking = false;
			playAnimation = false;
			
			targetUnit.HP -= unit.damage/5;
		}
		
		//if it has a target within range, play the attack animation
		if(playAnimation) {
			sprite.PlayLoop("Attack");
			
			isAttacking = true;
		}
		else {
			// if it has no target, move forward
			if(!target) {
				if(unit.getLayer() == 8)
					move(Vector3.right);
				else
					move (-Vector3.right);
			}
			// if it has a target, either start playing attack animation (if close enough) or move closer
			else {
				if(Vector3.Distance(target.position, transform.position) < range) { //within range
					//attack
					playAnimation = true;
				}
				else
					move((target.position - transform.position).normalized); //move closer
			}
		}
	}
	
	public void setTargetUnit(Unit u) { targetUnit = u; }
}
