using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed = 200, areaOfEffect = 300, damage = 50;
	public int lifetime = 200;
	public Vector3 targetVector, trajectory;
	public Unit target;
	private UnitManager manager;
	bool instantaneous = false;
	public OTSprite projectileSprite;
	
	private Transform myTransform;
	
	void Awake () {
		manager = UnitManager.getInstance();
	}
	
	void Start() {
		myTransform = transform;
		projectileSprite = GetComponent<OTSprite>();
		projectileSprite.onCollision = OnCollision;
		if(target)
			trajectory = (target.mmc.hitVector.position - myTransform.position).normalized;
		else {
			if(gameObject.layer == 8)
				trajectory = Vector3.right;
			if(gameObject.layer == 9)
				trajectory = -Vector3.right;
		}
	}
	
	public void OnCollision(OTObject owner) {
		Unit collisionTarget = owner.collisionObject.gameObject.GetComponent<Unit>();
		
		if(collisionTarget != null) {
			if((gameObject.layer == 8 && collisionTarget.getLayer() == 9) || (gameObject.layer == 9 && collisionTarget.getLayer() == 8))
			//if(collisionTarget.name == target.name)
				collisionTarget.HP -= damage;
		}
		if(gameObject != null)
			DestroyObject(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
//		if(gameObject.layer == 8) {
//			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
//			//projectileSprite.position += (Vector2)(Vector3.right * moveSpeed * Time.deltaTime);
//		}
//		else if(gameObject.layer == 9) {
//			transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
//			//myTransform.position = Vector3.Lerp(myTransform.position, targetVector, moveSpeed * Time.deltaTime);
//		}
//		if(target != null)
//			myTransform.Translate((target.mmc.hitVector.position - myTransform.position).normalized * moveSpeed * Time.deltaTime);
//		else {
//			if(gameObject.layer == 8)
//				myTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
//			if(gameObject.layer == 9)
//				myTransform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
//		}
		
		myTransform.Translate(trajectory * moveSpeed * Time.deltaTime);
		
		lifetime--;
		if(lifetime == 0)
			DestroyObject(gameObject);
	}
}
