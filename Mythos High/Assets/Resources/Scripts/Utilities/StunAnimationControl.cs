using UnityEngine;
using System.Collections;

public class StunAnimationControl : MonoBehaviour {
	//OTSprite stunSprite;
	// Use this for initialization
	public Texture2D stunIcon;
	public float duration = 1f;
	void Start () {
		Invoke("destroy", duration);
	}
	
	void OnGUI() {
		int iconSize = 64, offset = 190;
		Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
		GUI.DrawTexture(new Rect(center.x - iconSize/2, Screen.height - center.y - offset, iconSize, iconSize), stunIcon);
	}
	
	void destroy() {
		Destroy(gameObject);
	}
}
