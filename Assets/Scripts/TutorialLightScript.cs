using UnityEngine;
using System.Collections;

public class TutorialLightScript : MonoBehaviour {

	public float max_height;
	private float base_height;
	private float pos_delta;
	public bool done;
	// Use this for initialization

	void Start () {
		done = true;
		base_height = transform.localPosition.y;
		transform.localPosition = new Vector3(
			transform.localPosition.x,
			max_height,
			transform.localPosition.z
		);

		pos_delta = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!done) {
			if ((transform.localPosition.y - pos_delta) < base_height) {
				done = true;
				transform.localPosition = new Vector3 (
				transform.localPosition.x,
				max_height,
				transform.localPosition.z
				);
			} else {
				transform.localPosition = new Vector3 (
				transform.localPosition.x,
				transform.localPosition.y - pos_delta,
				transform.localPosition.z
				);
			}
		}
	}
	public void Appear(){
		done = false;
		GetComponent<Light> ().intensity = 8;
	}
}
