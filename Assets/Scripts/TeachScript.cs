using UnityEngine;
using Vuforia;

public class TeachScript : MonoBehaviour, IVirtualButtonEventHandler {

    private MetronomeScript metronome;
    private GameObject button;
    private bool playing;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        metronome = FindObjectOfType<MetronomeScript>();
        button = transform.FindChild("Button").gameObject;
        
        playing = true;
    }

    void FixedUpdate () {
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        button.SetActive(false);
        playing = !playing;
        button.GetComponent<Renderer>().material.SetColor("_Color",
            playing ? new Color(.23f, .7f, .11f) : new Color(.5f, .7f, .5f));
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        button.SetActive(true);
    }

}
