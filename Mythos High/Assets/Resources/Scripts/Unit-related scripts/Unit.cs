using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	//reference to unitmanager
	private UnitManager manager;
	//
	private int layer;
	private Transform unitTransform; //cached
	public SpriteControl sc;
	public bool isStatic = false, isHero = false, isSprite = true;
    public ShrineRigidBody deathAnimation;
	//public string name;

	//put stats whatevs here
	public float HP, maxHP, def = 10f;
	public float damage;
    public List<Effect> statusEffects;
	
	void Awake() {
		manager = UnitManager.getInstance();
		unitTransform = transform;
        if (isSprite)
            sc = GetComponent<SpriteControl>();
	}
	
	// Use this for initialization
	void Start () {
		//name = gameObject.name;
		layer = gameObject.layer;
		manager.addUnit(this);
		//print ("Unit "+gameObject.name+" added.");
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0)
        {
        }
	}

    public void dealDamage(float atk)
    {
        atk -= atk * (def/100f);
        HP -= atk;
    }
	
	protected void LateUpdate() {
		if(name == "enemyShrine" || name == "playerShrine" ||name =="Hero"){
			
		}
		else if(HP < 0) {
            if (!isSprite)
            {
                Instantiate(deathAnimation, transform.position, transform.rotation);
                //DestroyObject(gameObject);
            }
            //else OT.DestroyObject(gameObject);
			DestroyObject(gameObject);
		}
	}
	
	void OnDestroy() {
		//print (gameObject.name + " has died. :(");
		manager.removeUnit(this);
	}
	
	public int getLayer() { return layer; }
	public SpriteControl getSpriteControl() { return sc; }
	public UnitManager getUnitManager() { return manager; }
	public Transform getUnitTransform() { return unitTransform; }
}
