using UnityEngine;
using System.Collections;

public class DamageControl : MonoBehaviour {
	public bool activate = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.gameObject.layer == 9) {
			Unit u = c.gameObject.GetComponent<Unit>();
			//if(activate) {
				u.HP -= 20;
				activate = false;
			//}
		}
	}
}
