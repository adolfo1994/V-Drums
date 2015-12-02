using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject pauseMenu;

	private bool showingMenu;
	private CardboardHead head;
	private GameObject song;
	private MetronomeScript metronome;

	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		pauseMenu.SetActive (false);
		showingMenu = false;
		song = GameObject.FindWithTag("Song");
		metronome = FindObjectOfType<MetronomeScript>();

		Debug.Log ("Setting active false");
	}
	
	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered) {
				if(metronome.playing){
					metronome.TogglePlay();
				}
				pauseMenu.transform.SetParent(null);
				pauseMenu.SetActive(true);
				showingMenu = true;
				Debug.Log ("Setting active false");
		}
	}
}

