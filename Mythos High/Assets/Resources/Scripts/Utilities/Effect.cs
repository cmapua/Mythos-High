using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Effect : MonoBehaviour
{
    public string effectName;
    public float intensity;
    //public int affectedLayer;
    public int op;
    public bool isBuff = false;
    public float buffDuration = 1.5f;

    public List<Unit> referencedUnits;
    float[] prevValues;

    void Start() 
    {
        prevValues = new float[referencedUnits.Count];
        for(int i = 0; i < referencedUnits.Count; i++)
        {
            Unit u = referencedUnits[i];
            if (effectName == "damage")
            {
                prevValues[i] = u.damage;
                u.damage = u.damage + intensity;
            }
            if (effectName == "HP")
            {
                u.HP += u.maxHP * intensity;
                if (u.HP > u.maxHP) u.HP = u.maxHP;
            }
            if (effectName == "def")
            {
                prevValues[i] = u.def;
                u.def += u.maxHP * intensity;
            }
			if(effectName == "physical dmg") {
				u.HP -= intensity;
			}
			if(effectName == "stun") {
				prevValues[i] = u.moveSpeed;
				u.moveSpeed = 0;
			}
            if (effectName == "slow")
            {
                prevValues[i] = u.moveSpeed;
                u.moveSpeed = u.moveSpeed * intensity;
            }
        }
        Invoke("destroy", buffDuration);
    }

    void destroy() {
        for(int i = 0; i < referencedUnits.Count; i++)
        {
            Unit u = referencedUnits[i];
            if(u.statusEffects.Contains(this))
            {
                if (effectName == "damage") u.damage = prevValues[i];
                if (effectName == "def") u.def = prevValues[i];
				if (effectName == "stun" || effectName == "slow") u.moveSpeed = prevValues[i];
                u.statusEffects.Remove(this);
            }
        }
        DestroyObject(gameObject);
    }

    public float result(float prevvalue)
    {
        switch (op)
        {
            case 0: return prevvalue + intensity;
            case 1: return prevvalue - intensity;
            case 2: return prevvalue * intensity;
            case 3: return prevvalue / intensity;
            default: return prevvalue;
        }
    }
}
