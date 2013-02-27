using UnityEngine;
using System.Collections;

public class CameraLookat : MonoBehaviour {
	public Transform target, defaultTarget;
	public float rotSpeed = 5;
	public float transSpeed = 2;
	//cached
	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.deltaTime > 0)
        {
            //fancy smooth lookAt
            Quaternion lookRotation = Quaternion.LookRotation(target.position - myTransform.position);
            float str = Mathf.Min(transSpeed * Time.deltaTime, 1);
            float rotStr = Mathf.Min(rotSpeed * Time.deltaTime, 1);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, lookRotation, rotStr);

            //fancy smooth follow
            Vector3 xDiff = new Vector3(target.position.x, myTransform.position.y, myTransform.position.z);
            myTransform.position = Vector3.Lerp(myTransform.position, xDiff, str);
        }
        else
        {
            myTransform.position = new Vector3(target.position.x, myTransform.position.y, myTransform.position.z);
        }
	}
}
