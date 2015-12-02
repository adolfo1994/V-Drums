using UnityEngine;
using Vuforia;

public class ControlPauseScript : MonoBehaviour, IVirtualButtonEventHandler {

    public Sprite play, pause;

    private MetronomeScript metronome;
    private GameObject button;
    private SpriteRenderer buttonIcon;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        button = transform.FindChild("Button").gameObject;
        buttonIcon = button.GetComponentInChildren<SpriteRenderer>();
        metronome = FindObjectOfType<MetronomeScript>();
	}
	
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        button.SetActive(false);
        metronome.TogglePlay();
    }

    public void Update ()
    {
        Material material = button.GetComponent<Renderer>().material;
        if (metronome.playing)
        {
            buttonIcon.sprite = pause;
            material.SetColor("_Color", new Color(.23f, .7f, .11f));
        }
        else
        {
            buttonIcon.sprite = play;
            material.SetColor("_Color", new Color(.5f, .7f, .5f));
        }
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        button.SetActive(true);
    }
}
