using UnityEngine;
using System.Collections;

[RequireComponent(typeof(OTAnimatingSprite))]
public class SpriteControl : MonoBehaviour {
	public float moveSpeed;
	
	protected OTAnimation anim;
	protected OTAnimatingSprite sprite;
	protected bool wait = false;
	private DamageControl dc;	
	
	// Use this for initialization
	protected void Awake () {
		anim = GetComponent<OTAnimation>();
		sprite = GetComponent<OTAnimatingSprite>();
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
			if(Input.GetKey(KeyCode.LeftArrow)) {
				move(-Vector3.right);
			}
			else if(Input.GetKey(KeyCode.RightArrow)) {
				move(Vector3.right);
			}
			else if(Input.GetKey(KeyCode.UpArrow)) {
				move (Vector3.forward);
			}
			else if(Input.GetKey(KeyCode.DownArrow)) {
				move (-Vector3.forward);
			}
			else {
				sprite.PlayLoop ("Idle");
			}
		}
	}
	
	protected void move(Vector3 dir) {
		transform.Translate(dir * moveSpeed * Time.deltaTime);
		if(dir.x < 0 && !sprite._flipHorizontal)
			sprite.flipHorizontal = true;
		if(dir.x >= 0 && sprite._flipHorizontal)
			sprite.flipHorizontal = false;
		sprite.PlayLoop ("Run");
	}
}
