using UnityEngine;
using System.Collections;

public class StunAnimationControl : MonoBehaviour {
	//OTSprite stunSprite;
	// Use this for initialization
	public Texture2D stunIcon, maskIcon;
	public Transform faceVector;
	public bool mask = false;
	public float duration = 1f;
	void Start () {
		Invoke("destroy", duration);
	}
	
	void OnGUI() {
		int iconWidth = 128, iconHeight = 64, offset = 170;
		Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
		if(mask) {
			center = Camera.main.WorldToScreenPoint(faceVector.position);
			iconWidth = maskIcon.width/2;
			iconHeight = maskIcon.height/2;
			GUI.DrawTexture(new Rect(center.x - iconWidth/2, Screen.height - center.y - iconHeight/2, iconWidth, iconHeight), maskIcon);
		} else {
			GUI.DrawTexture(new Rect(center.x - iconWidth/2, Screen.height - center.y - offset, iconWidth, iconHeight), stunIcon);
		}
	}
	
	void destroy() {
		Destroy(gameObject);
	}
}
