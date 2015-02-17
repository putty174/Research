using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class Programmable : MonoBehaviour {

	public bool window = false;
	private bool reset = true;
	string[] command;
	int orders;
	int place;
	int location;
	public Vector3 spawn;
	string dir = "";
	int error = -1;
	Stack<int> loops;

	GameObject player;
	Camera cam;
	GameObject obj;
	bool move;
	float nextStep;
	Vector3 startPos;
	Vector3 eulOri;
	Quaternion ori;

	// Use this for initialization
	void Start () {
		orders = 10;
		command = new string[orders];
		for (int i = 0; i < orders; i++)
						command [i] = "";
		place = 0;
		loops = new Stack<int> ();

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

		if (Vector3.Distance(obj.transform.position, startPos) > 0.98f){
			obj.rigidbody.velocity = Vector3.zero;
			obj.transform.position = new Vector3(Mathf.Round(obj.transform.position.x), obj.transform.position.y, Mathf.Round (obj.transform.position.z));
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
		}
		if (move) {
			if (Time.time > nextStep) {
				analyze ();
				location++;
				nextStep = Time.time + 1.5f;
			}
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

			GUI.SetNextControlName("0");
			command[0] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f, Screen.width * 0.35f, Screen.height * 0.05f), command[0]);
			GUI.SetNextControlName("1");
			command[1] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 1), Screen.width * 0.35f, Screen.height * 0.05f), command[1]);
			GUI.SetNextControlName("2");
			command[2] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 2), Screen.width * 0.35f, Screen.height * 0.05f), command[2]);
			GUI.SetNextControlName("3");
			command[3] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 3), Screen.width * 0.35f, Screen.height * 0.05f), command[3]);
			GUI.SetNextControlName("4");
			command[4] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 4), Screen.width * 0.35f, Screen.height * 0.05f), command[4]);
			GUI.SetNextControlName("5");
			command[5] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 5), Screen.width * 0.35f, Screen.height * 0.05f), command[5]);
			GUI.SetNextControlName("6");
			command[6] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 6), Screen.width * 0.35f, Screen.height * 0.05f), command[6]);
			GUI.SetNextControlName("7");
			command[7] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 7), Screen.width * 0.35f, Screen.height * 0.05f), command[7]);
			GUI.SetNextControlName("8");
			command[8] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 8), Screen.width * 0.35f, Screen.height * 0.05f), command[8]);
			GUI.SetNextControlName("9");
			command[9] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f+ (40f * 9), Screen.width * 0.35f, Screen.height * 0.05f), command[9]);

			//place = int.Parse(GUI.GetNameOfFocusedControl());
			int.TryParse(GUI.GetNameOfFocusedControl(),out place);
			//Debug.Log (command);
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.2f, Screen.width * 0.07f, Screen.height * 0.07f), "forward")) {
				command[place] = "forward;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.36f, Screen.width * 0.07f, Screen.height * 0.07f), "back")) {
				command[place] = "back;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.11f, Screen.height * 0.28f, Screen.width * 0.07f, Screen.height * 0.07f), "move left")) {
				command[place] = "move left;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.19f, Screen.height * 0.28f, Screen.width * 0.07f, Screen.height * 0.07f), "move right")) {
				command[place] = "move right;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.07f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "turn left")) {
				command[place] = "turn left;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.23f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "turn right")) {
				command[place] = "turn right;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.37f, Screen.height * 0.15f, Screen.width * 0.07f, Screen.height * 0.07f), "light")) {
				command[place] = "light;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.5f, Screen.width * 0.07f, Screen.height * 0.07f), "[x] loops")) {
				command[place] += " loops;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.58f, Screen.width * 0.07f, Screen.height * 0.07f), "end")) {
				command[place] = "end;";
				place++;
				GUI.FocusControl(""+place);
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.7f, Screen.width * 0.07f, Screen.height * 0.07f), "restart")) {
				command[place] = "restart;";
				place++;
				GUI.FocusControl(""+place);
			}
			reset = GUI.Toggle(new Rect(Screen.width*0.57f,Screen.height*0.87f,Screen.width*0.15f,Screen.height*0.5f), reset, "Reset robot when done");

			if(GUI.Button(new Rect(Screen.width*0.8f,Screen.height*0.87f,Screen.width*0.07f,Screen.height*0.05f), "Clear")) {
				for(int i = 0; i < command.Length; i++){
					command[i] = "";
				}
			}
			if(GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Run")) {
				//analyze(command);
				location = 0;
				move = true;
				nextStep = Time.time + 1;
				window = false;
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Cancel")) {
				nextStep = Time.time;
				window = false;
			}
		} else {
		}
		if(error != -1)
			GUI.Box (new Rect (Screen.width * 0.35f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.2f), "Line " + error + ": Check spelling or syntax");
	}

	private void analyze() {
		if (command [location].StartsWith ("forward"))
			rigidbody.AddForce (transform.forward * 200);
		else if (command [location].StartsWith ("back"))
			rigidbody.AddForce (transform.forward * -200);
		else if (command [location].StartsWith ("move left"))
			rigidbody.AddForce (transform.right * -200);
		else if (command [location].StartsWith ("move right"))
			rigidbody.AddForce (transform.right * 200);
		else if (command [location].StartsWith ("turn left")) {
			eulOri.y -= 90;
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
		}
		else if (command [location].StartsWith ("turn right")) {
			eulOri.y += 90;
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
		}
		else if (command [location].StartsWith ("restart")) {
			obj.transform.position = spawn;
			obj.rigidbody.velocity = Vector3.zero;
			obj.transform.rotation = Quaternion.Euler(0,0,0);
		}
		else if (command [location].StartsWith ("light")) {
			rigidbody.AddForce(transform.up * 100);
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
		else if (command [location].EndsWith ("loops;")) {
			string[] loopStr = command[location].Split(' ');
			int times = 0;
			int.TryParse(loopStr[0], out times);
			for (int i = 0; i < times - 1; i++)
				loops.Push(location);
			Debug.Log(loops.Count);
		}
		else if (command [location].StartsWith ("end")) {
			if(loops.Count == 0) {}
			else
				location = loops.Pop();
		}
		startPos = obj.transform.position;
	}
}
