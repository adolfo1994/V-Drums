using System;
using UnityEngine;
using Vuforia;

public class StartScript : MonoBehaviour, IVirtualButtonEventHandler {

    public int point;
    public GameObject button;
    public Transform allButtons;

    private MetronomeScript metronome;

    void Start () {
        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        metronome = FindObjectOfType<MetronomeScript>();
	}
	
	void Update () {
	
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        metronome.SetStart(point);
        for (int i = 0; i < allButtons.childCount; ++i)
            allButtons.GetChild(i).gameObject.SetActive(false);
        button.SetActive(true);
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }
}
