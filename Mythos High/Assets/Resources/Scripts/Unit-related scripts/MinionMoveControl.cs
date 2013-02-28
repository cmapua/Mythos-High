using UnityEngine;
using System.Collections;

public class MinionMoveControl : SpriteControl {
	private int maxDistance;
	public OTObject projectile;

	public override bool canSearch(){
		if(!target){
			if (!isAttacking)
				return true;
		}
		return false;
	}

	void Start() {
		base.Start();
		maxDistance = unit.getUnitManager().getLevelLength();
		
		if(!hitVector) hitVector = transform;
		
		if(target)
			targetUnit = target.gameObject.GetComponent<Unit>();
		
		StartCoroutine("CoStart");
	}
	
	protected virtual IEnumerator CoStart() {
		while(true) {
			yield return StartCoroutine(CoUpdate());
		}
	}
	
	protected virtual IEnumerator CoUpdate() {
		if(canSearch()) {
			Unit newTarget = searchNearestTarget();
			if(newTarget) {
				target = newTarget.transform;
				targetUnit = newTarget;
			}
		}
		yield return new WaitForSeconds(0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.deltaTime > 0 && unit.moveSpeed > 0){	//deltaTime or TimeScale?
			sprite.Resume();
			
			//if at last frame of attack animation for archer, stop playing and deal damage
			if(unit_type == Unit.type.archer && isAttacking && sprite.CurrentFrame().index  == 13) {
				frames++;
				if(frames == 5) {
					GameObject arrow = OT.CreateObject("projectile");
					if(target) {
						arrow.transform.position = arrowPoint.position; //new Vector3(transform.position.x + 70, transform.position.y + 64, transform.position.z);
						arrow.GetComponent<Projectile>().target = targetUnit; //.targetVector = targetUnit.mmc.hitVector.position; //new Vector3(target.position.x + 32, target.position.y + 64, target.position.z); //target.Find ("hitVector").position;
						arrow.gameObject.layer = unit.getLayer();
					}
					
					frames = 0;
				}
				isAttacking = false;
				playAnimation = false;
			}
			//if at last frame of attack animation for swordsman, stop playing and deal damage
			else if(unit_type == Unit.type.swordsman && isAttacking && sprite.CurrentFrame().index  == 32) {
				//targetUnit.HP -= unit.damage/5;
                targetUnit.dealDamage(unit.damage / 5);
	
				isAttacking = false;
				playAnimation = false;
			}
			//if at last frame of attack animation for mage, stop playing and deal damage
			else if(unit_type == Unit.type.mage && isAttacking && sprite.CurrentFrame().index  == 53) {
				//targetUnit.HP -= unit.damage/5;
                targetUnit.dealDamage(unit.damage / 5);
	
				isAttacking = false;
				playAnimation = false;
			}
            //summons
            else if (unit_type == Unit.type.summon && isAttacking && sprite.CurrentFrame().index  == 1)
            {
                targetUnit.dealDamage(unit.damage / 10);

                isAttacking = false;
                playAnimation = false;
            }
			
			//if it has a target within range, play the attack animation
			if(playAnimation) {
				if(unit_type == Unit.type.mage) {
					sprite.PlayLoop("mage-attack");
				}
				else if(unit_type == Unit.type.swordsman) {
					sprite.PlayLoop("swordie-attack");
				}
				else if(unit_type == Unit.type.archer) {
					sprite.PlayLoop("archer-attack");
				}
                else if (unit_type == Unit.type.summon)
                {
                    sprite.PlayLoop("attack");
                }
				isAttacking = true;
			}
			
			else {
				// if it has no target, move forward
				if(!target) {
					if(unit.getLayer() == 8) {
						if(sprite.transform.position.x<maxDistance)
							move(Vector3.right);
					}
					else {
						if(sprite.transform.position.x>-(maxDistance))
							move (-Vector3.right);
					}
				}
				// if it has a target, either start playing attack animation (if close enough) or move closer
				else {
					if(Mathf.Abs(target.position.x- transform.position.x) <= unit.range) { //within range
						//attack
						playAnimation = true;
					}
					else {
						if(unit.getLayer() == 8) {
							if(sprite.transform.position.x<maxDistance)
								move((target.position - transform.position).normalized); //move closer
						}
						else {
							if(sprite.transform.position.x>-(maxDistance))
								move((target.position - transform.position).normalized); //move closer
						}
					}
				}
			}
		} else sprite.Pauze();
	}
}