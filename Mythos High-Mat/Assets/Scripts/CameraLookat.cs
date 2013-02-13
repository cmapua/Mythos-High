using UnityEngine;
using System.Collections;

public class CameraLookat : MonoBehaviour {
	public Transform target;
	//cached
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//fancy smooth lookAt
		Quaternion lookRotation = Quaternion.LookRotation(target.position - myTransform.position);
		float str = Mathf.Min(5 * Time.deltaTime, 1);
		myTransform.rotation = Quaternion.Lerp(myTransform.rotation, lookRotation, str);
	}
}
