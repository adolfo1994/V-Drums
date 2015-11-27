using System;
using UnityEngine;
using Vuforia;

public class MetronomeScript : MonoBehaviour, IVirtualButtonEventHandler {

    public float frequency, songTimer;
    public GameObject[] lights;
    public AudioSource song, kick;

    private float timer, kickTimer;
    private GameObject button, metronome;
    private int counter, kickCounter, superCounter;
    private bool metronomeActive = true;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        counter = 3;
        metronome = transform.Find("Metronome").gameObject;
        button = transform.Find("Button").gameObject;
        song.PlayDelayed(songTimer);
        timer = songTimer + .26f;
        kickTimer = songTimer * 100; //enough
        kickCounter = 0;
        superCounter = 0;
	}

	void FixedUpdate () {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            lights[counter++].SetActive(false);
            if ((counter & 3) == 0)
            {
                if (++superCounter > 8)
                    kickTimer = Time.fixedDeltaTime;
                counter = 0;
            }
            lights[counter].SetActive(true);
            timer += frequency;
        }
        kickTimer -= Time.fixedDeltaTime;
        if (kickTimer <= 0)
        {
            kick.Play();
            switch (kickCounter)
            {
                case 0:
                case 2: kickTimer += frequency * .475f; break;
                case 1: kickTimer += frequency * .9f; break;
                case 3: kickTimer += frequency * 1.17f; break;
                case 4: kickTimer += frequency; break;
            }
            if (++kickCounter == 5)
                kickCounter = 0;
        }
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronomeActive = !metronomeActive;
        metronome.SetActive(metronomeActive);
        button.GetComponent<Renderer>().material.SetColor("_Color",
            metronomeActive ? new Color(.23f, .7f, .11f) : new Color(.6f, .7f, .6f));
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }
}
