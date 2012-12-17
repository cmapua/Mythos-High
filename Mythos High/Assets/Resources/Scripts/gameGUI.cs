using UnityEngine;
using System.Collections;

public class gameGUI : MonoBehaviour {
	private UnitManager manager;
	public Texture2D hpBackIcon, hpIcon, hpEnemyIcon;
	public float hp_w = 60; 
	public float hp_h = 10;
	public float hp_yOffset = 120;
	private static gameGUI instance;
	public bool dialogue = true;
	
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
