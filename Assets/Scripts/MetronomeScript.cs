using System;
using UnityEngine;
using Vuforia;

public class MetronomeScript : MonoBehaviour, IVirtualButtonEventHandler {

    public float frequency, timer, songTimer;
    public GameObject[] lights;
    public AudioSource song;

    private GameObject button, metronome;
    private int counter;
    private bool songStarted;
    private bool metronomeActive = true;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        counter = 3;
        songStarted = false;
        metronome = transform.Find("Metronome").gameObject;
        button = transform.Find("Button").gameObject;
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

        songTimer -= Time.fixedDeltaTime;
        if (songTimer <= 0 && !songStarted)
        {
            song.Play();
            songStarted = true;
        }
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronomeActive = !metronomeActive;
        metronome.SetActive(metronomeActive);
        button.GetComponent<Renderer>().material.SetColor("_Color", metronomeActive ? Color.green : Color.red);
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }
}
