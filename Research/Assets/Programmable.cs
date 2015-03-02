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
	Stack<KeyValuePair<int,int>> loops;
	string loopval;
    string colorval;

    Stack<bool> ifs;
	int ifstack;

	GameObject player;
	GameObject obj;
	bool move;
	float nextStep;
	Vector3 startPos;
	Quaternion startOri;
	Vector3 eulOri;

	// Use this for initialization
	void Start () {
		orders = 10;
		command = new string[orders];
		for (int i = 0; i < orders; i++)
						command [i] = "";
		loopval = "";
        colorval = "";
		place = 0;
		loops = new Stack<KeyValuePair<int,int>> ();

        ifs = new Stack<bool> ();

		eulOri = new Vector3 (0, 0, 0);
		player = GameObject.FindGameObjectWithTag ("Player");
		obj = gameObject;
		move = false;

		startPos = obj.transform.position;
		startOri = obj.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		player.GetComponent<MouseLook>().enabled = !window;
		Camera.main.GetComponent<MouseLook> ().enabled = !window;
		if (Input.GetKeyDown (KeyCode.Tab)) {
			window = !window;
			if(Dialogue.lineNum == 0 && Application.loadedLevelName == "Level1"){
				Dialogue.nextLine();
			}
		}

		if (Vector3.Distance(obj.transform.position, startPos) > 0.98f){
			obj.rigidbody.velocity = Vector3.zero;
			obj.transform.position = new Vector3(Mathf.Round(obj.transform.position.x), obj.transform.position.y, Mathf.Round (obj.transform.position.z));
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
		}
		if (Mathf.Abs(Quaternion.Angle (gameObject.transform.rotation, startOri)) > 90.0f) {
			obj.transform.rotation = Quaternion.Euler(eulOri.x, eulOri.y, eulOri.z);
			obj.rigidbody.angularVelocity = Vector3.zero;
		}
		if (move) {
			if (Time.time > nextStep) {
				analyze ();
				nextStep = Time.time + 0.5f;
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
                GameObject[] mark = GameObject.FindGameObjectsWithTag("Mark");
                foreach(GameObject mk in mark)
                    Destroy(mk);
			}
			GUI.Box (new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.9f, Screen.height * 0.9f), "Programming");

			for(int i = 0; i < orders; i++) {
				GUI.SetNextControlName("Index " + i);
				command[i] = GUI.TextField (new Rect (Screen.width * 0.55f, Screen.height * 0.1f + (40f * i), Screen.width * 0.35f, Screen.height * 0.05f), command[i]);
			}
            if(GUI.GetNameOfFocusedControl().StartsWith("Index")){
                string[] i = GUI.GetNameOfFocusedControl().Trim ().Split (' ');
                place = int.Parse(i[1]);
            }

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
			GUI.SetNextControlName("loopval");
			loopval = GUI.TextField (new Rect (Screen.width * 0.11f, Screen.height * 0.51f, Screen.width * 0.03f, Screen.height * 0.04f), loopval);
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.5f, Screen.width * 0.07f, Screen.height * 0.07f), "[x] loops")) {
				if(!loopval.Equals(""))
					command[place] += loopval;
				command[place] += " loops;";
				place++;
				GUI.FocusControl(""+place);
			}
            if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.58f, Screen.width * 0.07f, Screen.height * 0.07f), "end")) {
                command[place] = "end;";
                place++;
                GUI.FocusControl(""+place);
            }
            GUI.SetNextControlName("colorval");
            colorval = GUI.TextField (new Rect(Screen.width * 0.25f, Screen.height * 0.58f, Screen.width * 0.07f, Screen.height * 0.04f), colorval);
            if(GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.5f, Screen.width * 0.07f, Screen.height * 0.07f), "Mark [Color]")) {
                if(!colorval.Equals(""))
                    command[place] += colorval;
                command[place] = "Mark: " + colorval + ";";
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
				GUI.FocusControl("0");
			}
			if(GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Run")) {
				location = 0;
				move = true;
				nextStep = Time.time + 1;
				window = false;
			}
			if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f), "Cancel")) {
				nextStep = Time.time;
				window = false;
			}
            if (place == orders)
                place = 0;
		}
	}

	private void analyze() {
		//Debug.Log (location + ": " + command[location]);
		if (command [location].Trim ().StartsWith ("forward")) {
            rigidbody.AddForce (transform.forward * 200);
            location++;
        } else if (command [location].Trim ().StartsWith ("back")) {
            rigidbody.AddForce (transform.forward * -200);
            location++;
        } else if (command [location].Trim ().StartsWith ("move left")) {
            rigidbody.AddForce (transform.right * -200);
            location++;
        } else if (command [location].Trim ().StartsWith ("move right")) {
            rigidbody.AddForce (transform.right * 200);
            location++;
        } else if (command [location].Trim ().StartsWith ("turn left")) {
            eulOri.y -= 90;
            rigidbody.AddTorque (Vector3.up * -50);
            location++;
        } else if (command [location].Trim ().StartsWith ("turn right")) {
            eulOri.y += 90;
            rigidbody.AddTorque (Vector3.up * 50);
            location++;
        } else if (command [location].Trim ().StartsWith ("restart")) {
            obj.transform.position = spawn;
            obj.rigidbody.velocity = Vector3.zero;
            obj.transform.rotation = Quaternion.Euler (0, 0, 0);
            location++;
        } else if (command [location].Trim ().StartsWith ("light")) {
            rigidbody.AddForce (transform.up * 100);
            GameObject l = new GameObject ("Light");
            l.tag = "ExtraLight";
            l.AddComponent<Light> ();
            l.light.type = LightType.Spot;
            l.transform.eulerAngles = new Vector3 (90, 0, 0);
            l.light.intensity = 8;
            l.light.range = 21;
            l.light.spotAngle = 40;
			
            Vector3 lpos = new Vector3 (obj.transform.position.x, obj.transform.position.y + 20f, obj.transform.position.z);
            l.transform.position = lpos;
            location++;
        } else if (command [location].Trim ().EndsWith ("loops;")) {
            string[] loopStr = command [location].Trim ().Split (' ');
            int times = 0;
            int.TryParse (loopStr [0], out times);
            loops.Push (new KeyValuePair<int,int> (location + 1, times - 1));
            location++;
            nextStep = Time.time + 0.01f;
        } else if (command [location].Trim ().StartsWith ("end")) {
            if (loops.Count == 0) {
            } else {
                KeyValuePair<int,int> h = loops.Pop ();
                if (h.Value != 0) {
                    location = h.Key;
                    loops.Push (new KeyValuePair<int,int> (h.Key, h.Value - 1));
                } else
                    location++;
            }
            nextStep = Time.time + 0.01f;
        } else if (command [location].Trim ().StartsWith ("if")) {
            string[] ifStr = command [location].Split (' ');
            bool yes = ((ifStr [2] == "is") || (ifStr [2] == "=="));

        } else if (command [location].Trim ().StartsWith ("Mark:")) {
            string[] colorStr = command [location].Trim ().Split (' ');
            string color = colorStr[1];
            Debug.Log (color);
            GameObject newMark = GameObject.CreatePrimitive(PrimitiveType.Plane);
            newMark.tag = "Mark";
            newMark.transform.position = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
            newMark.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            newMark.AddComponent<Rigidbody>();
            newMark.rigidbody.useGravity = false;
            if(color.StartsWith("r") || color.StartsWith("R"))
                newMark.renderer.material.color = Color.red;
            else if(color.StartsWith("o") || color.StartsWith("R"))
                newMark.renderer.material.color = new Color(1f, 0.647f, 0f);
            else if(color.StartsWith("y") || color.StartsWith("Y"))
                newMark.renderer.material.color = Color.yellow;
            else if(color.StartsWith("gree") || color.StartsWith("Gree"))
                newMark.renderer.material.color = Color.green;
            else if(color.StartsWith("bl") || color.StartsWith("Bl"))
                newMark.renderer.material.color = Color.blue;
            else if(color.StartsWith("p") || color.StartsWith("p"))
                newMark.renderer.material.color = new Color(0.545f, 0f, 0.8f);
            else if(color.StartsWith("bl") || color.StartsWith("Bl"))
                newMark.renderer.material.color = Color.black;
            else if(color.StartsWith("w") || color.StartsWith("W"))
                newMark.renderer.material.color = Color.white;
            else if(color.StartsWith("c") || color.StartsWith("C"))
                newMark.renderer.material.color = Color.cyan;
            else if(color.StartsWith("gra") || color.StartsWith("Gra"))
                newMark.renderer.material.color = Color.gray;
            else if(color.StartsWith("grey") || color.StartsWith("Grey"))
                newMark.renderer.material.color = Color.grey;
            else if(color.StartsWith("m") || color.StartsWith("m"))
                newMark.renderer.material.color = Color.magenta;
            location++;
        }
		startPos = obj.transform.position;
		startOri = obj.transform.rotation;
	}
}
