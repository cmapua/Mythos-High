using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

//[System.Serializable]
//public class Effect
//{
//    public string name;
//    public float intensity;
//    //public int affectedLayer;
//    public int op;
//    public bool isBuff = false;
//    public float buffDuration = 5f;

//    public float result(float prevvalue)
//    {
//        switch (op)
//        {
//            case 0: return prevvalue + intensity;
//            case 1: return prevvalue - intensity;
//            case 2: return prevvalue * intensity;
//            case 3: return prevvalue / intensity;
//            default: return prevvalue;
//        }
//    }
//}

public class Skill : MonoBehaviour {
    public Unit caster, target;
    public float cooldownTime = 5f; //default
    public float aoe = 300f; //default, in meters (?). if 0, either target only or whole battle field is affected (note: this is the radius.)
    public float duration = 5f; //default (in seconds)
	public int summonCount = 5; //default
    public GameObject skillAnimation, secondaryAnimation; //summon;
    public Texture2D icon, normalIcon, cooldownIcon;
    public string skillName;
    public bool isActive = true, isConstant = false; //

    public Effect[] effects;
    public LayerMask mask;

    private UnitManager manager;
    private faithHud hud;
    private List<Unit> affectedUnitsCache;
    private GameObject summon;

    //for reflection.
    private const BindingFlags flags = /*BindingFlags.NonPublic | */ BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

    void Awake()
    {
        manager = UnitManager.getInstance();
        hud = faithHud.getInstance();
        //Invoke("destroy", duration);
    }

    void Start()
    {
        icon = normalIcon;
		if(caster.getLayer() == 9) { //enemy caster
			int value = mask.value;
			if(value == 1 << 8) value = 1 << 9;
			if(value == 1 << 9) value = 1 << 8;
			//mask.value = ~mask.value; //invert bits...??
		}
		
        if (type == skillType.aura)
            StartCoroutine(CoStart());
    }

    IEnumerator CoStart()
    {
        //print ("CoStart() called.");
        while (true)
        {
            yield return StartCoroutine(CoUpdate());
        }
    }

    IEnumerator CoUpdate()
    {
        if (isConstant && Time.deltaTime > 0)
        {
            if (type == skillType.aura || (type == skillType.summon && skillName == "Ghost Busters")) applyEffectsOn(getAffectedUnits());
        }
        yield return new WaitForSeconds(duration);
        if(skillName == "Ghost Busters") isConstant = false;
    }

    public enum skillType
    {
        instant,    //
        aura,       //casts aura around caster
        target,     //requires a target
        summon,     //instantiates summonables
        ulti        //??? ULTIMATE SKILL. requires full faith bar (to be implemented laterz)
    };

    public skillType type;

    public Collider[] getAffectedUnits()
    {
        if (type == skillType.summon)
        {
            //GameObject summoned = manager.contraption; //GameObject.Find("summon-" + skillName);
            if (!summon) print("contraption not found");
            else print("contraption found, coords: " + summon.transform.position);
            if (aoe > 0 && summon != null) return Physics.OverlapSphere(summon.transform.position, aoe, mask.value);
        }
        if ((type == skillType.instant || type == skillType.aura)) //
        {
			if(aoe > 0)
            	return Physics.OverlapSphere(caster.transform.position, aoe, mask.value);
			else {
				if(mask.value == 1 << 8) return manager.getYourUnitColliders();
				if(mask.value == 1 << 9) return manager.getTheirUnitColliders();
				if(mask.value == (1 << 8 | 1 << 9)) return manager.getAllColliders();
			}
        }

        if (type == skillType.target && aoe > 0)
        {
            return Physics.OverlapSphere(target.transform.position, aoe, mask.value);
        }

        return null;
    }

    //
    public void applyEffectsOn(Unit target)
    {
        
        if (target)
        {
			//doesn't work -- oh wait it does
			//edit: deprecated
//            FieldInfo[] fields = target.GetType().GetFields(flags);

            foreach (Effect e in effects)
            {
				//deprecated
//                if (e.isBuff == false)
//                {
//                    foreach (FieldInfo f in fields)
//                    {
//                        if (e.effectName == f.Name) f.SetValue(target, e.result((float)f.GetValue(target)));
//                    }
//                }
//                else
//                {
                    Effect eInstance = Instantiate(e) as Effect;
                    if (!target.statusEffects.Contains(eInstance) && target.isStatic == false)
                    {
                        target.statusEffects.Add(eInstance);
                        eInstance.referencedUnits.Add(target);
                    }
//                }
            }
        }
    }

