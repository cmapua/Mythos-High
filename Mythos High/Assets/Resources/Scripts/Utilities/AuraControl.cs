using UnityEngine;
using System.Collections;

public class AuraControl : MonoBehaviour {
    public Projector projector;
    //bool playBirthAnimation = true;
	// Use this for initialization
	void Start () {
        //StartCoroutine(birthAnimation());
	}

    IEnumerator birthAnimation()
    {
        Vector3 prevPos = projector.transform.position;
        Vector3 currPos = new Vector3(prevPos.x, 0, prevPos.z);
        float str = Mathf.Min(5f * Time.deltaTime, 1);
        for (float i = 0; i < 5f; i += Time.deltaTime)
        {
            projector.transform.position = Vector3.Lerp(currPos, prevPos, str);
            yield return null;
        }
    }
	// Update is called once per frame
	void Update () {
        projector.transform.Rotate(0, 0, 5f * Time.deltaTime);
	}
}
