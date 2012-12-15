using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(OTAnimatingSprite))]
public class SpriteControl : MonoBehaviour {
	public float moveSpeed;
	
	public OTAnimation anim;
	public OTAnimatingSprite sprite;
	protected bool isArcher = false, isMage = false, isSwordsman = false, isCastle = false;
	protected bool wait = false;
	protected string unitType;
	private DamageControl dc;
	
	// Use this for initialization
	protected void Awake () {
		anim = GetComponent<OTAnimation>();
		sprite = GetComponent<OTAnimatingSprite>();
		//print (	sprite.animationFrameset);
	}
	
	void Start() {
		dc = transform.Find("AttackCollision").GetComponent<DamageControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z)) {
			sprite.PlayOnce("Attack");
			wait = true;
		}
		if(wait) {
			if(sprite.CurrentFrame().index  == 22) {
				wait = false;
				dc.activate = true;
			}
		}
		else {
			unitType = "hero";
			if(Input.GetKey(KeyCode.LeftArrow)) {
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
}