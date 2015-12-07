using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

    float timer;

	void Start () {
        timer = 3;
	}
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Application.LoadLevel(1);
        }
	}
}
