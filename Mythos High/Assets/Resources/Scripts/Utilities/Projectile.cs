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
	public bool animated = false;
	public OTAnimatingSprite animatingProjectileSprite;
	
	private Transform myTransform;
	
	void Awake () {
		//manager = UnitManager.getInstance();
		if(animated) {
			animatingProjectileSprite = GetComponent<OTAnimatingSprite>();
			animatingProjectileSprite.onCollision = OnCollision;
			animatingProjectileSprite.Play(72);
		}else {
			projectileSprite = GetComponent<OTSprite>();
			projectileSprite.onCollision = OnCollision;
		}
	}
	
	void Start() {
		myTransform = transform;
        if (target != null)
        {
            if (target.isSprite)
                trajectory = (target.sc.hitVector.position - myTransform.position).normalized;
            else trajectory = (target.getUnitTransform().position - myTransform.position).normalized;
        }
        else
        {
            if (gameObject.layer == 8)
                trajectory = Vector3.right;
            if (gameObject.layer == 9)
            {
                trajectory = -Vector3.right;
				if(animated) {
					if(!animatingProjectileSprite._flipHorizontal) animatingProjectileSprite.flipHorizontal = true;
				} else {
                	if(!projectileSprite._flipHorizontal) projectileSprite.flipHorizontal = true;
				}
            }
        }
	}
	
	public void OnCollision(OTObject owner) {
		if(areaOfEffect > 0 && (owner.gameObject.layer != gameObject.layer)) {
			
			Collider[] allUnitsCollided = null;
			if(gameObject.layer == 8) {
				allUnitsCollided = Physics.OverlapSphere(myTransform.position, areaOfEffect, 1 << 9);
			}
			if(gameObject.layer == 9) {
				allUnitsCollided = Physics.OverlapSphere(myTransform.position, areaOfEffect, 1 << 8);
			}
			if(allUnitsCollided != null) {
				foreach(Collider c in allUnitsCollided) {
					Unit u = c.gameObject.GetComponent<Unit>();
					if(u) u.dealDamage(damage);
					//c.gameObject.GetComponent<Unit>().dealDamage(damage);
				}
			}
			if(gameObject != null) DestroyObject(gameObject);
		} else {
			Unit collisionTarget = owner.collisionObject.gameObject.GetComponent<Unit>();
			
			if(collisionTarget != null) {
				if((gameObject.layer == 8 && collisionTarget.getLayer() == 9) || (gameObject.layer == 9 && collisionTarget.getLayer() == 8)) {
					//collisionTarget.HP -= damage;
	                collisionTarget.dealDamage(damage);
					if(gameObject != null)
						DestroyObject(gameObject);
	                    //OT.DestroyObject(gameObject);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//gah trigonometry again, screw it
		//Vector3 wavyTrajectory = new Vector3(trajectory.x, Mathf.Sin(trajectory.x) * transform.position.y, trajectory.z);
		
		myTransform.Translate(trajectory * moveSpeed * Time.deltaTime);
		
		if(animated) animatingProjectileSprite.PlayLoop("mage-projectile");
		
		lifetime--;
		if(lifetime == 0)
			DestroyObject(gameObject);
	}
}
