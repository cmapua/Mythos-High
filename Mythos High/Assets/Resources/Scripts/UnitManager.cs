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
		print (getYourUnits());
		print (getTheirUnits());
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
