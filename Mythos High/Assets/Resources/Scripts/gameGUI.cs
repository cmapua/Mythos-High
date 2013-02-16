using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameGUI : MonoBehaviour {
	private UnitManager manager;
	public Texture2D hpBackIcon, hpIcon, hpEnemyIcon;
	public float hp_w = 60; 
	public float hp_h = 10;
	public float hp_yOffset = 120;
	private static gameGUI instance;
	public bool dialogue = true;

    //moved this to faithHud

    //public bool toggleUnitSelection = false;
    //private float mover = 0;

    //void displayUnitSelectionWindow()
    //{
    //    //Time.timeScale = 0f; //pause action
    //    generateButtons(manager.getYourUnits(), Screen.width / 2, Screen.height / 2, false, mover);
    //}

    //void generateButtons(List<Unit> units, float x, float y, bool easing, float direction)
    //{
    //    float yOffset = 20f;
    //    GUI.BeginGroup(new Rect(x, y, 60, 100)); //show at most 5 units at a time
    //    for (int i = 0; i < units.Count; i++)
    //    {
    //        GUI.Box(new Rect(0, (yOffset * i) + direction, 50, 20), units[i].name);
    //    }
    //    GUI.EndGroup();
    //}

    //void Update()
    //{
    //    if (toggleUnitSelection)
    //    {
    //        if (Input.GetKeyUp(KeyCode.L))
    //        {
    //            if (toggleUnitSelection) toggleUnitSelection = false;
    //            else toggleUnitSelection = true;
    //        }
    //        if (Input.GetKeyUp(KeyCode.UpArrow)) mover = 20f;
    //        else if (Input.GetKeyUp(KeyCode.DownArrow)) mover = -20f;
    //        else mover = 0;
    //    }
    //}

	public static gameGUI getInstance() {
		if(instance == null) 
			instance = (gameGUI)FindObjectOfType(typeof(gameGUI));
		return instance;
	}
	
	
	void Awake() {
		manager = UnitManager.getInstance();
	}

	void OnGUI() {
		if(!dialogue) {
			foreach(Unit u in manager.getYourUnits()) {
				showHP(u);
			}
			foreach(Unit u in manager.getTheirUnits()) {
				showHP(u);
			}
		}
	}



	void showHP(Unit u) {
		//Vector3 offset = new Vector3(u.transform.position.x, u.transform.position.y + 80, u.transform.position.z);
		Vector3 center = Camera.main.WorldToScreenPoint(u.transform.position);
        Rect HPLoc = new Rect(center.x - hp_w/2, Screen.height - center.y - hp_yOffset, hp_w, hp_h);
        GUI.DrawTexture(HPLoc, hpBackIcon, ScaleMode.StretchToFill, true, 10f);
        float newWidth = HPLoc.width * (u.HP / u.maxHP);
		if(u.getLayer() == 8)
        	GUI.DrawTexture(new Rect(HPLoc.xMin, HPLoc.yMin, newWidth, HPLoc.height), hpIcon, ScaleMode.StretchToFill, true, 10f);
		if(u.getLayer() == 9)
			GUI.DrawTexture(new Rect(HPLoc.xMin, HPLoc.yMin, newWidth, HPLoc.height), hpEnemyIcon, ScaleMode.StretchToFill, true, 10f);
        //GUI.Label(new Rect(HPLoc.xMin, HPLoc.yMin, HPLoc.width + 5, 50), u.HP + "/" + u.maxHP);
	}
}
