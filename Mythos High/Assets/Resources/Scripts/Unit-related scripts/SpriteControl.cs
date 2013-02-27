using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(OTAnimatingSprite))]
public class SpriteControl : MonoBehaviour {
	public float moveSpeed, range = 70, hpRegenRate = 0.05f; //temporary value for effective weapon range (varies with hero)
	public OTAnimation anim;
	public OTAnimatingSprite sprite;
	public int unitTypeNumber;
	protected string unitType;
	protected bool isArcher = false, isMage = false, isSwordsman = false, isCastle = false;
	protected bool wait = false, isAttacking = false, isCasting = false, playAnimation = false;
    public bool skill1Triggered = false, skill2Triggered = false, skill3Triggered = false;
	protected Unit unit, targetUnit;
	protected int frames = 0, lastFrame = 22; //default last frame for hero
	public Transform target, arrowPoint, hitVector; //various places to do certain things
    //private Rigidbody scRigidbody;
    private CharacterController cc;
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
		else if(unitTypeNumber == 0){
			isCastle = true;
			unitType = "castle";
		}
		else if(unitTypeNumber == 4){
			unitType = "enemyHero";
		}
		else if(unitTypeNumber == 5){
			unitType = "hero";
		}
	}
	
	void Start() {
	}
	
	public void hpRegen(){
		unit.HP += hpRegenRate;
		if (unit.HP>unit.maxHP){
			unit.HP = unit.maxHP;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0)
        { //timeScale or deltaTime?

            if (unit.HP < unit.maxHP)
            {
                hpRegen();
            }

            if (wait)
            {
                if (sprite.CurrentFrame().index == lastFrame)
                {
                    actionChooser(currentState);
                    wait = false;
                }
            }
            //handle movement
            else
            {
                unitType = "hero";

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

                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (sprite.transform.position.x > -1050)
                        move(-Vector3.right, unitType);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (sprite.transform.position.x < 1050)
                        move(Vector3.right, unitType);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (sprite.transform.position.z < 90)
                        move(Vector3.forward, unitType);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (sprite.transform.position.z > -90)
                        move(-Vector3.forward, unitType);
                }
                else
                {
                    sprite.PlayLoop("hero-idle");
                }
            }
        }
	}
	
	void actionChooser(heroState hs) {
		switch(hs) {
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
		arrow.transform.position = arrowPoint.position; //new Vector3(transform.position.x + 70, transform.position.y + 64, transform.position.z);
		//arrow.GetComponent<Projectile>().target = targetUnit; //.targetVector = targetUnit.mmc.hitVector.position; //new Vector3(target.position.x + 32, target.position.y + 64, target.position.z); //target.Find ("hitVector").position;
		arrow.gameObject.layer = unit.getLayer();
	
        //foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
        //    float dist = Mathf.Abs(u.transform.position.x - transform.position.x);
        //    if(dist < range) {
        //        u.HP -= unit.damage;
        //    }
        //}
	}
	
	protected void move(Vector3 dir, string unitType) {
        //scRigidbody.AddForce(dir * moveSpeed * Time.deltaTime);
        //scRigidbody.velocity = dir * moveSpeed * Time.deltaTime;
		//transform.Translate(dir * moveSpeed * Time.deltaTime);
        if(cc)
            cc.SimpleMove(dir * (moveSpeed * 100) * Time.deltaTime);
        else transform.Translate(dir * moveSpeed * Time.deltaTime);
		if(dir.x < 0 && !sprite._flipHorizontal) {
			sprite.flipHorizontal = true;
			//arrowPoint.position.x = -1 * arrowPoint.position.x;
		}
		if(dir.x >= 0 && sprite._flipHorizontal) {
			sprite.flipHorizontal = false;
			//arrowPoint.position.x = -1 * arrowPoint.position.x;
		}
		if(unitType == "hero")
			sprite.PlayLoop ("hero-run");
		if(unitType == "archer")
			sprite.PlayLoop ("archer-run");
		if(unitType == "swordsman") 
			sprite.PlayLoop ("swordie-run");
		if(unitType == "mage")
			sprite.PlayLoop ("mage-run");
		if(unitType == "enemyHero")
			sprite.PlayLoop("hero-run");
	}

    void jump()
    {
        if(unitType=="hero")
            unit.rigidbody.AddForce(Vector3.up * 200, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space)) jump();
    }

	protected Unit searchNearestTarget() {
		Unit newTarget = null;
        //if player unit
		if(unit.getLayer() == 8) {
			float dist = Mathf.Infinity;
			
            //get nearest enemy unit
			foreach(Unit u in unit.getUnitManager().getTheirUnits()) {
				float newDist = u.getUnitTransform().position.x - unit.getUnitTransform().position.x;
				if(newDist<=(range+200)&& newDist >=0){
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
	public Unit getTargetUnit() { return targetUnit; }
}