    //not very sure about this
    //edit: deprecated
    public void applyEffectsOn(List<Unit> affectedUnits)
    {
        foreach (Unit u in affectedUnits)
        {
            applyEffectsOn(u);
        }
    }

    //
    public void applyEffectsOn(Collider[] units)
    {
        foreach (Collider c in units)
        {
            Unit u = c.gameObject.GetComponent<Unit>();
            applyEffectsOn(u);
        }
    }

    public IEnumerator waitForInput(KeyCode k) {
        while (!Input.GetKeyDown(k))
        {
            yield return null;
        }
    }

    public IEnumerator cooldown()
    {
        for (float i = 0; i < cooldownTime; i+= Time.deltaTime)
        {
            yield return null;
        }
        icon = normalIcon;
        isActive = true;
    }

    public IEnumerator activate()
    {
		if(type == skillType.summon && isActive) {
			//instantiate summons
			float distanceFromCaster = 150; //1.5ft ?
			for(int i = 0; i < summonCount; i++) {
				float rad = 2 * Mathf.PI * i/summonCount;
				Vector3 summonPos = new Vector3(caster.transform.position.x + Mathf.Cos(rad) * distanceFromCaster, caster.transform.position.y + 100, caster.transform.position.z + Mathf.Sin(rad) * distanceFromCaster);
				GameObject familiar = OT.CreateObject("summon-"+skillName);
				familiar.transform.position = summonPos;
                familiar.layer = caster.getLayer();
                summon = familiar;
			}

            if (skillName == "Ghost Busters")
            {
                Collider[] units = getAffectedUnits();
                foreach (Collider c in units)
                {
                    GameObject obj = Instantiate(skillAnimation, c.gameObject.transform.position, c.gameObject.transform.rotation) as GameObject;
                    obj.transform.parent = c.gameObject.transform;
                }
                isConstant = true;
                applyEffectsOn(units);
            }

			icon = cooldownIcon;
            isActive = false;
            StartCoroutine(cooldown());
		}
        if (type == skillType.instant && isActive)
        {
            Collider[] units = getAffectedUnits();
            foreach (Collider c in units)
            {
                GameObject obj = Instantiate(skillAnimation, c.gameObject.transform.position, c.gameObject.transform.rotation) as GameObject;
                obj.transform.parent = c.gameObject.transform;
            }
            
			if(skillName == "Scary Face") {
				GameObject obj = Instantiate(secondaryAnimation, caster.gameObject.transform.position, caster.gameObject.transform.rotation) as GameObject;
                obj.transform.parent = caster.gameObject.transform;
			}
			
            applyEffectsOn(units);

            icon = cooldownIcon;
            isActive = false;
            StartCoroutine(cooldown());
        }
        if (type == skillType.aura && isActive)
        {
            GameObject obj = Instantiate(skillAnimation, caster.transform.position, caster.transform.rotation) as GameObject;
            obj.transform.parent = caster.gameObject.transform;

            isConstant = true;
            icon = cooldownIcon;
            isActive = false;
        }
        if (type == skillType.target && isActive)
        {
            if (hud)
            {
                hud.toggleUnitSelection = true;
                Time.timeScale = 0;
                yield return StartCoroutine(waitForInput(KeyCode.Return));
                hud.toggleUnitSelection = false;
                Time.timeScale = 1;
                target = hud.selectedUnit;
                if (target)
                {
                    //play animation
                    GameObject obj = Instantiate(skillAnimation, target.transform.position, target.transform.rotation) as GameObject;
                    obj.transform.parent = target.gameObject.transform;
                    //obj.GetComponent<Particles>().parentPos = target.transform;
                    applyEffectsOn(target);
                }
            }

            icon = cooldownIcon;
            isActive = false;
            StartCoroutine(cooldown());
        }
    }

    void Update()
    {
        //just to 
        if(type == skillType.aura)
            Debug.DrawLine(caster.transform.position, new Vector3(caster.transform.position.x + aoe, caster.transform.position.y, caster.transform.position.z));
    }

    void destroy()
    {
        DestroyObject(gameObject);
    }
}
