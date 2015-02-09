using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	public static int lineNum = 0;
	public string[] lines;
	static bool display;

	// Use this for initialization
	void Start () {
		display = true;
		for(int i = 0; i < lines.Length; i++){
			lines[i] = lines[i].Replace(". ", ".\n");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (display) {
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Tab)) {
				display = false;
			}
		}
	}

	void OnGUI() {
		if (display) {
			GUI.Box (new Rect (Screen.width * 0.05f, Screen.height * 0.75f, Screen.width * 0.9f, Screen.height * 0.2f), lines[lineNum]);
		}
	}

	public static void nextLevel(){
		lineNum = 0;
		display = true;
		}

	public static void nextLine() {
		lineNum++;
		display = true;
	}
}
