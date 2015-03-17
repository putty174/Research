using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	public static int lineNum = 0;
	public string[] lines;
	static bool display;
	static float tabTime;
	public float waittime;

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
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Tab) && Time.time > tabTime + waittime) {
				display = false;
			}
		}
	}

	void OnGUI() {
		if (display) {
			GUI.depth = 0;
			GUI.Box (new Rect (Screen.width * 0.02f, Screen.height * 0.75f, Screen.width * 0.94f, Screen.height * 0.23f), lines[lineNum]);
		}
	}

	public static void nextLevel(){
		lineNum = 0;
		display = true;
	}

	public static void nextLine() {
		lineNum++;
		display = true;
		tabTime = Time.time;
	}
}
