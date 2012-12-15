using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {
	private List<Unit> yourUnits;
	private List<Unit> theirUnits;
	public const int yourUnitLayer = 8, theirUnitLayer = 9;

	//singleton design pattern
	private static UnitManager instance;

	public static UnitManager getInstance() {
		if(instance == null) 
			instance = (UnitManager)FindObjectOfType(typeof(UnitManager));
		return instance;
	}

	// Use this for initialization
	void Awake () {
		//instantiate lists
		yourUnits = new List<Unit>();
		theirUnits = new List<Unit>();
	}

	public void addUnit(Unit u) {
		switch(u.getLayer()) {
			case yourUnitLayer:
				yourUnits.Add(u);
				break;
			case theirUnitLayer:
				theirUnits.Add(u);
				break;
			default:
				print("illegal unit");
				break;
		}
	}

	public void removeUnit(Unit u) {
		switch(u.getLayer()) {
			case yourUnitLayer:
				if(yourUnits.Contains(u)) yourUnits.Remove(u);
				break;
			case theirUnitLayer:
				if(theirUnits.Contains(u)) theirUnits.Remove(u);
				break;
			default:
				print("no units to remove");
				break;
		}
	}
	
	public List<Unit> getYourUnits() {
		return yourUnits;
	}
	
	public List<Unit> getTheirUnits() {
		return theirUnits;
	}
	
	public Unit SearchForUnit(OTObject o, int searchLayer) {
		if(searchLayer == 8) { //looking for player sprite
			foreach(Unit u in yourUnits) {
				if(u.mmc.sprite.name == o.name)
					return u;
			}
		}
		if(searchLayer == 9) { //looking for player sprite
			foreach(Unit u in theirUnits) {
				if(u.mmc.sprite.name == o.name)
					return u;
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
