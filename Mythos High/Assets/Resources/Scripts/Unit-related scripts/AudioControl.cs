using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {
	public AudioClip hitClip, deathClip;
	private static AudioControl instance;
	
	public static AudioControl getInstance() {
		if(instance == null) 
			instance = (AudioControl)FindObjectOfType(typeof(AudioControl));
		return instance;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
