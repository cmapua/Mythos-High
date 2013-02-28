using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	//reference to unitmanager
	private UnitManager manager;
	//
	private int layer;
	private Transform unitTransform; //cached
	
	[HideInInspector]
	public SpriteControl sc;
	
	public bool isStatic = false, isHero = false, isSprite = true;
    public ShrineRigidBody deathAnimation;
	//public string name;

	//put stats whatevs here
	public float HP = 100f, maxHP = 100f, HPRegenRate = 0.01f, def = 10f;
	public float damage = 10f;
	public float moveSpeed = 100f, range = 80f;
    
	[HideInInspector]
	public List<Effect> statusEffects;
	
	public enum type {
		hero,
		enemyHero,
		swordsman,
		archer,
		mage,
		shrine
	}
	public type unit_type;
	
	void Awake() {
		manager = UnitManager.getInstance();
		unitTransform = transform;
        if (isSprite)
            sc = GetComponent<SpriteControl>();
	}
	
	// Use this for initialization
	void Start () {
		layer = gameObject.layer;
		manager.addUnit(this);
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
