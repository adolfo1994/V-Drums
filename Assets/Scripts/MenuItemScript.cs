using UnityEngine;
using System.Collections;

public class MenuItemScript: MonoBehaviour {
	
	private CardboardHead head;
	private Vector3 startingPosition;
	private float delay = 0.0f; 
	
	void Start() {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
	}
	
	void Update() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
		if (isLookedAt) { 
			Debug.Log("Looked");
			GetComponent<Renderer>().material.color = Color.red; 
			transform.localScale.Set(3.2f, 3.2f, 1);

		} 
		else if (!isLookedAt) { 
			Debug.Log("Not Looked");

			GetComponent<Renderer>().material.color = Color.yellow; 
			delay = Time.time + 2.0f; 
			transform.localScale.Set(3, 3, 1); 
		}
	}
	
}
