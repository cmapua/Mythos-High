using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed = 200, areaOfEffect = 300, damage = 50;
	public Vector3 targetVector;
	private UnitManager manager;
	bool instantaneous = false;
	public OTSprite projectileSprite;
	
	private Transform myTransform;
	
	void Awake() {
		manager = UnitManager.getInstance();
	}
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
		projectileSprite = GetComponent<OTSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		//rotate towards target
		//projectileSprite.RotateTowards(new Vector2(targetVector.x, targetVector.y));
		
		float dist = Vector3.Distance(targetVector, myTransform.position);
		if(dist < 0.1) { // very, very close
			//instantiate some explosion animation
			if(gameObject.layer == 8) {
				foreach(Unit u in manager.getTheirUnits()) {
					//calculate distance from epicenter
					float d = Vector3.Distance(u.transform.position, myTransform.position);
					if(d < areaOfEffect) {
						u.HP -= damage/5;
					}
				}
			}
			
			if(gameObject.layer == 9) {
				foreach(Unit u in manager.getYourUnits()) {
					//calculate distance from epicenter
					float d = Vector3.Distance(u.transform.position, myTransform.position);
					if(d < areaOfEffect) {
						u.HP -= damage/5;
					}
				}
			}
			//finally destroy object after dealing damage
			if(gameObject != null)
				DestroyObject(gameObject);
		}
		else { //move closer
			//myTransform.Translate((targetVector - myTransform.position).normalized * moveSpeed * Time.deltaTime);
			myTransform.position = Vector3.Lerp(myTransform.position, targetVector, moveSpeed * Time.deltaTime);
		}
	}
}
