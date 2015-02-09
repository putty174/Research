using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
	bool display = false;
	public string dialogue;
	public bool stayOn;
	bool on;
	string parsedText;

	void Start () {
		on = true;
		parsedText = dialogue.Replace(". ", ".\n");
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag.Equals("Player") && on) {
			display = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if(other.gameObject.tag.Equals("Player")) {
			display = false;
			if(!stayOn) {
				on = false;
			}
		}
	}

	void OnGUI() {
		if (display) {
			GUI.Box (new Rect (Screen.width * 0.05f, Screen.height * 0.75f, Screen.width * 0.9f, Screen.height * 0.2f), parsedText);
		}
	}
}