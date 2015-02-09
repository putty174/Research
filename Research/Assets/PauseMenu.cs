using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	GameObject player;
	bool quit;
	bool pause;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		player.GetComponent<MouseLook>().enabled = !pause;
		Camera.main.GetComponent<MouseLook> ().enabled = !pause;
		if (Input.GetKeyDown("escape"))
		{
			pause = !pause;
		}
	}

	void OnGUI()
	{
		if (pause)
		{
			if(!quit) {
				GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.4f, Screen.height * 0.2f), "Pause");
				if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f), "Resume"))
					pause = false;
				if (GUI.Button(new Rect (Screen.width * 0.5f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f), "Quit"))
					quit = true;
			}
			else {
				GUI.Box(new Rect(Screen.width * 0.2f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.2f), "Are you sure?");
				if (GUI.Button (new Rect (Screen.width * 0.25f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f), "Back"))
					quit = false;
				if (GUI.Button(new Rect (Screen.width * 0.55f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f), "Exit Game"))
					Application.Quit();
			}
		}
	}
}
