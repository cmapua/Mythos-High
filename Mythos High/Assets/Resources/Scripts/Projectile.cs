using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed = 200, areaOfEffect = 300, damage = 50;
	public int lifetime = 200;
	public Vector3 targetVector, trajectory;
	public Unit target;
	//private UnitManager manager;
	//bool instantaneous = false;
	public OTSprite projectileSprite;
	
	private Transform myTransform;
	
	void Awake () {
		//manager = UnitManager.getInstance();
	}
	
	void Start() {
		myTransform = transform;
		projectileSprite = GetComponent<OTSprite>();
		projectileSprite.onCollision = OnCollision;
		if(target != null)
			trajectory = (target.sc.hitVector.position - myTransform.position).normalized;
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

		myTransform.Translate(trajectory * moveSpeed * Time.deltaTime);
		
		lifetime--;
		if(lifetime == 0)
			DestroyObject(gameObject);
	}
}
