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
//        for (int i = 0; i < playerSkills.Length; i++)
//        {
//            playerSkills[i] = new Skill();
//            playerSkills[i].caster = playerCaster;
//        }
//        for (int i = 0; i < enemySkills.Length; i++)
//        {
//            enemySkills[i] = new Skill();
//            enemySkills[i].caster = enemyCaster;
//        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                Skill s = Instantiate(playerSkills[0]) as Skill;
				s.isActive = true;
                StartCoroutine(s.activate());
                //print("player cast skill " + s.name + "!");
                //playerSkills[0].isActive = true;
				//StartCoroutine(playerSkills[0].activate());
            }
        }
	}
}
