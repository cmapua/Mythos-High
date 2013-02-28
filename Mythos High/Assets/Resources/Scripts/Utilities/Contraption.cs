using UnityEngine;
using System.Collections;

public class Contraption : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Invoke("destroy", 4);
	}

    void destroy()
    {
        DestroyObject(gameObject);
    }
}
