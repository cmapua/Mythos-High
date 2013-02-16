using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour {
    //singleton design pattern
    private static SkillManager instance;

    private UnitManager manager;

    public Unit playerCaster, enemyCaster;
    public Skill[] playerSkills, enemySkills;

    public static SkillManager getInstance()
    {
        if (instance == null)
            instance = (SkillManager)FindObjectOfType(typeof(SkillManager));
        return instance;
    }

    void Awake()
    {
        manager = UnitManager.getInstance();
    }

	// Use this for initialization
	void Start () {
        foreach (Skill s in playerSkills)
        {
            s.caster = playerCaster;
        }
        foreach (Skill s in enemySkills)
        {
            s.caster = enemyCaster;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                Skill s = Instantiate(playerSkills[0]) as Skill;
                StartCoroutine(s.activate());
                print("player cast skill " + s.name + "!");
            }
        }
	}
}
