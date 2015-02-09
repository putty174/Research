using UnityEngine;
using System.Collections;
using System.Timers;

public class Programmable : MonoBehaviour {

	public bool window = false;
	private bool reset = true;
	string command;
	public Vector3 spawn;
	bool loop;
	string dir = "";

	GameObject player;
	Camera cam;
	Queue commands = new Queue();
	GameObject obj;
	bool move;
	float nextStep;
	Vector3 startPos;
	Vector3 eulOri;
	Quaternion ori;

	// Use this for initialization
	void Start () {
		command = "";

		eulOri = new Vector3 (0, 0, 0);
		ori = Quaternion.Euler (eulOri.x, eulOri.y, eulOri.z);
		player = GameObject.FindGameObjectWithTag ("Player");
		cam = player.GetComponent<Camera> ();
		obj = gameObject;
		move = false;
	}
	
	// Update is called once per frame
	void Update () {
		player.GetComponent<MouseLook>().enabled = !window;
		Camera.main.GetComponent<MouseLook> ().enabled = !window;
		if (Input.GetKeyDown (KeyCode.Tab)) {
			window = !window;
			if(Dialogue.lineNum == 1 && Application.loadedLevelName == "Scene2"){
				Dialogue.nextLine();
			}
		}

		if (Vector3.Distance(obj.transform.position, startPos) > 0.95f){
			obj.rigidbody.velocity = Vector3.zero;
			obj.transform.position = new Vector3(Mathf.Round(obj.transform.position.x), obj.transform.position.y, Mathf.Round (obj.transform.position.z));
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
				}

		if(Input.GetKeyDown(KeyCode.I)){
			commands.Enqueue ("I");
			Debug.Log(commands.Count);
		}
		if(Input.GetKeyDown(KeyCode.J)) {
			commands.Enqueue ("J");
			Debug.Log(commands.Count);
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			commands.Enqueue ("K");
			Debug.Log(commands.Count);
		}
		if(Input.GetKeyDown(KeyCode.L)) {
			commands.Enqueue ("L");
			Debug.Log(commands.Count);
		}
		if(Input.GetKeyDown(KeyCode.P)) {
			move = true;
			nextStep = Time.time + 1;
		}

		if (move && commands.Count > 0) {
			if (Time.time > nextStep) {
				if (!loop)
					dir = (string)commands.Dequeue ();
				if (dir.Equals ("I")) {
					rigidbody.AddForce(transform.forward * 200);
					startPos = obj.transform.position;
				}
				if (dir.Equals ("J")) {
					rigidbody.AddForce(transform.right * -200);
					startPos = obj.transform.position;
				}
				if (dir.Equals ("K")) {
					rigidbody.AddForce(transform.forward * -200);
					startPos = obj.transform.position;
				}
				if (dir.Equals ("L")) {
					rigidbody.AddForce(transform.right * 200);
					startPos = obj.transform.position;
				}
				if (dir.Equals ("U")) {
					eulOri.y -= 90;
					obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
						startPos = obj.transform.position;
				}
				if (dir.Equals ("O")) {
					eulOri.y += 90;
					obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
					startPos = obj.transform.position;
				}
				if(dir.Equals("loop")){
				loop = true;
				}
				if(dir.Equals("end")){
					loop = false;
				}
				if(dir.Equals("Restart")) {
					obj.transform.position = spawn;
					obj.rigidbody.velocity = Vector3.zero;
					obj.transform.rotation = Quaternion.Euler(0,0,0);
				}
					if(dir.Equals("light")){
						GameObject l = new GameObject("Light");
						l.tag = "ExtraLight";
						l.AddComponent<Light>();
						l.light.type = LightType.Spot;
						l.transform.eulerAngles = new Vector3(90,0,0);
						l.light.intensity = 8;
						l.light.range = 21;
						l.light.spotAngle = 40;

						Vector3 lpos = new Vector3(obj.transform.position.x, obj.transform.position.y + 20f,obj.transform.position.z);
						l.transform.position = lpos;

						}
				nextStep = Time.time + 1f;
			}
				} else {
						move = false;
				}
	}

