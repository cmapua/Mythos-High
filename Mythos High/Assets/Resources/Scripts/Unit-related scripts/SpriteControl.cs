using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(OTAnimatingSprite))]
public class SpriteControl : MonoBehaviour {
	protected OTAnimation anim;
	protected OTAnimatingSprite sprite;
	protected Unit unit, targetUnit;
	protected int frames = 0, lastFrame = 22; //default last frame for hero
	private int levelLength, levelWidth;
	private CharacterController cc;
	
	[HideInInspector]
	public bool isArcher = false, isMage = false, isSwordsman = false, isCastle = false, 
					wait = false, isAttacking = false, isCasting = false, playAnimation = false,
    				skill1Triggered = false, skill2Triggered = false, skill3Triggered = false;

	[HideInInspector]
	public Transform target;
	
	public Transform arrowPoint, hitVector; //various places to do certain things
	
	[HideInInspector]
	public Unit.type unit_type;
	
	protected enum heroState {
		attacking,
		castingSpell1,
		castingSpell2,
		castingSpell3,
		//enemyAI states
		standby,
		chasing,
		fallingBack
	}
	protected heroState currentState = heroState.attacking; //default state
	
	void Awake () {
		anim = GetComponent<OTAnimation>();
		sprite = GetComponent<OTAnimatingSprite>();
		unit = GetComponent<Unit>();
        cc = GetComponent<CharacterController>();
	}
	
	protected void Start() {
		unit_type = unit.unit_type;
		levelLength = unit.getUnitManager().getLevelLength();
		//levelWidth = unit.getUnitManager().getLevelWidth();
	}
	
	public void hpRegen(){
		unit.HP += unit.HPRegenRate;
		if (unit.HP>unit.maxHP){
			unit.HP = unit.maxHP;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0 && unit.moveSpeed > 0)
        {
			sprite.Resume();
			
            if (unit.HP < unit.maxHP) hpRegen();

            if (wait)
            {
                if (sprite.CurrentFrame().index == lastFrame)
                {
                    actionChooser();
                    wait = false;
                }
            }
            //handle input
            else if(unit_type == Unit.type.hero)
            {
                //handle attack
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    sprite.PlayOnce("hero-attack");
                    lastFrame = 5;
                    wait = true;
                    currentState = heroState.attacking;
                }

                //handle skills
                else if (Input.GetKeyDown(KeyCode.X) || skill1Triggered)
                {
                    sprite.PlayOnce("hero-cast");
                    lastFrame = 22;
                    wait = true;
                    currentState = heroState.castingSpell1;
                }
                else if (Input.GetKeyDown(KeyCode.C) || skill2Triggered)
                {
                    sprite.PlayOnce("hero-cast");
                    lastFrame = 22;
                    wait = true;
                    currentState = heroState.castingSpell2;
                }
                else if (Input.GetKeyDown(KeyCode.V) || skill3Triggered)
                {
                    sprite.PlayOnce("hero-cast");
                    lastFrame = 22;
                    wait = true;
                    currentState = heroState.castingSpell3;
                }

				//handle movement
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (sprite.transform.position.x > -(levelLength))
                        move(-Vector3.right);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (sprite.transform.position.x < levelLength)
                        move(Vector3.right);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (sprite.transform.position.z < 90)
                        move(Vector3.forward);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (sprite.transform.position.z > -90)
                        move(-Vector3.forward);
                }
                else
                {
                    sprite.PlayLoop("hero-idle");
                }
            }
        } else sprite.Pauze();
	}
	
	void actionChooser() {
		switch(currentState) {
		case heroState.attacking:
			attack();
			break;
		case heroState.castingSpell1:
            SkillManager.getInstance().activateSkill(0);
            skill1Triggered = false;
			//cast spell 1
			break;
		case heroState.castingSpell2:
            SkillManager.getInstance().activateSkill(1);
            skill2Triggered = false;
			//cast spell 2
			break;
		case heroState.castingSpell3:
			//cast spell 3
            SkillManager.getInstance().activateSkill(2);
            skill3Triggered = false;
			break;
		}
	}
	
	void attack() {
		GameObject arrow = OT.CreateObject("note-projectile");
		arrow.transform.position = arrowPoint.position;
		arrow.gameObject.layer = unit.getLayer();
	}
	
	protected void move(Vector3 dir) {
        if(cc)
            cc.SimpleMove(dir * unit.moveSpeed); //(moveSpeed * 100) * Time.deltaTime
        else transform.Translate(dir * unit.moveSpeed * Time.deltaTime);
		
		if(dir.x < 0 && !sprite._flipHorizontal) sprite.flipHorizontal = true;
		if(dir.x >= 0 && sprite._flipHorizontal) sprite.flipHorizontal = false;
		
		switch(unit_type) {
			case Unit.type.enemyHero:
			case Unit.type.hero: sprite.PlayLoop("hero-run"); break;
			case Unit.type.archer: sprite.PlayLoop("archer-run"); break;
			case Unit.type.swordsman: sprite.PlayLoop("swordie-run"); break;
			case Unit.type.mage: sprite.PlayLoop("mage-run"); break;
			default: break;
		}
	}

	protected Unit searchNearestTarget() {
		Unit newTarget = null;
        //if player unit
		if(unit.getLayer() == 8) {
			float dist = Mathf.Infinity;
			
            //get nearest enemy unit
			foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
				float newDist = u.getUnitTransform().position.x - unit.getUnitTransform().position.x;
				if(newDist <= (unit.range + 200) && newDist >=0){
					if(newDist < dist) {
						dist = newDist;
						newTarget = u;
					}
				}
			}
            //
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
				if(newDist <= (unit.range + 200) && newDist >=0){
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
	public Unit getTargetUnit() { return targetUnit; }
	public OTAnimatingSprite getSprite() { return sprite; }
	public OTAnimation getAnim() { return anim; }
}