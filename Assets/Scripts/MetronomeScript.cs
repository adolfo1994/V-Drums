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
    private bool metronomeActive, kickActive;
    public bool playing { get; private set; }

    void Start() {
        delay = .26f;
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        metronome = transform.Find("Metronome").gameObject;
        button = transform.Find("Button").gameObject;

        Init();
        metronomeActive = true;
        kickActive = true;
    }

    void Init()
    {
        counter = 3;
        kickTimer = timer = float.MaxValue;
        kickCounter = 0;
        superCounter = 0;
        playing = false;
        for (int i = 0; i < 4; ++i)
            lights[i].SetActive(false);
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
                case 0: kickTimer += frequency * .47f; break;
                case 1: kickTimer += frequency * 1.03f; break;
                case 2: kickTimer += frequency * .49f; break;
                case 3: kickTimer += frequency * 1.01f; break;
                case 4: kickTimer += frequency; break;
            }
            ++kickCounter;
        }
        if (!song.isPlaying && playing)
            Init();
    }

    void Update()
    {
        dial.transform.localPosition = new Vector3(-.315f + song.time * dialSpeed,  0, -.129f);
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronomeActive = !metronomeActive;
        metronome.SetActive(metronomeActive);
        button.SetActive(false);
        button.GetComponent<Renderer>().material.SetColor("_Color",
            metronomeActive ? new Color(.23f, .7f, .11f) : new Color(.5f, .7f, .5f));
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        button.SetActive(true);
    }

    public void TogglePlay()
    {
        playing = !playing;
        if (playing)
        {
            if (!song.isPlaying)
            {
                song.Play();
                timer = delay;
            }
            song.UnPause();
        } else
        {
            song.Pause();
        }
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
            kickTimer = float.MaxValue;
            superCounter = 0;
        }
    }
}
