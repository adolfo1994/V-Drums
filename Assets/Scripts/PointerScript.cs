using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PointerScript : MonoBehaviour {

	private CardboardHead head;
	private RaycastHit rayHit;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {    
		head = Camera.main.GetComponent<StereoController>().Head;
		lr = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		Physics.Raycast (ray.origin, ray.direction, out rayHit);
		Debug.Log (head.Gaze);
		lr.SetVertexCount (2);
		/*lr.SetPosition (0, 
		                new Vector3(
							rayHit.point.x,
							rayHit.point.y,
							rayHit.point.z + 0.01f));
		*/
		lr.SetPosition (0, ray.origin);
		lr.SetPosition (1, rayHit.point);




		
	}
}
