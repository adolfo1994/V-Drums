using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;  

public class TutorialScript : MonoBehaviour {

	ArrayList tabList;
	public bool tutorialActive;
	private int crashIndex = 0;
	private int rideIndex = 0;
	private int snareIndex = 0;
	private int tomIndex = 0;

	void Start () {
		disableTutorialLights ();
		tutorialActive = false;
		tabList = new ArrayList ();
		try
		{
			string line;
			StreamReader theReader = new StreamReader("Assets/Texts/tabs.txt", Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
					Debug.Log (line);

					if (line != null)
					{
						tabList.Add (line);
					}
				}
				while (line != null);
				theReader.Close();
			}
		}
		catch (IOException e)
		{
			Debug.LogError(e.Message);
		}
	}

	// Update is called once per frame
	void Update () {
		if(tutorialActive){
			GameObject[] tutorialLights = GameObject.FindGameObjectsWithTag ("Tutorial");
			foreach (GameObject light in tutorialLights) {
				if(light.transform.parent.name == "CrashTarget"){
					if(light.GetComponent<TutorialLightScript>().done){
						if(tabList.ToArray()[crashIndex].ToString()[0] == 'X'){
							light.GetComponent<TutorialLightScript>().Appear();
						}
						if(crashIndex+1 < tabList.Count){
							crashIndex += 1;
							Debug.Log (crashIndex);
						}else{
							crashIndex = 0;
						}
					}
				}
				if(light.transform.parent.name == "RideTarget"){
					if(light.GetComponent<TutorialLightScript>().done){
						if(tabList.ToArray()[rideIndex].ToString()[1] == 'X'){
							light.GetComponent<TutorialLightScript>().Appear();
						}
						if(rideIndex+1 < tabList.Count){
							rideIndex += 1;
						}else{
							rideIndex = 0;
						}
					}
				}
				if(light.transform.parent.name == "SnareTarget"){
					//Debug.Log(tabList.ToArray()[snareIndex].ToString()[2]);
					if(light.GetComponent<TutorialLightScript>().done){
						if(tabList.ToArray()[snareIndex].ToString()[2]== 'X'){
							light.GetComponent<TutorialLightScript>().Appear();
						}
						if(snareIndex+1 < tabList.Count){
							snareIndex += 1;
						}else{
							snareIndex = 0;
						}
					}

				}
				if(light.transform.parent.name == "Tom1Target"){
					if(light.GetComponent<TutorialLightScript>().done){
						if(tabList.ToArray()[tomIndex].ToString()[3]== 'X'){
							light.GetComponent<TutorialLightScript>().Appear();
						}
						if(tomIndex+1 < tabList.Count){
							tomIndex += 1;
						}else{
							tomIndex = 0;
						}

					}
				}
			}
		}
	}
	void disableTutorialLights(){
		GameObject[] tutorialLights = GameObject.FindGameObjectsWithTag ("Tutorial");
		foreach (GameObject light in tutorialLights) {
			light.GetComponent<Light>().intensity = 0;
		}
	}
}
