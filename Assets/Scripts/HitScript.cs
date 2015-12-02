using UnityEngine;
using Vuforia;

public class HitScript : MonoBehaviour, IVirtualButtonEventHandler {

    private GameObject glare;
    private AudioSource sound;

	void Start () {
        sound = GetComponentInChildren<AudioSource>();
        glare = transform.FindChild("Glare").gameObject;

        GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}
	
	void Update () {
	
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        sound.Play();
        glare.SetActive(true);
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        glare.SetActive(false);
    }

}