	void OnGUI() {
		if (window) {
			if(reset){
				obj.transform.position = spawn;
				eulOri.x = 0;
				eulOri.y = 0;
				eulOri.z = 0;
				obj.transform.rotation = Quaternion.Euler(0,0,0);
				obj.rigidbody.velocity = Vector3.zero;
				GameObject[] lights = GameObject.FindGameObjectsWithTag("ExtraLight");
				foreach(GameObject lte in lights)
					Destroy(lte);
			}
			GUI.Box (new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.9f, Screen.height * 0.9f), "Programming");
			command = GUI.TextArea (new Rect (Screen.width * 0.55f, Screen.height * 0.1f, Screen.width * 0.35f, Screen.height * 0.75f), command);
			//Debug.Log (command);
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.2f, Screen.width * 0.07f, Screen.height * 0.07f), "forward")) {
				command += "forward;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.36f, Screen.width * 0.07f, Screen.height * 0.07f), "back")) {
				command += "back;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.11f, Screen.height * 0.28f, Screen.width * 0.07f, Screen.height * 0.07f), "move left")) {
				command += "move left;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.19f, Screen.height * 0.28f, Screen.width * 0.07f, Screen.height * 0.07f), "move right")) {
				command += "move right;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.07f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "turn left")) {
				command += "turn left;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.23f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "turn right")) {
				command += "turn right;\n";
			}
			if(GUI.Button(new Rect(Screen.width * 0.37f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "light")) {
				command += "light;\n";
			}
//			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.5f, Screen.width * 0.07f, Screen.height * 0.07f), "loop")) {
//				command += "loop;\n";
//			}
//			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.58f, Screen.width * 0.07f, Screen.height * 0.07f), "end")) {
//				command += "end;\n";
//			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.7f, Screen.width * 0.07f, Screen.height * 0.07f), "restart")) {
				command += "restart;\n";
			}
			reset = GUI.Toggle(new Rect(Screen.width*0.57f,Screen.height*0.87f,Screen.width*0.15f,Screen.height*0.5f), reset, "Reset robot when done");

			if(GUI.Button(new Rect(Screen.width*0.8f,Screen.height*0.87f,Screen.width*0.07f,Screen.height*0.05f), "Clear")) {
				command = "";
			}
			if(GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Run")) {
				analyze(command);
				move = true;
				nextStep = Time.time + 1;
				window = false;
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Cancel")) {
				commands.Clear();
				command = "";
				nextStep = Time.time;
				window = false;
			}
		} else {
		}
	}

	private void analyze (string command) {
		Debug.Log (command);
		string[] delimiter = {";\n"};
		string[] list = command.Split(delimiter,System.StringSplitOptions.None);
		Debug.Log (list);
		foreach (string s in list) {
			Debug.Log(s);
				}
		Debug.Log (list.Length-1);
		foreach (string s in list) {
			switch (s) {
			case "forward":
				commands.Enqueue("I");
				break;
			case "back":
				commands.Enqueue("K");
				break;
			case "move left":
				commands.Enqueue("J");
				break;
			case "move right":
				commands.Enqueue("L");
				break;
			case "turn left":
				commands.Enqueue("U");
				break;
			case "turn right":
				commands.Enqueue("O");
				break;
			case "loop":
				commands.Enqueue("loop");
				break;
			case "end":
				commands.Enqueue("end");
				break;
			case "light":
				commands.Enqueue("light");
				break;
			case "restart":
				commands.Enqueue("Restart");
				break;
			case "\nforward":
				commands.Enqueue("I");
				break;
			case "\nback":
				commands.Enqueue("K");
				break;
			case "\nleft":
				commands.Enqueue("J");
				break;
			case "\nright":
				commands.Enqueue("L");
				break;
			case "\nturn left":
				Debug.Log("YES");
				commands.Enqueue("U");
				break;
			case "\nturn right":
				Debug.Log("NO");
				commands.Enqueue("O");
				break;
			case "\nlight":
				commands.Enqueue("light");
				break;
			case "\nrestart":
				commands.Enqueue("Restart");
				break;
			default:
				break;
			}
		}
	}
}
