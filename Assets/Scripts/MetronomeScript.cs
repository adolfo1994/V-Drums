using System;
using UnityEngine;
using Vuforia;

public class MetronomeScript : MonoBehaviour, IVirtualButtonEventHandler {

    public float frequency, timer, songTimer;
    public GameObject[] lights;
    public AudioSource song;

    private GameObject button, metronome;
    private int counter;
    private bool metronomeActive = true;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        counter = 3;
        metronome = transform.Find("Metronome").gameObject;
        button = transform.Find("Button").gameObject;
        song.PlayDelayed(songTimer);
	}

	void FixedUpdate () {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            lights[counter++].SetActive(false);
            if (counter == 4)
                counter = 0;
            lights[counter].SetActive(true);
            timer += frequency;
        }
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronomeActive = !metronomeActive;
        metronome.SetActive(metronomeActive);
        button.GetComponent<Renderer>().material.SetColor("_Color",
            metronomeActive ? new Color(.23f, .7f, .11f) : new Color(.8f, 0, 0));
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }
}
