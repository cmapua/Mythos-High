using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[System.Serializable]
public class Effect
{
    public string name;
    public float intensity;
    //public int affectedLayer;
    public int op;

    public float result(float prevvalue)
    {
        switch (op)
        {
            case '0': return prevvalue + intensity;
            case '1': return prevvalue - intensity;
            case '2': return prevvalue * intensity;
            case '3': return prevvalue / intensity;
            default: return prevvalue;
        }
    }
}

public class Skill : MonoBehaviour {
    public Unit caster, target;
    public float cooldown = 5f; //default
    public float aoe = 3f; //default, in meters (?). if 0, either target only or whole battle field is affected
    public float duration = 5f; //default (in seconds)
    public GameObject skillAnimation;
    public bool isActive = false; //

    public Effect[] effects;

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

    public enum skillType
    {
        instant,    //like BOOM.
        aura,       //casts aura around caster
        target,     //requires a target
        summon,     //instantiates summonables
        ulti        //??? ULTIMATE SKILL. requires full faith bar (to be implemented laterz)
    };

    public skillType type;

    public List<Unit> getAffectedUnits()
    {
        List<Unit> units = null;

        if ((type == skillType.instant || type == skillType.aura) && aoe > 0) //
        {
            foreach (Unit u in manager.getTheirUnits())
            {
                if (Vector3.Distance(caster.transform.position, u.transform.position) < aoe) units.Add(u);
            }
            foreach (Unit u in manager.getYourUnits())
            {
                if (Vector3.Distance(caster.transform.position, u.transform.position) < aoe) units.Add(u);
            }
        }

        if (type == skillType.target && aoe > 0)
        {
            foreach (Unit u in manager.getTheirUnits())
            {
                if (Vector3.Distance(target.transform.position, u.transform.position) < aoe) units.Add(u);
            }
            foreach (Unit u in manager.getYourUnits())
            {
                if (Vector3.Distance(target.transform.position, u.transform.position) < aoe) units.Add(u);
            }
        }

        return units;
    }

    //
    public void applyEffectsOn(Unit target)
    {
        //doesn't work
        //FieldInfo[] fields = target.GetType().GetFields(flags);

        foreach (Effect e in effects)
        {
            //if (e.name == "HP") //affect HP
                target.HP = e.result(target.HP);
        }
    }

    //not very sure about this
    public void applyEffectsOn(List<Unit> affectedUnits)
    {
        foreach (Unit u in affectedUnits)
        {
            applyEffectsOn(u);
        }
    }

    public IEnumerator waitForInput(KeyCode k) {
        while (!Input.GetKeyDown(k))
        {
            yield return null;
        }
    }

    public IEnumerator activate()
    {
        if (isActive)
        {
            if (type == skillType.target)
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

                isActive = false;
                //destroy();
            }
        }
    }

    void Update()
    {
        if (isActive && Time.deltaTime > 0)
        {

        }
    }

    void destroy()
    {
        DestroyObject(gameObject);
    }
}
