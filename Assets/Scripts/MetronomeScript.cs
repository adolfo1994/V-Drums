using System;
using UnityEngine;
using Vuforia;

public class MetronomeScript : MonoBehaviour, IVirtualButtonEventHandler {

    public float frequency, songTimer, dialSpeed;
    public GameObject[] lights;
    public AudioSource song, kick;
    public int quarters;
    public GameObject dial;

    private float timer, kickTimer, delay;
    private GameObject button, metronome;
    private int counter, kickCounter, superCounter;
    private bool metronomeActive;
    private bool playing, kickActive;

    void Start() {
        delay = .26f;
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        counter = 3;
        metronome = transform.Find("Metronome").gameObject;
        button = transform.Find("Button").gameObject;

        song.PlayDelayed(songTimer);
        timer = songTimer + delay;
        kickTimer = frequency * 40; //enough
        kickCounter = 0;
        superCounter = 0;
        metronomeActive = true;
        playing = true;
        kickActive = true;
    }

    void FixedUpdate() {
        if (!playing)
            return;
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            lights[counter++].SetActive(false);
            if ((counter & 3) == 0)
            {
                if (++superCounter > 8)
                {
                    kickTimer = Time.fixedDeltaTime;
                    kickCounter = 0;
                }
                counter = 0;
            }
            lights[counter].SetActive(true);
            timer += frequency;
        }
        kickTimer -= Time.fixedDeltaTime;
        if (kickTimer <= 0)
        {
            if (kickActive)
                kick.Play();
            switch (kickCounter)
            {
                case 0:
                case 2: kickTimer += frequency * .475f; break;
                case 1: kickTimer += frequency * .9f; break;
                case 3: kickTimer += frequency * 1.17f; break;
                case 4: kickTimer += frequency; break;
            }
            ++kickCounter;
        }
    }

    void Update()
    {
        dial.transform.localPosition = new Vector3(-.315f + song.time * dialSpeed,  0, -.129f);
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronomeActive = !metronomeActive;
        metronome.SetActive(metronomeActive);
        button.GetComponent<Renderer>().material.SetColor("_Color",
            metronomeActive ? new Color(.23f, .7f, .11f) : new Color(.5f, .7f, .5f));
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }

    public void TogglePlay()
    {
        playing = !playing;
        if (playing)
            song.UnPause();
        else
            song.Pause();
    }

    public void ToogleKick()
    {
        kickActive = !kickActive;
    }

    public void SetStart(int point)
    {
        song.time = frequency * point * 4 * quarters;
        timer = delay;
        counter = 3;
        kickTimer = delay;
        kickCounter = 0;
        superCounter = 9;
        if (point == 0)
        {
            kickTimer = frequency * 40; //enough
            superCounter = 0;
        }
    }
}
