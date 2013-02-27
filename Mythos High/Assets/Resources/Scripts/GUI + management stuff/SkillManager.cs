using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour {
    //singleton design pattern
    private static SkillManager instance;

    //private UnitManager manager;

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
        //manager = UnitManager.getInstance();
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < playerSkills.Length; i++)
        {
            playerSkills[i] = Instantiate(playerSkills[i]) as Skill;
            playerSkills[i].caster = playerCaster;
        }
        for (int i = 0; i < enemySkills.Length; i++)
        {
            enemySkills[i] = Instantiate(enemySkills[i]) as Skill;
            enemySkills[i].caster = enemyCaster;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //if (Time.deltaTime > 0)
        //{
        //    if (Input.GetKeyUp(KeyCode.X))
        //    {
        //        activateSkill(0);
        //    }
        //}
	}

    public void activateSkill(int index)
    {
        //Skill s = Instantiate(playerSkills[index]) as Skill;
        //s.isActive = true;
        StartCoroutine(playerSkills[index].activate());
    }
}
