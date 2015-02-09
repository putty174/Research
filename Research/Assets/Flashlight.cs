using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
	GameObject light;
	bool on;

	// Use this for initialization
	void Start () {
		light = gameObject;
		on = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
						on = !on;
			if(Dialogue.lineNum == 0 && Application.loadedLevelName == "Scene2"){
				Dialogue.nextLine();
			}
		}
		if (on) {
						light.light.intensity = 1;
				} else {
						light.light.intensity = 0;
				}
	}
}
