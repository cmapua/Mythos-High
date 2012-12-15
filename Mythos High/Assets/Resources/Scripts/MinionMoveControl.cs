using UnityEngine;
using System.Collections;

public class MinionMoveControl : SpriteControl {
	//public int unitTypeNumber;
	public bool newType = false;
	public OTObject projectile;

	public override bool canSearch(){
		if(!target){
			if (!isAttacking)
				return true;
		}
		return false;
	}

	void Start() {
		if(!hitVector) hitVector = transform;
		if(newType) {
			anim = gameObject.GetComponentInChildren<OTAnimation>(); //transform.Find("animSprite").GetComponent<OTAnimation>();
			sprite = gameObject.GetComponentInChildren<OTAnimatingSprite>(); //transform.Find("animSprite").GetComponent<OTAnimatingSprite>();
		}

		if(target)
			targetUnit = target.gameObject.GetComponent<Unit>();
		
		StartCoroutine("CoStart");
	}
	
	protected virtual IEnumerator CoStart() {
		print (gameObject.name + "'s CoStart() called.");
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
				print (gameObject.name + " found a target. --> " + newTarget.name);
			}
		}
		yield return new WaitForSeconds(0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		//kill v2's
		if( newType && Input.GetKeyUp(KeyCode.K) ) {
			DestroyObject(gameObject);
		}
		
		if (isCastle) {
			playAnimation = true;
		}
		//if at last frame of attack animation for archer, stop playing and deal damage
		else if(isArcher && isAttacking && sprite.CurrentFrame().index  == 13) {
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
			else if (isCastle) {
				sprite.Stop();
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
				if(Mathf.Abs(target.position.x- transform.position.x) <= range) { //within range
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
}
