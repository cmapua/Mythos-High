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
    public GameObject skillAnimation;
    public Texture2D icon, normalIcon, cooldownIcon;
    public string skillName;
    public bool isActive = true; //

    public Effect[] effects;
    public LayerMask mask;

    private UnitManager manager;
    private faithHud hud;
    private List<Unit> affectedUnitsCache;

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

        //for (int i = 0; i < effects.Length; i++ )
        //{
        //    effects[i] = Instantiate(effects[i]) as Effect;
        //}

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
        if (isActive && Time.deltaTime > 0)
        {
            if (type == skillType.aura) applyEffectsOn(getAffectedUnits());
        }
        yield return new WaitForSeconds(duration);
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
        //List<Unit> units = null;

        if ((type == skillType.instant || type == skillType.aura) && aoe > 0) //
        {
            //deprecated
            //foreach (Unit u in manager.getTheirUnits())
            //{
            //    if (Vector3.Distance(caster.transform.position, u.transform.position) < aoe) units.Add(u);
            //}
            //foreach (Unit u in manager.getYourUnits())
            //{
            //    if (Vector3.Distance(caster.transform.position, u.transform.position) < aoe) units.Add(u);
            //}

            return Physics.OverlapSphere(caster.transform.position, aoe, mask.value);
        }

        if (type == skillType.target && aoe > 0)
        {
            //foreach (Unit u in manager.getTheirUnits())
            //{
            //    if (Vector3.Distance(target.transform.position, u.transform.position) < aoe) units.Add(u);
            //}
            //foreach (Unit u in manager.getYourUnits())
            //{
            //    if (Vector3.Distance(target.transform.position, u.transform.position) < aoe) units.Add(u);
            //}

            return Physics.OverlapSphere(target.transform.position, aoe, mask.value);
        }

        return null;
    }

    //
    public void applyEffectsOn(Unit target)
    {
        //doesn't work -- oh wait it does
        if (target)
        {
            FieldInfo[] fields = target.GetType().GetFields(flags);

            foreach (Effect e in effects)
            {
                if (e.isBuff == false)
                {
                    foreach (FieldInfo f in fields)
                    {
                        if (e.effectName == f.Name) f.SetValue(target, e.result((float)f.GetValue(target)));
                    }
                }
                else
                {
                    Effect eInstance = Instantiate(e) as Effect;
                    //Effect eInstance = Instantiate(e) as Effect;
                    if (!target.statusEffects.Contains(eInstance) && target.isStatic == false)
                    {
                        target.statusEffects.Add(eInstance);
                        eInstance.referencedUnits.Add(target);
                    }

                }
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
        if (type == skillType.instant && isActive)
        {
            Collider[] units = getAffectedUnits();
            foreach (Collider c in units)
            {
                GameObject obj = Instantiate(skillAnimation, c.gameObject.transform.position, c.gameObject.transform.rotation) as GameObject;
                obj.transform.parent = c.gameObject.transform;
            }
            
            applyEffectsOn(units);

            icon = cooldownIcon;
            isActive = false;
            StartCoroutine(cooldown());
        }
        if (type == skillType.aura)
        {
            isActive = true;
            GameObject obj = Instantiate(skillAnimation, caster.transform.position, caster.transform.rotation) as GameObject;
            obj.transform.parent = caster.gameObject.transform;

            icon = cooldownIcon;
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
