﻿using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject pauseMenu;

	private bool showingMenu;
	private CardboardHead head;


	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		pauseMenu.SetActive (false);
		showingMenu = false;
		Debug.Log ("Setting active false");
	}
	
	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered) {
			if(showingMenu){
				pauseMenu.transform.parent = head.transform;
				pauseMenu.SetActive(false);
				showingMenu = false;
				Debug.Log("Bye Menu");
			}else{
				pauseMenu.transform.parent = null;
				pauseMenu.SetActive(true);
				showingMenu = true;
				Debug.Log ("Setting active false");
			}

		}
	}
}