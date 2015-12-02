using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject pauseMenu;
    public GameObject drums;
    public TextMesh instructions;

	private CardboardHead head;
	private GameObject song;
	private MetronomeScript metronome;
    private float instructionsTimer;

	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		pauseMenu.SetActive (false);
		song = GameObject.FindWithTag("Song");
		metronome = FindObjectOfType<MetronomeScript>();

		Debug.Log ("Setting active false");
        instructionsTimer = float.MinValue;
        SendInstructions("Tap para ir al menú");
	}
	
	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered && !pauseMenu.activeSelf) {
            SendInstructions("Gira tu cabeza para seleccionar");
            if (metronome.playing){
                metronome.TogglePlay();
            }
            drums.SetActive(false);
            pauseMenu.transform.SetParent(null);
            pauseMenu.SetActive(true);
            Debug.Log ("Setting active false");
		}
        if (instructionsTimer > 0)
        {
            instructionsTimer -= Time.deltaTime;
            if (instructionsTimer <= 0)
                instructions.text = "";
        }
	}

    public void SendInstructions (string text, float time = 4)
    {
        instructions.text = text;
        instructionsTimer = time;
    }
}

