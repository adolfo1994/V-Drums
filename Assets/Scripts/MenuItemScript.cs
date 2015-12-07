using UnityEngine;
using System.Collections;

public enum ButtonType{
	play,restart,song,ambient, nextSong,previousSong
}
public class MenuItemScript: MonoBehaviour {


	public Material inactiveMaterial;
	public Material activeMaterial;
	public ButtonType buttonType;
	public GameObject pauseMenu;
    public GameObject drums;

	private MetronomeScript metronome;
	private Transform originalParent;
	private CardboardHead head;
	private Vector3 startingPosition;
	private float delay = 0.0f; 
	private Transform buttonText;
	private GameObject song;
    private UIScript ui;

	
	void Start() {
		head = Camera.main.GetComponent<StereoController>().Head;
		originalParent = pauseMenu.transform.parent;
		buttonText = transform.GetChild (1);
		buttonText.gameObject.SetActive (false);
		song = GameObject.FindWithTag("Song");
		metronome = FindObjectOfType<MetronomeScript>();
		ui = FindObjectOfType<UIScript>();
	}
	
	void Update() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);

		if (isLookedAt) { 
			buttonText.gameObject.SetActive(true);
			if(Cardboard.SDK.Triggered){
				switch(buttonType){
					case ButtonType.play:
						pauseMenu.SetActive(false);
						pauseMenu.transform.SetParent(originalParent);
                        drums.SetActive(true);
						if (!metronome.playing){
							metronome.TogglePlay();
						}
                        ui.SendInstructions("Clocks - Coldplay");
						break;
					case ButtonType.restart:
						pauseMenu.transform.SetParent(originalParent);
                        pauseMenu.SetActive(false);
                        metronome.Init();
                        metronome.TogglePlay();
                        drums.SetActive(true);
                        ui.SendInstructions("Clocks - Coldplay");
						break;
					case ButtonType.song:
						break;
					case ButtonType.previousSong:
						ui.SendInstructions("No hay mas canciones disponibles");
						break;
					case ButtonType.nextSong:
						ui.SendInstructions("No hay mas canciones disponibles");
						break;
					case ButtonType.ambient:
						break;
				}
			}
			Debug.Log("Looked");
			GetComponent<Renderer>().material = activeMaterial; 
			transform.localScale.Set(3.2f, 3.2f, 1);

		} else if(!isLookedAt) { 
			Debug.Log("Not Looked");
			buttonText.gameObject.SetActive(false);
			GetComponent<Renderer>().material = inactiveMaterial; 
			transform.localScale.Set(3, 3, 1); 
		}
	}
	
}
